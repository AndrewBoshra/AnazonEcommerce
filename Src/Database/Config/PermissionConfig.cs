using Anazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anazon.Database.Config;

public class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasData([
            new Permission(){ Id = 1 , Key = "User_Read"},
            new Permission(){ Id = 2 , Key = "User_Create"}
        ]);
    }
}