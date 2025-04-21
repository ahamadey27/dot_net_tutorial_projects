using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System;

var builder = WebApplication.CreateBuilder(args);

// Register services with different lifetimes
// Uncomment one at a time to test each scope

// AddSingleton: A single instance of the service is created and shared across the entire application lifetime.
// builder.Services.AddSingleton<IMyService, MyService>();

// AddScoped: A new instance of the service is created for each HTTP request and shared within that request.
// builder.Services.AddScoped<IMyService, MyService>();

// AddTransient: A new instance of the service is created every time it is requested.
builder.Services.AddTransient<IMyService, MyService>();

var app = builder.Build();

// Middleware to demonstrate lifecycle in multiple parts of the pipeline
app.Use(async (context, next) =>
{
    // Retrieve the service instance from the dependency injection container
    var myService = context.RequestServices.GetRequiredService<IMyService>();
    
    // Log the creation of the service in the first middleware
    myService.LogCreation("First Middleware"); 
    
    // Call the next middleware in the pipeline
    await next();
});

app.Use(async (context, next) =>
{
    // Retrieve the service instance from the dependency injection container
    var myService = context.RequestServices.GetRequiredService<IMyService>();
    
    // Log the creation of the service in the second middleware
    myService.LogCreation("Second Middleware"); 
    
    // Call the next middleware in the pipeline
    await next();
});

// Final endpoint to demonstrate service lifecycle in the request
app.MapGet("/", (IMyService myService) =>
{
    // Log the creation of the service in the endpoint
    myService.LogCreation("Root"); 
    
    // Return a response to the client
    return Results.Ok("Check the console for service creation logs.");
});

app.Run();

// Interface defining the contract for the service
public interface IMyService
{
    // Method to log the creation of the service
    void LogCreation(string message);
}

// Implementation of the service
public class MyService : IMyService
{
    private readonly int _serviceId; // Unique identifier for the service instance

    public MyService()
    {
        // Generate a random 6-digit number to represent the service instance
        _serviceId = new Random().Next(100000, 999999);
    }

    public void LogCreation(string message)
    {
        // Log the message along with the unique service ID
        Console.WriteLine($"{message} - Service ID: {_serviceId}");
    }
}