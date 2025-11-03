
namespace Anazon.Configs;


public static partial class Roles
{

    public const string Anonymous = "Anonymous";
    public const string Admin = "Admin";
    public const string Customer = "Customer";


    public static string DefaultRole => Customer;
}


