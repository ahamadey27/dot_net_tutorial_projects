using System;

namespace LibrarySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize book slots
            string[] books = new string[5];

            while (true)
            {
                Console.WriteLine("\nLibrary System");
                Console.WriteLine("1. Add a Book");
                Console.WriteLine("2. Remove a Book");
                Console.WriteLine("3. Display Books");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook(books);
                        break;
                    case "2":
                        RemoveBook(books);
                        break;
                    case "3":
                        DisplayBooks(books);
                        break;
                    case "4":
                        Console.WriteLine("Exiting the program. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddBook(string[] books)
        {
            Console.Write("Enter the title of the book to add: ");
            string newBook = Console.ReadLine();

            for (int i = 0; i < books.Length; i++)
            {
                if (string.IsNullOrEmpty(books[i]))
                {
                    books[i] = newBook;
                    Console.WriteLine($"Book '{newBook}' added to the library.");
                    return;
                }
            }

            Console.WriteLine("The library is full. Cannot add more books.");
        }

        static void RemoveBook(string[] books)
        {
            Console.Write("Enter the title of the book to remove: ");
            string bookToRemove = Console.ReadLine();

            for (int i = 0; i < books.Length; i++)
            {
                if (books[i] != null && books[i].Equals(bookToRemove, StringComparison.OrdinalIgnoreCase))
                {
                    books[i] = null;
                    Console.WriteLine($"Book '{bookToRemove}' removed from the library.");
                    return;
                }
            }

            Console.WriteLine($"Book '{bookToRemove}' not found in the library.");
        }

        static void DisplayBooks(string[] books)
        {
            Console.WriteLine("\nBooks currently in the library:");
            bool hasBooks = false;

            for (int i = 0; i < books.Length; i++)
            {
                if (!string.IsNullOrEmpty(books[i]))
                {
                    Console.WriteLine($"- {books[i]}");
                    hasBooks = true;
                }
            }

            if (!hasBooks)
            {
                Console.WriteLine("No books in the library.");
            }
        }
    }
}