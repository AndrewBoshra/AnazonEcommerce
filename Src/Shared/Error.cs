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

    public static Error CategoryInvalidId =>
        new("CategoryInvalidId", "Invalid CategoryId");


    public static Error AttributeNotFound =>
        new("AttributeNotFound", "No Attribute found with the specified id.");
    
    public static Error AttributeCantBeDeletedContainsProducts =>
        new("AttributeCantBeDeletedContainsProducts", "Attribute can't be deleted as it contains some products");
    
    public static Error CategoryAttributeValuesAlreadyExist(List<string> values) =>
        new(
            "CategoryAttributeValuesAlreadyExist",
            "The following attribute values already exist: " + string.Join(", ", values)
        );
    

    public static Error AttributeValueNotFound =>
        new("AttributeValueNotFound", "No Attribute Value found with the specified value.");
    public static Error AttributeValueInUse =>
        new("AttributeValueInUse", "Attribute Value can't be deleted as it is in use by some products.");


    public static Error InvalidBrandId =>
        new("InvalidBrandId", "The specified BrandId is invalid.");
    public static Error InvalidCategoryId =>
        new("InvalidCategoryId", "The specified CategoryId is invalid.");


    public static Error TagsNotFound(List<string> missingTags) =>
        new(
            "TagsNotFound",
            "The following tags were not found: " + string.Join(", ", missingTags)
        );

    public static Error InvalidAttributeId(int attrId) => 
        new(
            "InvalidAttributeId",
            $"The attribute id '{attrId}' is invalid."
        );
    
    public static Error InvalidAttributeValue(int attrId, string attrValue) =>
        new(
            "InvalidAttributeValue",
            $"The attribute value '{attrValue}' is invalid for attribute '{attrId}'."
        );

}   


public record Error<TDetails>(string Code, string Message, TDetails Details) : Error(Code, Message);