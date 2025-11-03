using Anazon.Configs;
using Anazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anazon.Database.Config;

public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    { 
        //Admin
        builder.HasData([
            new RolePermission(){ Id = 1 , RoleId = 1, PermissionId = 1 },
            new RolePermission(){ Id = 2 , RoleId = 1, PermissionId = 2 },
            new RolePermission(){ Id = 3 , RoleId = 1, PermissionId = 3 },
            new RolePermission(){ Id = 4 , RoleId = 1, PermissionId = 4 },
            new RolePermission(){ Id = 5 , RoleId = 1, PermissionId = 5 },
        ]);

        builder.HasData([
            new RolePermission(){ Id = 6 ,  RoleId = 1, PermissionId = 6  },
            new RolePermission(){ Id = 7 ,  RoleId = 1, PermissionId = 7  },
            new RolePermission(){ Id = 8 ,  RoleId = 1, PermissionId = 8  },
            new RolePermission(){ Id = 9 ,  RoleId = 1, PermissionId = 9  },
            new RolePermission(){ Id = 10 , RoleId = 1, PermissionId = 10 },
        ]);



        //Anonymous
        builder.HasData([
            new RolePermission(){ Id = 1001 , RoleId = 3, PermissionId = 1 },
            new RolePermission(){ Id = 1002 , RoleId = 3, PermissionId = 2 },
            new RolePermission(){ Id = 1003 , RoleId = 3, PermissionId = 6 },
            new RolePermission(){ Id = 1004 , RoleId = 3, PermissionId = 7 },
        ]);
    }
}