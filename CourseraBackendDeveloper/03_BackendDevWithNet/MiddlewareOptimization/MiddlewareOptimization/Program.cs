var builder = WebApplication.CreateBuilder(args);

// Configure to listen on HTTP only for simplicity
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5194); // HTTP Only
});

var app = builder.Build();

// Middleware for Simulated HTTPS Enforcement
app.Use(async (context, next) =>
{
    // Check if the 'secure' query parameter is set to 'true'
    if (context.Request.Query["secure"] != "true")
    {
        context.Response.StatusCode = 400; // Block request as if it were non-HTTPS
        await context.Response.WriteAsync("Simulated HTTPS required");
        Console.WriteLine("Security Event: HTTPS enforcement failed."); // Log security event
        return; // Short-circuit further processing
    }
    await next();
});

// Middleware for Input Validation
app.Use(async (context, next) =>
{
    var input = context.Request.Query["input"];
    // Validate and sanitize input
    if (!IsValidInput(input))
    {
        context.Response.StatusCode = 400; // Block request for invalid input
        await context.Response.WriteAsync("Invalid Input");
        Console.WriteLine("Security Event: Invalid input detected."); // Log security event
        return; // Short-circuit further processing
    }
    await next();
});

// Helper method for input validation
static bool IsValidInput(string input)
{
    return string.IsNullOrEmpty(input) || (input.All(char.IsLetterOrDigit) && !input.Contains("<script>"));
}

// Middleware for Unauthorized Access Handling
app.Use(async (context, next) =>
{
    // Check if the request path is '/unauthorized'
    if (context.Request.Path == "/unauthorized")
    {
        context.Response.StatusCode = 401; // Block unauthorized access
        await context.Response.WriteAsync("Unauthorized Access");
        Console.WriteLine("Security Event: Unauthorized access attempt."); // Log security event
        return; // Short-circuit further processing
    }
    await next();
});

// Middleware for Authentication Checks
app.Use(async (context, next) =>
{
    // Check if the 'authenticated' query parameter is true
    var isAuthenticated = context.Request.Query["authenticated"] == "true";
    if (!isAuthenticated)
    {
        context.Response.StatusCode = 403; // Restrict access for unauthenticated users
        await context.Response.WriteAsync("Access Denied");
        Console.WriteLine("Security Event: Authentication failed."); // Log security event
        return; // Short-circuit further processing
    }
    // Add secure cookies for authenticated users
    context.Response.Cookies.Append("SecureCookies", "Securedata", new CookieOptions
    {
        HttpOnly = true,
        Secure = true
    });
    await next();
});

// Middleware for Asynchronous Processing
app.Use(async (context, next) =>
{
    // Simulate asynchronous processing
    await Task.Delay(100);
    await context.Response.WriteAsync("Processed Asynchronously\n");
    await next();
});

app.Run();