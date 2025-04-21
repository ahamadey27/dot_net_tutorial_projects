var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Root Path ME");
app.MapGet("/Downloads", () => "Downloads ME");
app.MapPut("/", () => "This is a put");
app.MapDelete("/", () => "Delete ME");
app.MapPost("/", () => "Post ME");

app.Run();
