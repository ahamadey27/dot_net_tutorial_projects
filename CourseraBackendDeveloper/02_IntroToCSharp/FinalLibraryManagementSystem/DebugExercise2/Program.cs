using System;
using System.Collections.Generic;

class LibraryManager
{
    static void Main()
    {
        // Dictionary to store books and their status (true = checked out, false = available)
        Dictionary<string, bool> books = new Dictionary<string, bool>();
        // List to track books borrowed by the user
        List<string> borrowedBooks = new List<string>();
        const int maxLibraryCapacity = 5; // Maximum number of books the library can hold
        const int maxBorrowLimit = 3; // Maximum number of books a user can borrow

        while (true)
        {
            // Prompt user for an action
            Console.WriteLine("Choose an action: add, remove, borrow, return, search, status, or exit.");
            string action = Console.ReadLine().ToLower();

            // Handle user action
            switch (action)
            {
                case "add":
                    AddBook(books, maxLibraryCapacity);
                    break;
                case "remove":
                    RemoveBook(books);
                    break;
                case "borrow":
                    BorrowBook(books, borrowedBooks, maxBorrowLimit);
                    break;
                case "return":
                    ReturnBook(books, borrowedBooks);
                    break;
                case "search":
                    SearchBook(books);
                    break;
                case "status":
                    ChangeBookStatus(books);
                    break;
                case "exit":
                    return; // Exit the program
                default:
                    Console.WriteLine("Invalid action. Please choose: add, remove, borrow, return, search, status, or exit.");
                    break;
            }

            // Display the current state of the library and borrowed books
            DisplayLibraryBooks(books);
            DisplayBorrowedBooks(borrowedBooks);
        }
    }

    // Adds a new book to the library if capacity allows
    static void AddBook(Dictionary<string, bool> books, int maxCapacity)
    {
        if (books.Count >= maxCapacity)
        {
            Console.WriteLine("The library is full. No more books can be added.");
            return;
        }

        Console.WriteLine("Enter the title of the book to add:");
        string title = Console.ReadLine();

        if (books.ContainsKey(title))
        {
            Console.WriteLine("This book already exists in the library.");
        }
        else
        {
            books[title] = false; // Default status is available
            Console.WriteLine($"'{title}' has been added to the library.");
        }
    }

    // Removes a book from the library if it exists
    static void RemoveBook(Dictionary<string, bool> books)
    {
        if (books.Count == 0)
        {
            Console.WriteLine("The library is empty. No books to remove.");
            return;
        }

        Console.WriteLine("Enter the title of the book to remove:");
        string title = Console.ReadLine();

        if (books.Remove(title))
        {
            Console.WriteLine($"'{title}' has been removed from the library.");
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }

    // Allows the user to borrow a book if it is available and within the borrow limit
    static void BorrowBook(Dictionary<string, bool> books, List<string> borrowedBooks, int borrowLimit)
    {
        if (borrowedBooks.Count >= borrowLimit)
        {
            Console.WriteLine($"You have reached the borrowing limit of {borrowLimit} books.");
            return;
        }

        Console.WriteLine("Enter the title of the book to borrow:");
        string title = Console.ReadLine();

        if (books.TryGetValue(title, out bool status))
        {
            if (status)
            {
                Console.WriteLine($"The book '{title}' is already checked out.");
            }
            else
            {
                books[title] = true; // Mark as checked out
                borrowedBooks.Add(title);
                Console.WriteLine($"You have borrowed '{title}'.");
            }
        }
        else
        {
            Console.WriteLine("Book not found in the library.");
        }
    }

    // Allows the user to return a borrowed book
    static void ReturnBook(Dictionary<string, bool> books, List<string> borrowedBooks)
    {
        if (borrowedBooks.Count == 0)
        {
            Console.WriteLine("You have no borrowed books to return.");
            return;
        }

        Console.WriteLine("Enter the title of the book to return:");
        string title = Console.ReadLine();

        if (borrowedBooks.Remove(title))
        {
            books[title] = false; // Mark as available
            Console.WriteLine($"You have returned '{title}'.");
        }
        else
        {
            Console.WriteLine("Book not found in your borrowed list.");
        }
    }

    // Changes the status of a book (checked out or available)
    static void ChangeBookStatus(Dictionary<string, bool> books)
    {
        Console.WriteLine("Enter the title of the book to change its status:");
        string title = Console.ReadLine();

        if (books.TryGetValue(title, out bool status))
        {
            books[title] = !status; // Toggle status
            string newStatus = books[title] ? "checked out" : "available";
            Console.WriteLine($"The book '{title}' is now {newStatus}.");
        }
        else
        {
            Console.WriteLine("Book not found in the library.");
        }
    }

    // Searches for a book in the library and displays its status
    static void SearchBook(Dictionary<string, bool> books)
    {
        Console.WriteLine("Enter the title of the book to search for:");
        string title = Console.ReadLine();

        if (books.TryGetValue(title, out bool status))
        {
            string bookStatus = status ? "checked out" : "available";
            Console.WriteLine($"The book '{title}' is {bookStatus} in the library.");
        }
        else
        {
            Console.WriteLine($"The book '{title}' is not in the library.");
        }
    }

    // Displays all books in the library with their status
    static void DisplayLibraryBooks(Dictionary<string, bool> books)
    {
        Console.WriteLine("Library books:");
        if (books.Count == 0)
        {
            Console.WriteLine("No books in the library.");
            return;
        }

        int index = 1;
        foreach (var book in books)
        {
            string status = book.Value ? "Checked Out" : "Available";
            Console.WriteLine($"{index}. {book.Key} - {status}");
            index++;
        }
    }

    // Displays the list of books currently borrowed by the user
    static void DisplayBorrowedBooks(List<string> borrowedBooks)
    {
        Console.WriteLine("Your borrowed books:");
        if (borrowedBooks.Count == 0)
        {
            Console.WriteLine("No borrowed books.");
            return;
        }

        for (int i = 0; i < borrowedBooks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {borrowedBooks[i]}");
        }
    }
}