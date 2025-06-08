using RestaurantAPI.API.Models.Domain;

namespace RestaurantAPI.Middlewares
{
    public class RestaurantContextMiddleware
    {
        private readonly RequestDelegate next;
        public RestaurantContextMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated &&
                (context.User.IsInRole("Admin") || context.User.IsInRole("Employee")))
            {
                var restaurantId = context.User.FindFirst("RestaurantId")?.Value;
                if (!string.IsNullOrEmpty(restaurantId))
                {
                    context.Items["RestaurantId"] = Guid.Parse(restaurantId);
                }
            }

            await next(context);
        }
    }
}
