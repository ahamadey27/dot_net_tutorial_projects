using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Enable Logging
builder.Services.AddLogging();
builder.Services.AddSingleton<Dictionary<int, User>>(new Dictionary<int, User>
{
    { 1, new User { Id = 1, Name = "Alice", Email = "alice@example.com" } },
    { 2, new User { Id = 2, Name = "Bob", Email = "bob@example.com" } }
});

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "YourIssuer",
            ValidAudience = "YourAudience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKey"))
        };
    });

// **Fix: Register Authorization Services**
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// **Middleware for Request-Response Logging**
app.UseMiddleware<RequestResponseLoggingMiddleware>();

// **Middleware for JWT Token Validation**
app.UseMiddleware<JwtTokenValidationMiddleware>();

// **Global Exception Handling Middleware**
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// **Secure API Endpoints with Authentication & Authorization**
app.UseAuthentication();
app.UseAuthorization();

// **Define CRUD Endpoints (Secured)**
app.MapGet("/users", (Dictionary<int, User> users, ILogger<Program> logger) =>
{
    return Results.Ok(users.Values.ToList());
}).RequireAuthorization();

app.MapGet("/users/{id}", (int id, Dictionary<int, User> users, ILogger<Program> logger) =>
{
    return users.TryGetValue(id, out var user)
        ? Results.Ok(user)
        : Results.NotFound(new { error = $"User with ID {id} not found." });
}).RequireAuthorization();

app.MapPost("/users", (User user, Dictionary<int, User> users, ILogger<Program> logger) =>
{
    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(user);
    Validator.TryValidateObject(user, validationContext, validationResults, true);

    var errorMessages = validationResults.Select(r => r.ErrorMessage).ToList();
    if (errorMessages.Any())
        return Results.BadRequest(new { errors = errorMessages });

    user.Id = users.Keys.DefaultIfEmpty(0).Max() + 1;
    users[user.Id] = user;
    return Results.Created($"/users/{user.Id}", user);
}).RequireAuthorization();

app.MapPut("/users/{id}", (int id, User updatedUser, Dictionary<int, User> users, ILogger<Program> logger) =>
{
    if (!users.ContainsKey(id)) return Results.NotFound(new { error = $"User with ID {id} not found." });

    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(updatedUser);
    Validator.TryValidateObject(updatedUser, validationContext, validationResults, true);

    var errorMessages = validationResults.Select(r => r.ErrorMessage).ToList();
    if (errorMessages.Any())
        return Results.BadRequest(new { errors = errorMessages });

    updatedUser.Id = id;
    users[id] = updatedUser;
    return Results.Ok(updatedUser);
}).RequireAuthorization();

app.MapDelete("/users/{id}", (int id, Dictionary<int, User> users, ILogger<Program> logger) =>
{
    if (!users.ContainsKey(id)) return Results.NotFound(new { error = $"User with ID {id} not found." });

    users.Remove(id);
    return Results.NoContent();
}).RequireAuthorization();

app.Run();

// **Middleware for Request-Response Logging**
public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation($"Incoming Request: {context.Request.Method} {context.Request.Path}");

        var originalResponseBody = context.Response.Body;
        using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        await _next(context);

        _logger.LogInformation($"Outgoing Response: {context.Response.StatusCode}");

        responseBodyStream.Seek(0, SeekOrigin.Begin);
        await responseBodyStream.CopyToAsync(originalResponseBody);
        context.Response.Body = originalResponseBody;
    }
}

// **Middleware for JWT Token Validation**
public class JwtTokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<JwtTokenValidationMiddleware> _logger;

    public JwtTokenValidationMiddleware(RequestDelegate next, ILogger<JwtTokenValidationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        context.Response.ContentType = "application/json"; // Ensure proper response format

        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { error = "Unauthorized: No token provided." });
            return;
        }

        try
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "YourIssuer",
                ValidAudience = "YourAudience",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKey"))
            };

            tokenHandler.ValidateToken(token, validationParameters, out _);
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Invalid token detected.");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { error = "Unauthorized: Invalid token." });
        }
    }
}

// **Global Exception Handling Middleware**
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { error = "Internal server error." });
        }
    }
}

// **User Class**
class User
{
    public int Id { get; set; }

    [Required, MinLength(2)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, CustomEmailValidation]
    public string Email { get; set; } = string.Empty;
}

// **Custom Email Validation**
class CustomEmailValidation : ValidationAttribute
{
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        return value is string email && EmailRegex.IsMatch(email)
            ? ValidationResult.Success
            : new ValidationResult("Invalid email format.");
    }
}