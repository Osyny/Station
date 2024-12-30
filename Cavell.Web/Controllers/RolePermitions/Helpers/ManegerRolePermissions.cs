using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Station.Core;
using Station.Core.Entities;
using Station.Core.Entities.Identities;
using Station.Web.Controllers.RolePermitions.Dtos;
using Station.Web.Controllers.RolePermitions.Helpers.Interfaces;
using Station.Web.Controllers.Users.Dtos;

namespace Station.Web.Controllers.RolePermitions.Helpers
{
    public class ManegerRolePermissions : IManegerRolePermissions
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public ManegerRolePermissions(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserRole> CreatedUserRole(UserRoleInput role)
        {
            //if (await IsUserRoleExist(role.UserId, role.RoleId))
            //    throw new CafmException("User already has this role");

            if (await IsUserRoleExist(role.RoleId, role.UserId))
                await UnAssignUserRoleAsync(role.RoleId, role.UserId);

            var createdUserRole = new UserRole()
            {
                UserId = role.UserId,
                RoleId = role.RoleId,
            };

            await _dbContext.UserRoles.AddAsync(createdUserRole);
            await _dbContext.SaveChangesAsync();

            return createdUserRole;
        }

        public async Task UnAssignUserRoleAsync(int roleId, int userId)
        {
            var find = await _dbContext.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (find != null)
            {
                _dbContext.UserRoles.Remove(find);
                await _dbContext.SaveChangesAsync();
            }

        }


        private async Task<bool> IsUserRoleExist(int roleId, int userId)
        {
            bool IsRolePermissionExist = await _dbContext.UserRoles.AnyAsync(u => u.UserId == userId && u.UserId == userId);

            return IsRolePermissionExist;
        }
    }
}
