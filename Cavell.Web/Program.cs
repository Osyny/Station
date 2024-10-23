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

            builder.Services.AddCors(
                options => options.AddPolicy(
                    "Default Policy",
                    builder => builder
                    .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod()


                ));

            builder.Services.AddAutoMapper(typeof(MappingProfile));

         //   builder.Services.AddSingleton<TimerControl>();
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
            app.UseHttpsRedirection();
            app.UseCors("Default Policy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/Home/Index");
                    return Task.CompletedTask;
                });
            });

            app.MapControllers();

            app.Run();
        }
    }
}
