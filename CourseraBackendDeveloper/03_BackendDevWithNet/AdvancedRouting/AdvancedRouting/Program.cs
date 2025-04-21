var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/users/{userId}/posts/{slug}", (int userId, string slug) => { return $"User ID: {userId}, Post ID: {slug}"; }); //Static Route

app.MapGet("/products/{id:int:min(0)}", (int id) => { return $"Product ID: {id}"; }); //Route Constraint_esnures product ID is greater than 0

app.MapGet("/report/{year?}", (int? year = 2016) => { return $"Report for Year: {year}"; }); //Optional Parameter Example

app.MapGet("/search", (string? q, int page = 1) => { return $"Searching for {q} on page {page}"; }); //Query Paramaters. 'q' is for query string
//query parameters difined my search term...in this case q
app.Run();
