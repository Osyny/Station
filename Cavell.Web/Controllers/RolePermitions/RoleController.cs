using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Station.Core;
using Station.Core.Entities.Identities;
using Station.Core.Enums;
using Station.Web.Controllers.RolePermitions.Dtos;
using Station.Web.Dtos;
using Station.Web.Dtos.IdentityDtos;
using System.Runtime.InteropServices;

namespace Station.Web.Controllers.RolePermitions
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public RoleController(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("get-roles")]
        [AllowAnonymous]
        public async Task<List<RoleDto>> GetRolesAsync()
        { /* ... */

           List<Role>  roles =  await _dbContext.Roles.AsNoTracking()
                .Include(r => r.RolePermissions)
                   .ThenInclude(p => p.PermissionAction)
                   .ThenInclude(a => a.PermissionCategory)
                .Include(r => r.UserRoles)
                .ToListAsync();

            foreach (var role in roles)
            {
                foreach (var rolePermission in role.RolePermissions)
                {
                    rolePermission.Role = null;
                    
                }
            }
         var map = _mapper.Map<List<RoleDto>>(roles);

            return map;     
        }


        //[HttpPost("CreateRole")]
        //public async Task CreateRoleAsync(RoleDto role)
        //{ /* ... */ }

        // ...

        //public async Task<List<RolePermission>> AssignRolePermissionsAsync(RolePermissionDtoInp permissions)
        //{
        //    var featurePermissions = permissions.PermissionActionIds.Select(
        //        x => new RolePermission()
        //        {
        //            RoleId = permissions.RoleId,
        //            PermissionActionId = x
        //        }).ToList();

        //    foreach (var permission in featurePermissions)
        //    {
        //        if (await IsRolePermissionExistAsync(permission.RoleId ?? 0, permission.PermissionActionId ?? 0))
        //            continue;
        //        _dbContext.RolePermissions.Add(permission);
        //    }

        //    await _context.SaveChangesAsync();
        //    return featurePermissions;
        //}

        [HttpPost("assign-role-permission")]
        public async Task<List<RolePermission>> AssignRolePermissionsAsync([FromForm]RolePermissionDtoInut permissions)
        {
            var featurePermissions = permissions.PermissionActionIds.Select(
                x => new RolePermission()
                {
                    RoleId = permissions.RoleId,
                    PermissionActionId = x
                }).ToList();

            foreach (var permission in featurePermissions)
            {
                if (await IsRolePermissionExistAsync(permission.RoleId ?? 0, permission.PermissionActionId ?? 0))
                    continue;
                _dbContext.RolePermissions.Add(permission);
            }

            await _dbContext.SaveChangesAsync();
            return featurePermissions;
        }

        private async Task<bool> IsRolePermissionExistAsync(int roleId, int permissionActionId)
        {
            bool IsRolePermissionExist = await _dbContext.RolePermissions
                .AnyAsync(p => p.PermissionActionId == permissionActionId &&
             p.RoleId == roleId);

            return IsRolePermissionExist;
        }

    }
}
