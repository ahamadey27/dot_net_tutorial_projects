using System.Xml.Serialization;

class TaskManager
{
    static List<string> tasks = new List<string>(); //actual task descriptions
    static List<bool> taskStatus = new List<bool>(); //false pending tasks and true completed tasks

    static void Main (string[] args)
    {
        while (true)
        {
            Console.WriteLine("Task Manager");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Mark Task as Complete");
            Console.WriteLine("3. View Task");
            Console.WriteLine("4. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    CompleteTask();
                    break;
                case "3":
                    ViewTask();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Coice");
                    break;
            }
        }
    }

    static void AddTask()
    {
        Console.WriteLine("Enter Task Description:");
        string task = Console.ReadLine();   
        tasks.Add(task); //add to task list
        taskStatus.Add(false); //new task is not completed
        Console.WriteLine("Task successfuly added");
    }

    static void CompleteTask()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("Enter task number to mark as complete");
            return;
        }

        Console.WriteLine("Enter task to be marked as completed");

        if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
        {
            taskStatus[taskNumber - 1] = true;
            Console.WriteLine($"Task '{tasks[taskNumber - 1]} marked as complete");
        }

        else
        {
            Console.WriteLine("Invalid task number");
        }
    }

    static void ViewTask()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available");
            return;
        }

        Console.WriteLine("Tasks:");
        for (int i = 0; i < tasks.Count; i++)
        {
            string status = taskStatus[i] ? "Comlpeted" : "Pending";
            Console.WriteLine($"{i + 1}. {tasks[i]} - {status}");
        }

    }


}