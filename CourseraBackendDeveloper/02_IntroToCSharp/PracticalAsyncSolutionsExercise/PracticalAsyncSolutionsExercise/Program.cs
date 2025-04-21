using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Product
{
    public string Name { get; set; }

    public Product(string name)
    {
        Name = name;
    }
}

public class Review
{
    public string Content { get; set; }

    public Review(string content)
    {
        Content = content;
    }
}

public class Program
{
    // Asynchronous method to fetch product data
    public async Task<List<Product>> FetchProductsAsync()
    {
        await Task.Delay(2000); // Simulating a 2-second delay for fetching products
        return new List<Product> { new Product("Eco Bag"), new Product("Reusable Straw") };
    }

    // Asynchronous method to fetch review data
    public async Task<List<Review>> FetchReviewsAsync()
    {
        await Task.Delay(3000); // Simulating a 3-second delay for fetching reviews
        return new List<Review>
        {
            new Review("Great product!"),
            new Review("Good value for the money."),
        };
    }

    // Asynchronous method to display both products and reviews
    public async Task FetchDataAsync()
    {
        // Start fetching products and reviews concurrently
        Task<List<Product>> productsTask = FetchProductsAsync();
        Task<List<Review>> reviewsTask = FetchReviewsAsync();

        // Wait for both tasks to complete
        await Task.WhenAll(productsTask, reviewsTask);

        // Get the results
        List<Product> products = await productsTask;
        List<Review> reviews = await reviewsTask;

        // Display the results
        Console.WriteLine("Products:");
        foreach (Product product in products)
        {
            Console.WriteLine(product.Name);
        }

        // Display fetched reviews
        Console.WriteLine("\nReviews:");
        foreach (Review review in reviews)
        {
            Console.WriteLine(review.Content);
        }
    }

    // Main entry point
    public static async Task Main(string[] args)
    {
        // Calling the asynchronous method to fetch and display products and reviews
        Program program = new Program();
        await program.FetchDataAsync();
    }
}
