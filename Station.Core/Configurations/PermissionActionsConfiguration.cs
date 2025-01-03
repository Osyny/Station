using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Station.Core.Entities.Identities;
using Station.Core.Enums;
using Station.Core.Helpers.SelectList;
using Station.Web.Host.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Core.Configurations
{
    internal sealed class PermissionActionsConfiguration : IEntityTypeConfiguration<PermissionAction>
    {
        public void Configure(EntityTypeBuilder<PermissionAction> builder)
        {
            int index = 0;
            foreach (var permissionCategory in GetPermissionCategory())
            {

                IEnumerable<PermissionAction> permissionActions
                  = Enum.GetValues<PermissionActionEnum>().Select(action => new PermissionAction
                  {
                      Id = (int)action + index,
                      Name = action.GetDisplayValue(),
                      Description = action.GetDisplayValue(),
                      Value = action,
                      PermissionCategoryId = permissionCategory.Id
                  });
                builder.HasData(permissionActions);

                index = permissionActions.Count() + 1;
            }
           
        }

        private IEnumerable<PermissionCategory> GetPermissionCategory()
        {
            IEnumerable<PermissionCategory> permissionCategories = Enum.GetValues<PermissionCategoryEnum>().Select(r => new PermissionCategory
            {
                Id = (int)r,
                Name = r.GetDisplayValue(),
                Description = r.GetDisplayValue(),
            });
            return permissionCategories;
        }

    }
}
