// using System.Net.Http;
// using System.Runtime.InteropServices.Marshalling;
using System.Collections.Concurrent;

await new SwaggerClientGenerator().GenerateClient(); 
 

// Console.WriteLine("Hello");
// // Create an instance of HttpClient to make HTTP requests
// var httpClient = new HttpClient();
// var apiBasicUrl = "http://127.0.0.1:5500/"; // Base URL of the API

// try
// {
//     // Send a GET request to fetch blogs from the API
//     var httpResults = await httpClient.GetAsync($"{apiBasicUrl}/blogs");

//     // Check if the response status code is not OK (200)
//     if (httpResults.StatusCode != System.Net.HttpStatusCode.OK)
//     {
//         Console.WriteLine("Failed to fetch blogs"); // Log failure message
//         return; // Exit the program
//     }

//     // Read the response content as a stream
//     var blogStream = await httpResults.Content.ReadAsStreamAsync();

//     // Configure JSON deserialization options
//     var options = new System.Text.Json.JsonSerializerOptions
//     {
//         PropertyNameCaseInsensitive = true // Ignore case when matching property names
//     };

//     // Deserialize the JSON response into a list of Blog objects
//     var blogs = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Blog>>(blogStream, options);

//     // If blogs are successfully deserialized, iterate and display them
//     if (blogs != null)
//     {
//         foreach (var blog in blogs)
//         {
//             Console.WriteLine($"{blog.Title}: {blog.Body}"); // Print blog title and body
//         }
//     }
// }
// catch (HttpRequestException ex)
// {
//     Console.WriteLine($"An error occurred while making the HTTP request: {ex.Message}");
//     Console.WriteLine("Please ensure the API service is running and accessible.");
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"An unexpected error occurred: {ex.Message}");
// }

// // Define the Blog class to represent the structure of a blog
// class Blog
// {
//     public required string Title { get; set; } // Blog title
//     public required string Body { get; set; }  // Blog body/content
// }