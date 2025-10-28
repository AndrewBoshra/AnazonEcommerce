using Microsoft.AspNetCore.Mvc;

namespace Anazon.Shared;


public static class ResultExtension
{
    public static T Match<T>(this Result result, Func<T> onSuccess, Func<Error, T> onFailure)
    {
        if (result.IsSuccess)
        {
            return onSuccess();
        }
        else
        {
            return onFailure(result.Error);
        }
    }



    public static IResult ToSuccessHttpResult<T>(this Result<T> result)
    {
        return result.Match(
            onSuccess: () => Results.Ok(result.Value),
            onFailure: error => throw new InvalidOperationException("Cannot convert a failure result to a success HTTP result.")
        );
    }
    public static IResult ToSuccessHttpResult(this Result result)
    {
        return result.Match(
            onSuccess: () => Results.Ok(),
            onFailure: error => throw new InvalidOperationException("Cannot convert a failure result to a success HTTP result.")
        );
    }


    public static IResult ToBadRequestHttpResult<T>(this Result<T> result)
    {
        return result.Match(
            onSuccess: () => throw new InvalidOperationException("Cannot convert a success result to a bad request HTTP result."),
            onFailure: error => Results.BadRequest(
                new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = error.Message,
                    Status = 400,
                    Extensions = { { "ErrorCode", error.Code } }
                }
            )
        );
    }

    /* 
        <summary>
        Converts a Result (non-generic) to an IResult HTTP response.
        Either returns 200 OK for success or 400 Bad Request for failure.
        </summary>
    */
    public static IResult ToHttpResult<T>(this Result<T> result)
    {
        return result.Match(
            onSuccess: () => result.ToSuccessHttpResult(),
            onFailure: _ => result.ToBadRequestHttpResult()
        );
    }
}