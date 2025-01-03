using Station.AutoMapper;

using Station.Core;
using Station.Web.Services.JwtProviders;
using Station.Web.Services.PasswordHashers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Station.Web.Services;
using Microsoft.AspNetCore.Http.Connections;
using Station.Web.Controllers.ChargeStations;
using Station.Web.Controllers.RolePermitions.Helpers;
using Station.Web.Controllers.RolePermitions.Helpers.Interfaces;
using Station.Web.Seeds;
using static Station.Web.Seeds.DefaultRolesPermissions;
using Microsoft.Extensions.Hosting;

namespace Cavell
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            builder.Services.AddScoped<IChargeStationsManager, ChargeStationsManager>();
            builder.Services.AddScoped<IManegerRolePermissions, ManegerRolePermissions>();
            builder.Services.AddScoped<IHelperPermissions, HelperPermissions>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connetionString = builder.Configuration.GetConnectionString("Default");
                options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
            });

            var corsOrigins = configuration["App:CorsOrigins"] == null ? "http://localhost:4200" : configuration["App:CorsOrigins"];
            builder.Services.AddCors(
                options => options.AddPolicy(
                    "Default Policy",
                    builder => builder
                    .WithOrigins(
                            // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                            corsOrigins?
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .ToArray()
                        )
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials()


                ));

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddSingleton<TimerControl>();
            builder.Services.AddSignalR();



            // configure JWT Bearer authentication
            var key = configuration["Authentication:JwtBearer:SecurityKey"];

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
                {
                    option.RequireHttpsMetadata = false;
                    option.SaveToken = true;
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                        //LifetimeValidator = CustomLifetimeValidator,
                        //RequireExpirationTime = true,
                    };
                    option.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["token"];
                            return Task.CompletedTask;
                        }
                    };
                });
            builder.Services.AddAuthorization();

            var app = builder.Build();


            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always,
            });
            app.UseCors("Default Policy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<StationHub>("/stationhub", options =>
                {
                    options.Transports =
                        HttpTransportType.WebSockets |
                        HttpTransportType.LongPolling;
                });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/Home/Index");
                    return Task.CompletedTask;
                });
            });

            //2.Build an intermediate service provider
           var sp = builder.Services.BuildServiceProvider();
           DefaultRolesPermissions.SeedRolePermissions(sp);

            app.MapControllers();

            app.Run();
        }
    }
}
