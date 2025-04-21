using System.Text.Json; // Importing the System.Text.Json namespace for JSON serialization
using Microsoft.AspNetCore.Identity; // Importing ASP.NET Core Identity namespace

var builder = WebApplication.CreateBuilder(args);

// Configuring the default JSON serialization options for the application
builder.Services.ConfigureHttpJsonOptions(options =>
{
    // Setting the default property naming policy to KebabCaseUpper for JSON serialization
    // This means property names will be serialized in kebab-case with uppercase letters
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.KebabCaseUpper;
});

var app = builder.Build();

// Sample object to demonstrate serialization
var samplePerson = new Person { UserName = "Alice", UserAge = 30 };

// Basic route to return a simple string
app.MapGet("/", () => "I am Root");

// Route to manually serialize the object using the default JSON serializer
app.MapGet("/manual-json", () =>
{
    // Serializing the samplePerson object to JSON using the default options
    var jsonString = JsonSerializer.Serialize(samplePerson);
    // Returning the serialized JSON string with the appropriate content type
    return TypedResults.Text(jsonString, "application/json");
});

// Route to demonstrate custom serialization with a different naming policy
app.MapGet("/custom-serializer", () =>
{
    // Creating custom JSON serialization options with SnakeCaseLower naming policy
    var options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    // Serializing the samplePerson object using the custom options
    var customJsonString = JsonSerializer.Serialize(samplePerson, options);
    // Returning the custom serialized JSON string with the appropriate content type
    return TypedResults.Text(customJsonString, "application/json");
});

// Route to use the default automatic .NET serialization for JSON
app.MapGet("/json", () =>
{
    // Returning the samplePerson object as JSON using TypedResults.Json
    // This uses the default serialization options configured earlier
    return TypedResults.Json(samplePerson);
});

// Route to demonstrate automatic serialization by returning the object directly
app.MapGet("/auto", () =>
{
    // Returning the samplePerson object directly
    // .NET automatically serializes the object to JSON using the default options
    return samplePerson;
});

app.Run();

// Definition of the Person class
// This class is used as the object to be serialized in the above routes
public class Person
{
    // Required property for the user's name
    required public string UserName { get; set; }
    // Required property for the user's age
    required public int UserAge { get; set; }
}
