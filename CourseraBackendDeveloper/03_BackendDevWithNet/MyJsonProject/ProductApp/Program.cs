using Newtonsoft.Json;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}

public class Program
{
    public static void Main()
    {
        Person newPerson = new Person
        {
            Name = "Jane Smith",
            Age = 25,
            Email = "jane.smith@example.com"
        };

        string serializedPerson = JsonConvert.SerializeObject(newPerson, Formatting.Indented);
        Console.WriteLine($"Serialized JSON:\n{serializedPerson}");

        string json = "{\"Name\": \"John Doe\", \"Age\": 30, \"Email\": \"john.doe@example.com\"}";

        Person person = JsonConvert.DeserializeObject<Person>(json);

        Console.WriteLine($"Name: {person.Name}");
        Console.WriteLine($"Age: {person.Age}");
        Console.WriteLine($"Email: {person.Email}");
        // Product newProduct = new Product
        // {
        //     Name = "Laptop",
        //     Price = 999.99m,
        //     Tags = new List<string> { "Electronics", "Computers" }
        // };

        // string json = JsonConvert.SerializeObject(newProduct, Formatting.Indented);
        // Console.WriteLine($"Serialized JSON:\n{json}");


        // string json = "{\"name\": \"Laptop\",\n\"price\": 999.99,\n\"tags\": [\"Electronics\", \"Computers\"]\n}";
        // Product product = JsonConvert.DeserializeObject<Product>(json);
        // Console.WriteLine{$"Product: {product.Name}, Price: {product.Price}, Tags:{} {string.Join(", ", product.Tags)}";
    }
}