using System.Security.Claims;

namespace OrderAPI.Middlewares
{
    public class OrderContextMiddleWare
    {
        private readonly RequestDelegate _request;

        public OrderContextMiddleWare(RequestDelegate request)
        {
            _request = request;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true &&
                (context.User.IsInRole("Customer")))
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (Guid.TryParse(userId, out Guid parsedUserId))
                {
                    context.Items["UserId"] = parsedUserId;
                }
            }
            await _request(context);
        }
    }
}
