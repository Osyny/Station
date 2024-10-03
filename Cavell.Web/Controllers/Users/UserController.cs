using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Station.Core;
using Station.Core.Entities;
using Station.Web.Controllers.Users.Dtos;
using Station.Web.Dtos;
using Station.Web.Host.Extentions;
using System.Data;
using Station.Core.Helpers.SelectList;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Station.Web.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;


        public UserController( IMapper mapper, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }

        [HttpGet("getAllUsers")]
        public async Task<UsersResponse> GetAllUsers([FromQuery] UserInput input)
        {
            IQueryable<User> query;
            if (!string.IsNullOrWhiteSpace(input.FilterText))
            {
                input.FilterText = input.FilterText?.ToLower();
                var queryFilter = _dbContext.Users.Where(u => u.FirstName.Contains(input.FilterText) ||
                    u.LastName.ToLower().Contains(input.FilterText) ||
                    u.UserName.ToLower().Contains(input.FilterText) ||
                    u.Email.ToLower().Contains(input.FilterText)).AsNoTracking().AsQueryable();

                query = queryFilter;
            }
            else
            {
                query = _dbContext.Users.AsNoTracking().AsQueryable();
            }
            var count = await query.CountAsync();
            IList<User> sortQuery = await GetSortQuery(input, query);

            List<UserDto> mapUsers = new List<UserDto>();
            foreach (var user in sortQuery)
            {
                var mapUser = MapUserEntityDto(user);
                mapUsers.Add(mapUser);
            }
            //var map = _mapper.Map<List<UserDto>>(sortQuery);


            return new UsersResponse() { Users = mapUsers, Total = count };
        }

        [HttpGet("getRoles")]
        public UsersRolesResponse GetRoles()
        {
            var roleResponse = new UsersRolesResponse()
            {
                RoleSelectList = ItemsHelpers.GetRoleItems()

            };
            return roleResponse;
        }

        private async Task<IList<User>> GetSortQuery(UserInput input, IQueryable<User> query)
        {
            var parse = input?.Sorting?.Split(" ");

            IList<User>? sortQuery = null;

            if (parse != null && parse.Count() > 1)
            {
                var type = parse[0].First().ToString().ToUpper() + parse[0].Substring(1);
                var propertyInfo = typeof(User).GetProperty(type);

                switch (parse[1])
                {
                    case "asc":
                        sortQuery = await query.OrderByField(type, true).Skip((int)(input?.Skip)).
                             Take((int)(input?.Rows)).AsNoTracking().ToListAsync();

                        return sortQuery;
                    case "desc":
                        sortQuery = await query.OrderByField(type, false).Skip((int)(input?.Skip)).
                            Take((int)(input?.Rows)).AsNoTracking().ToListAsync();
                        return sortQuery;

                }
            }
            else
            {
                sortQuery = await query.OrderBy(p => p.UserName).Skip((int)(input?.Skip)).
                    Take((int)(input?.Rows)).AsNoTracking().ToListAsync();
            }
            return sortQuery;
        }

        private UserDto MapUserEntityDto(User user)
        {
            var map = _mapper.Map<UserDto>(user);
            map.RoleName = user.Role.GetDisplayValue();
            return map;
        }
    }
}
