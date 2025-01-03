using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Station.Core;
using Station.Core.Entities.Identities;
using Station.Core.Enums;
using Station.Core.Helpers.SelectList;
using Station.Web.Host.Extentions;
using Station.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace Station.Web.Seeds
{
    public static class DefaultRolesPermissions
    {

        public static void SeedRolePermissions(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetService<ApplicationDbContext>();     

            var roles = dbContext.Roles
                .AsNoTracking()
                .Include(p => p.RolePermissions).ToList();

            var rolePermissions = dbContext.RolePermissions
               .AsNoTracking()
               .Include(r => r.PermissionAction).ToList();

            var permissionActions = dbContext.PermissionActions
               .AsNoTracking()
               .Include(r => r.PermissionCategory).ToList();

            SeedRolePermission(dbContext, roles, rolePermissions, permissionActions);

        }

        private static void SeedUserRolePermission(ApplicationDbContext context, List<Role> roles, List<RolePermission> rolePermissions)
        {

            var roleUser = roles.FirstOrDefault(r => r.Name == RoleEnum.User.GetDisplayValue());

            var permissionActiontems = ItemsHelpers.GetPermissionActionItems();
            // User
            var resRolePermissions = new List<RolePermission>();

            foreach (var permissionAction in ItemsHelpers.GetPermissionActionItems())
            {
                if (!permissionAction.Equals(PermissionActionEnum.View))
                {
                    continue;
                }
                if (!rolePermissions.Any(r => r.PermissionAction.Name == permissionAction.GetDisplayValue() && r.RoleId == roleUser.Id))
                {

                    var userRolePermissions = new List<RolePermission>()
                    {
                        new RolePermission()
                        {
                            RoleId = roleUser.Id,

                            PermissionAction = new PermissionAction
                            {
                                Description = "",
                                Name = permissionAction.GetDisplayValue(),
                                Value = permissionAction,
                                PermissionCategory = new PermissionCategory()
                                {
                                    Description = "",
                                    Name = PermissionCategoryEnum.ChargeStation.GetDisplayValue(),
                                    Value = PermissionCategoryEnum.ChargeStation
                                }
                            },

                        },


                    };
                    resRolePermissions.AddRange(userRolePermissions);


                }
            }

            if (resRolePermissions.Count > 0)
            {
                context.RolePermissions.AddRangeAsync(resRolePermissions);
                context.SaveChangesAsync();
            }

        }

        private static void SeedRolePermission(ApplicationDbContext context, List<Role> roles, List<RolePermission> rolePermissions, List<PermissionAction> permissionActions)
        {
            var roleAdmin = roles.FirstOrDefault(r => r.Name == RoleEnum.Admin.GetDisplayValue());

            var permissionActiontems = ItemsHelpers.GetPermissionActionItems();
            // Admin
            var resRolePermissions = new List<RolePermission>();
            foreach (var permissionAction in permissionActions)
            {

                if (!rolePermissions.Any(r => r.PermissionAction?.Id == permissionAction.Id && r.RoleId == roleAdmin?.Id))
                {
                    var adminRolePermissions = new List<RolePermission>()
                    {
                        new RolePermission()
                        {
                            RoleId = roleAdmin.Id,
                            PermissionActionId = permissionAction.Id,

                        },                         
                    };
                    resRolePermissions.AddRange(adminRolePermissions);
                }
            }


            var roleUser = roles.FirstOrDefault(r => r.Name == RoleEnum.User.GetDisplayValue());

            // User
            foreach (var permissionAction in permissionActions)
            {
                if (!rolePermissions.Any(r => r.PermissionAction?.Id == permissionAction.Id && r.RoleId == roleUser?.Id))
                {

                    if (!isUserPermissionAction(permissionAction))
                    {
                        continue;
                    }
                    var userRolePermissions = new List<RolePermission>()
                    {
                        new RolePermission()
                        {
                            RoleId = roleUser.Id,
                            PermissionActionId = permissionAction.Id,
                        }
                    };
                    resRolePermissions.AddRange(userRolePermissions);
                }
            }

            if (resRolePermissions.Count > 0)
            {
                context.RolePermissions.AddRangeAsync(resRolePermissions);
                context.SaveChangesAsync();
            }

            
        }

        private static bool isUserPermissionAction(PermissionAction permissionAction)
        {
            var result = permissionAction.Value.Equals(PermissionActionEnum.View) &&
                        permissionAction.PermissionCategory.Value == PermissionCategoryEnum.ChargeStation;
            return result;
        }

        private static void SeedDefaultAdminAndRoles(ApplicationDbContext context, List<Role> roles, List<RolePermission> rolePermissions)
        {

            var roleAdmin = roles.FirstOrDefault(r => r.Name == RoleEnum.Admin.GetDisplayValue());

            var resRolePermissions = new List<RolePermission>();
            if (roleAdmin != null)
            {
                var  hashadPasword = BCrypt.Net.BCrypt.EnhancedHashPassword("123Pa$$word!");
                var defaultAdmin = new User()
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    IsActive = true,
                    FirstName= "admin",
                    LastName  = "admin",
                    HashPasword = hashadPasword,

                    UserRoles = new List<UserRole>()
                    {
                        new UserRole()
                        {
                            RoleId = roleAdmin.Id, 
                        }
                     }   

                };
                context.Users.Add(defaultAdmin);
                context.SaveChangesAsync();

            }
    
        }
    }

}

