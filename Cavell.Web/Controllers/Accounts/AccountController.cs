
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

        public AccountController(
            ApplicationDbContext dbContext,
            IJwtProvider jwtProvider,
            IPasswordHasher passwordHasher,
            IConfiguration configuration,
            IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }


        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<ActionResult<AccountResponse>> Login([FromQuery] LoginInput input)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == input.Email);
            if (user != null)
            {
                var result = _passwordHasher.Veryfy(input.Password, user.HashPasword);

                if (!result)
                {
                    return new AccountResponse();
                }
                var token = _jwtProvider.GenerateToken(user);
                // var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                HttpContext.Response.Cookies.Append("token", token);
                var map = _mapper.Map<UserDto>(user);
               // Ok(token);
                return  Ok(new AccountResponse() { User = map, Token = token }); //new AccountResponse() { User = map, Token = token };
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
        public async Task<IActionResult> Register([FromForm] RegisterInput input)
        {
            var hashPasword = _passwordHasher.GetHashadPasword(input.Password);
            User user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == input.Email);
            if (user != null)
            {
                ModelState.AddModelError("message", "User alredy exist!!!.");

                return BadRequest(ModelState);
            }
            user = new User
            {
                UserName = input.UserName,
                Email = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Role = input.Role,
                HashPasword = hashPasword,
            };

            var result = await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();


            return Ok(new { userID = user.Id });

        }


    }
}
