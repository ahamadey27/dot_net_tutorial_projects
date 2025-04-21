using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using NewApiSpace; // Ensure the namespace matches the one used in ClientGenerator.cs

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        app.MapControllers();

        app.Run(); // Start the server synchronously

        var httpClient = new HttpClient();
        var client = new CustomApiClient("http://localhost:5228", httpClient); // Ensure the class name matches the one used in ClientGenerator.cs
        var user = await client.GetUserAsync(1);
        Console.WriteLine($"Fetched User: {user}");
    }
}