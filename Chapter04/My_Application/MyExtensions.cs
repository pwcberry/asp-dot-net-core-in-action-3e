namespace MyApplication
{
    public static class MyExtensions
    {
        public static void SetupBasicMiddleware(this WebApplication app)
        {
            app.UseStaticFiles();
            app.UseWelcomePage("/blah");
            app.MapGet("/", static () => Results.File("~/index.html", "text/html"));
        }

        public static void SetupMinimalApi(this WebApplication app)
        {
            app.UseDeveloperExceptionPage(); // can be omitted, added by WebApplication by default
            app.UseStaticFiles();
            app.UseRouting();

            app.MapGet("/", () => Results.Ok("Hello World!"));
        }

        public static void SetupDeveloperExceptionHandling(this WebApplication app)
        {
            app.UseDeveloperExceptionPage();
            //app.UseExceptionHandler("/error");
            app.MapGet("/foo", () => Results.Problem("An error occurred."));
            app.MapGet("/bad", () => (new Service()).DoSomething());
            app.MapGet("/", static () => Results.File("~/index.html", "text/html"));
        }

        public static void SetupExceptionHandling(this WebApplication app)
        {
            app.UseExceptionHandler("/error");
            app.MapGet("/foo", () => Results.Problem("An error occurred.")); // Skips the ExceptionHandler middleware because it doesn't throw an exception
            app.MapGet("/bad", () => (new Service()).DoSomething());
            app.MapGet("/error", () => Results.File("~/error.html", "text/html"));
            app.MapGet("/", static () => Results.File("~/index.html", "text/html"));
        }
    }
}
