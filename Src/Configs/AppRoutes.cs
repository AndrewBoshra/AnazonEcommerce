namespace Anazon.Configs;
public static class AppRoutes
{
    public const string Base = "/api";

    public const string BaseAuth = Base + "/auth"; 
    public const string Brands = Base + "/brands"; 
    public const string Categories = Base + "/categories"; 
    public const string Attributes = Base + "/attributes"; 
    public const string AttributeValues = Base + "/attributes/{AttributeId:int}/values"; 
}
