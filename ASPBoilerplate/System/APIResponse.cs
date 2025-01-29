using System;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ASPBoilerplate;

public static class CustomResponse
{
    public static IResult Ok(object? data, string message = "Success")
    {
        var response = new
        {
            Success = true,
            Message = message,
            Data = data
        };
        return Results.Ok(response);
    }
    public static IResult CreatedAtRoute(string route, string Id, object? data, string message = "Success")
    {
        var response = new
        {
            Success = true,
            Message = message,
            Data = data
        };
        return Results.CreatedAtRoute("GetFileSingle", new { Id = Id }, response);
    }
    public static IResult BadRequest(string message = "Bad Request!")
    {
        var response = new
        {
            Success = false,
            Message = message
        };
        return Results.BadRequest(response);
    }
    public static IResult Unauthorized()
    {
        return Results.Unauthorized();
    }
}
