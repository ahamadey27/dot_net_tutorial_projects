// Inventory Manager Console App
// This application allows users to add, restock, update, and view product inventory.
// It uses a Product class and a List<Product> to manage data.

using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

class InventoryManager
{
    static List<Product> inventory = new List<Product>();

    static void Main(string[] args)
    {
        // Main menu loop to handle user choices
        while (true)
        {
            Console.WriteLine("\nWelcome to Cinco's Inventory Manager System.");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Restock Inventory");
            Console.WriteLine("3. Update Inventory");
            Console.WriteLine("4. View All Products");
            Console.WriteLine("5. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    RestockInventory();
                    break;
                case "3":
                    UpdateInventory();
                    break;
                case "4":
                    ViewInventory();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void AddProduct()
    {
        // Adds a new product to the inventory with default quantity 0
        Console.Write("Enter product name: ");
        string name = Console.ReadLine();

        Console.Write("Enter product price: ");
        decimal price;
        while (!decimal.TryParse(Console.ReadLine(), out price))
        {
            Console.Write("Invalid price. Enter a valid number: ");
        }

        inventory.Add(new Product { Name = name, Price = price, Quantity = 0 });
        Console.WriteLine("Product added with initial quantity 0.");
    }

    static void RestockInventory()
    {
        // Finds a product by name and adds quantity to its inventory
        Console.Write("Enter product name to restock: ");
        string name = Console.ReadLine();

        Console.Write("Enter quantity to add: ");
        int quantity;
        while (!int.TryParse(Console.ReadLine(), out quantity))
        {
            Console.Write("Invalid quantity. Enter a valid integer: ");
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                inventory[i].Quantity += quantity;
                Console.WriteLine($"{quantity} units added to {name}.");
                return;
            }
        }

        Console.WriteLine("Product not found.");
    }

    static void UpdateInventory()
    {
        // Allows user to set a new quantity for a specific product
        Console.Write("Enter product name to update: ");
        string name = Console.ReadLine();

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Current quantity: {inventory[i].Quantity}");
                Console.Write("Enter new quantity: ");

                int newQty;
                while (!int.TryParse(Console.ReadLine(), out newQty))
                {
                    Console.Write("Invalid input. Enter a valid integer: ");
                }

                inventory[i].Quantity = newQty;
                Console.WriteLine($"Inventory for {name} updated to {newQty}.");
                return;
            }
        }

        Console.WriteLine("Product not found.");
    }

    static void ViewInventory()
    {
        // Displays all products with their name, price, and quantity
        if (inventory.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        Console.WriteLine("\nCurrent Inventory:");
        foreach (var product in inventory)
        {
            Console.WriteLine($"Product: {product.Name}, Price: {product.Price:C}, Quantity: {product.Quantity}");
        }
    }
}
