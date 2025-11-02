using Anazon.Configs;
using Anazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anazon.Database.Config;

public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasData([
            new RolePermission(){ Id = 1 , RoleId = 1, PermissionId = 1 },
            new RolePermission(){ Id = 2 , RoleId = 1, PermissionId = 2 },
            new RolePermission(){ Id = 3 , RoleId = 1, PermissionId = 3 },
            new RolePermission(){ Id = 4 , RoleId = 1, PermissionId = 4 },
            new RolePermission(){ Id = 5 , RoleId = 1, PermissionId = 5 },
        ]);
    }
}