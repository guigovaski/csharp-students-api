using System.Net;
using System.Text.Json;

namespace StudentsApi.Exceptions;

public class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ApiExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var responseModel = ApiResponse<string>.Fail(e.Message);

            switch (e)
            {
                case ApiException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            await response.WriteAsJsonAsync(responseModel);
        }
    }
}
