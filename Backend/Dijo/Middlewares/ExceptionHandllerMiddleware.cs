using RestaurantAPI.Exceptions;
using System.Net;

namespace RestaurantAPI.Middlewares
{
    public class ExceptionHandllerMiddleware
    {
        private readonly ILogger<ExceptionHandllerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandllerMiddleware(ILogger<ExceptionHandllerMiddleware> logger,
            RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                //Log this excpetion
                logger.LogError(ex, $"{errorId} : {ex.Message}");

                //Return a Custom Error Response
                
                httpContext.Response.ContentType = "application/json";

                object errorResponse;
                int statusCode;

                switch (ex)
                {
                    case UnsupportedFileExtensionException unsupportedFileEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse = new
                    {
                        id = errorId,
                        ErrorCode = statusCode,
                        ErrorMessage = unsupportedFileEx
                    };
                    break;

                    case ArgumentException argEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse = new
                    {
                        Id = errorId,
                        ErrorCode = statusCode,
                        ErrorMessage = argEx.Message
                    };
                    break;

                    case KeyNotFoundException notFoundEx:
                    statusCode = (int)HttpStatusCode.NotFound;
                    errorResponse = new
                    {
                        Id = errorId,
                        ErrorCode = statusCode,
                        ErrorMessage = notFoundEx.Message
                    };
                    break;

                    case FileSizeExceededException fileSizeExceededException:
                    statusCode = StatusCodes.Status413PayloadTooLarge;
                    errorResponse = new
                    {
                        Id = errorId,
                        ErrorCode = statusCode,
                        ErrorMessage = fileSizeExceededException.Message
                    };
                    break;

                    default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse = new
                    {
                        Id = errorId,
                        ErrorCode = statusCode,
                        ErrorMessage = "Something went wrong! We are looking into resolving this."
                    };
                    break;
                }

                httpContext.Response.StatusCode = statusCode;
                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
