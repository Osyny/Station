using Microsoft.EntityFrameworkCore.Migrations;
using Station.Core.Enums;
using Station.Web.Host.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station.Core.DefaultDatas
{
    public static class UpdateDefaultDataForMigrations
    {
        public static void AddAdmin_Up(MigrationBuilder migrationBuilder)
        {
            var hashadPasword = BCrypt.Net.BCrypt.EnhancedHashPassword("123Pa$$word!");
            migrationBuilder.Sql(
               $"INSERT INTO \"Users\"(\"Id\", \"Email\",\"UserName\",\"IsActive\",\"FirstName\",\"LastName\",\"HashPasword\") VALUES ('{1}', 'admin@gmail.com', 'admin@gmail.com', true, 'admin', 'admin', '{hashadPasword}')");

          
        }
        public static void AddRoles_Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
               $"INSERT INTO \"Roles\"(\"Id\", \"Name\",\"Description\") VALUES ('{1}', '{RoleEnum.Admin.GetDisplayValue()}', '{RoleEnum.Admin.GetDisplayValue()}')");

            migrationBuilder.Sql(
             $"INSERT INTO \"Roles\"(\"Id\", \"Name\",\"Description\") VALUES ('{1}', '{RoleEnum.User.GetDisplayValue()}', '{RoleEnum.User.GetDisplayValue()}')");
        }
    }
}
