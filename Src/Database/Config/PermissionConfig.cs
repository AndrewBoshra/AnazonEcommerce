using Anazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anazon.Database.Config;

public class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {

        var permissionsType = typeof(Configs.Permissions);

        var nestedStaticTypes = permissionsType.GetNestedTypes(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)
            .Where(t => t.IsAbstract && t.IsSealed); // static classes

        var stringFields = nestedStaticTypes
            .SelectMany(t => t.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy)
                              .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string)));

        var permissionNames = stringFields
            .Select(f => f.GetRawConstantValue() as string)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Distinct()
            .ToArray();

        // adjust initialization as needed to match your Permission model
        var permissionSeed = permissionNames
            .Select((key, index) => new Permission { Id = index + 1, Key = key! })
            .ToArray();

        builder.HasData(permissionSeed);
    }
}