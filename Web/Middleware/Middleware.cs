namespace Web.Middleware
{
    public static class Middleware
    {
        public static void UseMiddlewareFilter(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var userId = context.Session.GetString("userId");
                var path = context.Request.Path;
                if (!string.IsNullOrEmpty(userId))
                {
                    await next(context);
                }
                else
                {
                    if (path.StartsWithSegments("/Login") || path.StartsWithSegments("/Homepage") || path.StartsWithSegments("/"))
                    {
                        await next(context);
                    }
                    else
                    {
                        context.Response.Redirect("/Login");
                    }
                }
            });
        }
    }
}
