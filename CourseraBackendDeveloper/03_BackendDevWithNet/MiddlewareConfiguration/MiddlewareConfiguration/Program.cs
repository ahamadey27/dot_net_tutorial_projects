var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging((o) => { });
var app = builder.Build();

//These allow users login and authentication
app.UseRouting();
app.UseAuthentication();
app.UseAuthentication();

//Use exception handleling..only run when in development
app.UseExceptionHandler();

app.Use(async (context, next) =>
{
    Console.WriteLine("Logic Befor 1");
    await next.Invoke();
    Console.WriteLine("Logic after 2");
});


app.MapGet("/", () => "Hello World!");
app.MapGet("/hello", () => "This is the hello route");

//Use this middleware after code
//app.UseEndpoints();

app.Run();
