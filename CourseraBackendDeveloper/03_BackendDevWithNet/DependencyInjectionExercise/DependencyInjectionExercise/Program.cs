// Create a WebApplication builder, which is used to configure and build the application.
var builder = WebApplication.CreateBuilder(args);

// Register the IMyService interface with its implementation MyService in the dependency injection container.
// AddScoped means a new instance of MyService will be created for each HTTP request.
builder.Services.AddScoped<IMyService, MyService>();

// Build the application from the configured builder.
var app = builder.Build();

// Add the first middleware to the request pipeline.
// Middleware is a piece of code that can process HTTP requests and responses.
app.Use(async (context, next) =>
{
    // Retrieve an instance of IMyService from the dependency injection container.
    var myService = context.RequestServices.GetRequiredService<IMyService>();
    
    // Log a message using the service to demonstrate its usage.
    myService.LogCreation("First Middleware");
    
    // Call the next middleware in the pipeline.
    await next.Invoke();
});

// Add the second middleware to the request pipeline.
app.Use(async (context, next) =>
{
    // Retrieve an instance of IMyService from the dependency injection container.
    var myService = context.RequestServices.GetRequiredService<IMyService>();
    
    // Log a message using the service to demonstrate its usage.
    myService.LogCreation("Second Middleware");
    
    // Call the next middleware in the pipeline.
    await next.Invoke();
});

// Define an endpoint for the root URL ("/").
// This endpoint demonstrates dependency injection by directly injecting IMyService into the handler.
app.MapGet("/", (IMyService myService) =>
{
    // Log a message using the service to demonstrate its usage.
    myService.LogCreation("Root");
    
    // Return a response to the client.
    return Results.Ok("Check the console for service creation logs.");
});

// Start the application and begin listening for incoming HTTP requests.
app.Run();

// Define an interface for the service.
// This interface is used to define the contract that the service implementation must follow.
public interface IMyService
{
    // A method to log messages, which will be implemented by the service.
    void LogCreation(string message);
}

// Implement the IMyService interface in the MyService class.
public class MyService : IMyService
{
    // A unique identifier for the service instance, used to demonstrate service lifetime.
    private readonly int _serviceId;

    // Constructor for MyService.
    // A random number is generated to represent the unique ID of this service instance.
    public MyService()
    {
        _serviceId = new Random().Next(10000, 999999);
    }

    // Implementation of the LogCreation method from the IMyService interface.
    // Logs a message to the console, along with the unique service ID.
    public void LogCreation(string message)
    {
        Console.WriteLine($"{message} - Service ID: {_serviceId}");
    }
}
