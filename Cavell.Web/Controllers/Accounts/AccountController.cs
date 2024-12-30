
using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Station.Core;
using Station.Core.Entities;
using Station.Web.Dtos;

using Microsoft.AspNetCore.Authorization;

using Station.Web.Services.JwtProviders;
using Station.Web.Services.PasswordHashers;
using Station.Web.Controllers.Accounts.Dtos;
using System.IdentityModel.Tokens.Jwt;
using Station.Core.Entities.Identities;
using Station.Web.Controllers.RolePermitions.Helpers.Interfaces;
using Station.Web.Controllers.RolePermitions.Helpers;

namespace Cavell.Web.Controllers.Accounts
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;

        private readonly IConfiguration _configuration;
        private readonly IManegerRolePermissions _manegerRolePermissions;
        private readonly IHelperPermissions _helperPermissions;

        public AccountController(
            ApplicationDbContext dbContext,
            IJwtProvider jwtProvider,
            IPasswordHasher passwordHasher,
            IConfiguration configuration,
            IMapper mapper,
            IManegerRolePermissions manegerRolePermissions,
            IHelperPermissions helperPermissions)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _manegerRolePermissions = manegerRolePermissions;
            _helperPermissions = helperPermissions;
        }


        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<ActionResult<AccountResponse>> Login([FromQuery] LoginInput input)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.UserRoles)                  
                .FirstOrDefaultAsync(user => user.Email == input.Email);

            if (user != null)
            {
                var result = _passwordHasher.Veryfy(input.Password, user.HashPasword);

                if (!result)
                {
                    return new AccountResponse();
                }
                var ur = await _dbContext.UserRoles
                    .AsNoTracking()
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.UserId == user.Id);
                var permissionsClaim =
                    await _helperPermissions.GetPermissionClaimsByUserAsync(user.Id);

                var token = _jwtProvider.GenerateTokenAsync(user, permissionsClaim, ur.Role);
 
                HttpContext.Response.Cookies.Append("token", token);

                foreach (var userRole in user.UserRoles) {
                    userRole.User = null;
                }
                var map = _mapper.Map<UserDto>(user);
   
                return  Ok(new AccountResponse() { User = map, Token = token });
            }
           return new AccountResponse();

        }



        [HttpGet("logout")]
        public async Task<bool> Logout()
        {
            Response.Cookies.Delete("token");
            return true;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<OutputDataResponse> Register([FromForm] RegisterInput input)
        {
            var error = "";
            var hashPasword = _passwordHasher.GetHashadPasword(input.Password);
            User user = await _dbContext.Users?.FirstOrDefaultAsync(user => user.Email == input.Email);
            if (user != null)
            {
                error = "User already exist!";
                return new OutputDataResponse() { Error = error };
            }

             Role role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == (int)input.Role);

            user = new User
            {
                UserName = input.UserName,
                Email = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
                UserRoles = new List<UserRole>(),
                HashPasword = hashPasword,
            };

            var result = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            await _manegerRolePermissions.CreatedUserRole(new Station.Web.Controllers.Users.Dtos.UserRoleInput() { RoleId = role.Id, UserId = result.Entity.Id });

            return new OutputDataResponse() { UserId = result.Entity.Id };

        }

        
    }
}
