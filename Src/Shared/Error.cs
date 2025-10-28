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
        
         
}   


public record Error<TDetails>(string Code, string Message, TDetails Details) : Error(Code, Message);