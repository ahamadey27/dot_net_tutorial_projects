var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var blogs = new List<Blog>
{
    new Blog {Title = "My first post", Body = "This is my first post"},
    new Blog {Title = "My second post", Body = "This is my second post"}
};

app.MapGet("/", () => "Root domain");

app.MapGet("/blogs", () => { return blogs; });

app.MapGet("/blogs/{id}", (int id) =>
{
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NoContent();
    }
    else
    {
        return Results.Ok(blogs[id]);
    }
});

//Updates blog
app.MapPost("/blogs", (Blog blog) => {
    blogs.Add(blog);
    return Results.Created($"/blogs/{blogs.Count - 1}", blog);
    });

app.Run();
