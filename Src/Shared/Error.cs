namespace Anazon.Shared;

public record Error
{
    public string Code { get; init; }
    public string Message { get; init; }
    protected internal Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public static readonly Error None = new(string.Empty, string.Empty);
    public static Error Create<TDetails>(string code, string message, TDetails details) =>
        new Error<TDetails>(code, message, details);

    public static Error Create(string code, string message) =>
        new(code, message);


    public static Error<TDetails> Validation<TDetails>(TDetails details) =>
        new("ValidationError", "One or more validation errors occurred.", details);




    public static Error EmailAlreadyInUse =>
        new("EmailAlreadyInUse", "The provided email is already in use.");

    public static Error PhoneAlreadyInUse =>
        new("PhoneAlreadyInUse", "The provided phone number is already in use.");   
        
    public static Error InvalidCredentials =>
        new("InvalidCredentials", "Invalid Credentials.");   
        
    public static Error ExpiredOrInvalidRefreshToken =>
        new("ExpiredOrInvalidRefreshToken", "The refresh token is either expired or invalid");   
        
    public static Error UserIsDisabledOrDeleted =>
        new("UserIsDisabledOrDeleted", "User is either disabled or deleted");

    public static Error BrandNotFound =>
        new("BrandNotFound", "No brand found with the specified id.");   
        
    public static Error CategoryNotFound =>
        new("CategoryNotFound", "No Category found with the specified id.");   
        
    public static Error CategoryCantBeDeletedContainsProducts =>
        new("CategoryCantBeDeletedContainsProducts", "Category can't be deleted as it contains some products");   
        
    public static Error CategoryCantBeDeletedHasChildren =>
        new("CategoryCantBeDeletedHasChildren", "Category can't be deleted as it has some children");   
        
    public static Error CategoryInvalidParentId =>
        new("CategoryInvalidParentId", "Invalid ParentCategoryId");   
        
         
}   


public record Error<TDetails>(string Code, string Message, TDetails Details) : Error(Code, Message);