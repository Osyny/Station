using Microsoft.EntityFrameworkCore.Metadata;
using Station.Core.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Station.Core.Enums;
using Station.Web.Host.Extentions;

namespace Station.Core.Configurations
{
    internal sealed class PermissionCategoriesConfiguration : IEntityTypeConfiguration<PermissionCategory>
    {
        public void Configure(EntityTypeBuilder<PermissionCategory> builder)
        {
            IEnumerable<PermissionCategory> permissionCategories 
                = Enum.GetValues<PermissionCategoryEnum>().Select(cat => new PermissionCategory
                {
                Id = (int)cat,
                Name = cat.GetDisplayValue(),
                Description = cat.GetDisplayValue(),
                Value = cat
            });
            builder.HasData(permissionCategories);
        }
    }
}
