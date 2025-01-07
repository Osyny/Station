using Microsoft.EntityFrameworkCore;
using Station.Core;
using Station.Core.Entities.Identities;

namespace Station.Web.Controllers.RolePermitions.Helpers
{
    public class HelperPermissions : IHelperPermissions
    {
        private readonly ApplicationDbContext _dbContext;

        public HelperPermissions(ApplicationDbContext dbContext)
        { 
            _dbContext = dbContext;
        }

        public async Task<PermissionsClaim> GetPermissionClaimsByUserAsync(int userId)
        {
            // Fetch the role for the user
            var roleId = await _dbContext
                .UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .FirstOrDefaultAsync();

            if (roleId == default)
            {
                // Handle case where the user does not have an assigned role
                return new PermissionsClaim();
            }

            // Fetch the permissions for the user's role
            var rolePermissions = await _dbContext
                .RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Include(rp => rp.PermissionAction)
                .ThenInclude(pa => pa.PermissionCategory)
                .ToListAsync();

            var permissionCategories = rolePermissions
                .GroupBy(rp => rp.PermissionAction?.PermissionCategory)
                .Select(categoryGroup => new PermissionCategoryClaim
                {
                    Name = categoryGroup.Key?.Name,
                    Value = categoryGroup.Key.Value,
                    Actions = categoryGroup
                        .Select(rp => new PermissionActionClaim
                        {
                            Name = rp.PermissionAction.Name,
                            Value = rp.PermissionAction.Value
                        })
                        .ToList()
                })
                .ToList();

            return new PermissionsClaim()
            {
                PermissionCategoryClaims = permissionCategories
            };
        }
    }
}
