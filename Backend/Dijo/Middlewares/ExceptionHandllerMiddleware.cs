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
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    errorCode = (int)HttpStatusCode.InternalServerError,
                    ErrorMessage = "Something went wrong! We are looking into resolving this"
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
