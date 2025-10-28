using Anazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anazon.Database.Config;

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData([
            new Role(){ Id = 1 , Key = "Admin", Name = "Admin" },
            new Role(){ Id = 2 , Key = "Customer", Name = "Customer" }
        ]);
    }
}