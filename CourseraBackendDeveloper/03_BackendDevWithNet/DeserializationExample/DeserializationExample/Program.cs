using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "I am Root!");

// Endpoint that uses automatic deserialization of the request body into a Person object
app.MapPost("/auto", (Person personFromClient) =>
{
    // The framework automatically deserializes the JSON payload into the Person object
    // Console.WriteLine(personFromClient); // Uncomment to debug and see the deserialized object
    // personFromClient.UserName = "Hammer"; // Example of modifying the deserialized object
    return TypedResults.Ok(personFromClient); // Return the deserialized object back to the client
});

// Endpoint that manually reads and deserializes JSON from the request body
app.MapPost("/json", async (HttpContext context) =>
{
    // Explicitly read and deserialize the JSON payload into a Person object
    var person = await context.Request.ReadFromJsonAsync<Person>();
    return TypedResults.Json(person); // Return the deserialized object as JSON
});

// Endpoint that uses custom deserialization options
app.MapPost("/custom-option", async (HttpContext context) =>
{
    var options = new JsonSerializerOptions
    {
        // Disallow unmapped members in the JSON payload to enforce strict deserialization
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
    };
    // Deserialize the JSON payload using the custom options
    var person = await context.Request.ReadFromJsonAsync<Person>(options);
    return TypedResults.Json(person); // Return the deserialized object as JSON
});

app.Run();

// The Person class defines the structure of the object to be deserialized
public class Person
{
    // The 'required' modifier ensures that this property must be provided during deserialization
    required public string UserName { get; set; }
    // Nullable property to allow optional deserialization of UserAge
    public int? UserAge { get; set; }
}
