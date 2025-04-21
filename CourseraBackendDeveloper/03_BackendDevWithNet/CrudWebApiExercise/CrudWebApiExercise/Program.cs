using Microsoft.AspNetCore.Builder; // Provides classes and methods to configure and build an ASP.NET Core application.
using Microsoft.AspNetCore.Http; // Provides HTTP-related classes for handling requests and responses.
using System.Collections.Generic; // Provides the List<T> class for managing collections.
using System.Linq; // Provides LINQ methods for querying collections.

// In-memory list to store items. This acts as a simple database for the application.
List<Item> items = new List<Item>();

// Create a WebApplication builder to configure the application.
var builder = WebApplication.CreateBuilder(args);

// Build the application from the builder configuration.
var app = builder.Build();

// Define the root route ("/") that returns a welcome message.
app.MapGet("/", () => "Welcome to the Simple Web API!");

// Define a route to GET all items (Read operation in CRUD).
// Returns the entire list of items.
app.MapGet("/items", () => items);

// Define a route to GET a specific item by its ID (Read operation in CRUD).
// The {id:int} constraint ensures the ID is an integer.
app.MapGet("/items/{id:int}", (int id) =>
{
    // Find the item with the matching ID in the list.
    var item = items.FirstOrDefault(i => i.Id == id);

    // If the item is found, return it with a 200 OK status.
    // Otherwise, return a 404 Not Found status.
    return item is not null ? Results.Ok(item) : Results.NotFound();
});

// Define a route to POST (create) a new item (Create operation in CRUD).
// The new item is passed in the request body.
app.MapPost("/items", (Item newItem) =>
{
    // Assign a unique ID to the new item.
    // If the list is empty, start with ID 1; otherwise, use the max ID + 1.
    newItem.Id = items.Count > 0 ? items.Max(i => i.Id) + 1 : 1;

    // Add the new item to the list.
    items.Add(newItem);

    // Return a 201 Created status with the location of the new item.
    return Results.Created($"/items/{newItem.Id}", newItem);
});

// Define a route to PUT (update) an existing item by its ID (Update operation in CRUD).
// The updated item is passed in the request body.
app.MapPut("/items/{id:int}", (int id, Item updatedItem) =>
{
    // Find the item with the matching ID in the list.
    var item = items.FirstOrDefault(i => i.Id == id);

    // If the item is not found, return a 404 Not Found status.
    if (item is null)
    {
        return Results.NotFound();
    }

    // Update the item's properties with the new values.
    item.Name = updatedItem.Name;
    item.Description = updatedItem.Description;
    item.Price = updatedItem.Price;

    // Return the updated item with a 200 OK status.
    return Results.Ok(item);
});

// Define a route to DELETE an item by its ID (Delete operation in CRUD).
app.MapDelete("/items/{id:int}", (int id) =>
{
    // Find the item with the matching ID in the list.
    var item = items.FirstOrDefault(i => i.Id == id);

    // If the item is not found, return a 404 Not Found status.
    if (item is null)
    {
        return Results.NotFound();
    }

    // Remove the item from the list.
    items.Remove(item);

    // Return a 204 No Content status to indicate successful deletion.
    return Results.NoContent();
});

// Start the application and listen for incoming HTTP requests.
app.Run();

// Define the Item class, which represents the structure of an item in the list.
public class Item
{
    public int Id { get; set; } // Unique identifier for the item.
    public string Name { get; set; } // Name of the item.
    public string Description { get; set; } // Description of the item.
    public decimal Price { get; set; } // Price of the item.
}