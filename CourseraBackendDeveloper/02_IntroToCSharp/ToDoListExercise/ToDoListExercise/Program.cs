using System;
using System.Diagnostics;

class ToDoList
{
    string[] tasks = new string[10];
    int taskCount;

    void AddTask()
    {
        if (taskCount >= tasks.Length)
        {
            Console.WriteLine("Task list is full. Cannot add more tasks.");
            return;
        }

        Console.WriteLine("Please enter a task:");
        tasks[taskCount] = Console.ReadLine();
        taskCount++;
        Console.WriteLine("Task added successfully.");
    }

    void ViewTasks()
    {
        if (taskCount == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        Console.WriteLine("Tasks:");
        for (int i = 0; i < taskCount; i++)
        {
            Console.WriteLine($"{i + 1}. {tasks[i]}");
        }

        Console.WriteLine("Select a task number to mark as complete:");
        if (int.TryParse(Console.ReadLine(), out int taskComplete) && taskComplete > 0 && taskComplete <= taskCount)
        {
            tasks[taskComplete - 1] = "Task Complete";
            Console.WriteLine("Task marked as complete.");
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }

        Debug.WriteLine($"Dubug {taskCount}");
    }

    void Run()
    {
        while (true)
        {
            Console.WriteLine("Choose an option: 1. Add Task 2. View Tasks 3. Exit");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                AddTask();
            }
            else if (choice == "2")
            {
                ViewTasks();
            }
            else if (choice == "3")
            {
                Console.WriteLine("Exiting...");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }
    }

    static void Main(string[] args)
    {
        ToDoList app = new ToDoList();
        app.Run();
    }
}
