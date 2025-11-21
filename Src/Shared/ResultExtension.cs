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
            onFailure: error => throw new AppException("Cannot convert a failure result to a success HTTP result.")
        );
    }
    public static IResult ToSuccessHttpResult(this Result result)
    {
        return result.Match(
            onSuccess: () => Results.Ok(),
            onFailure: error => throw new AppException("Cannot convert a failure result to a success HTTP result.")
        );
    }


    public static IResult ToCreatedHttpResult(this Result result)
    {
        return result.Match(
            onSuccess: () => Results.Created(),
            onFailure: error => throw new AppException("Cannot convert a failure result to a Created HTTP result.")
        );
    }
    public static IResult ToCreatedHttpResult<T>(this Result<T> result)
    {
        return result.Match(
            onSuccess: () => Results.Created(string.Empty, result.Value),
            onFailure: error => throw new AppException("Cannot convert a failure result to a Created HTTP result.")
        );
    }


    public static IResult ToUnauthorized<T>(this Result<T> result)
    {
        return result.Match(
            onSuccess: () => throw new AppException("Cannot convert a success result to an Unauthorized request HTTP result."),
            onFailure: error => Results.Problem(
                new ProblemDetails
                {
                    Title = "UnAuthorized",
                    Detail = error.Message,
                    Status = StatusCodes.Status401Unauthorized,
                    Extensions = { { "ErrorCode", error.Code } }
                }
            )
        );
    }
    public static IResult ToBadRequestHttpResult(this Result result)
    {
        return result.Match(
            onSuccess: () => throw new AppException("Cannot convert a success result to a bad request HTTP result."),
            onFailure: error => Results.Problem(
                new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = error.Message,
                    Status = StatusCodes.Status400BadRequest,
                    Extensions = { { "ErrorCode", error.Code } }
                }
            )
        );
    }
    public static IResult ToBadRequestHttpResult<T>(this Result<T> result)
    {
        return result.Match(
            onSuccess: () => throw new AppException("Cannot convert a success result to a bad request HTTP result."),
            onFailure: error => Results.Problem(
                new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = error.Message,
                    Status = StatusCodes.Status400BadRequest,
                    Extensions = { { "ErrorCode", error.Code } }
                }
            )
        );
    }
    public static IResult ToNotFoundHttpResult<T>(this Result<T> result)
    {
        return result.Match(
            onSuccess: () => throw new AppException("Cannot convert a success result to a not found HTTP result."),
            onFailure: error => Results.Problem(
                new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = error.Message,
                    Status = StatusCodes.Status404NotFound,
                    Extensions = { { "ErrorCode", error.Code } }
                }
            )
        );
    }
    public static IResult ToNotFoundHttpResult(this Result result)
    {
        return result.Match(
            onSuccess: () => throw new AppException("Cannot convert a success result to a not found HTTP result."),
            onFailure: error => Results.Problem(
                new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = error.Message,
                    Status = StatusCodes.Status404NotFound,
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