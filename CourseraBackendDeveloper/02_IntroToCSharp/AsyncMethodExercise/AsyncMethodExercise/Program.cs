// See https://aka.ms/new-console-template for more information
public class Program
{
    public async Task DownloadDataAsync()
    {
        try
        {
            Console.WriteLine("Download data started...");
            throw new InvalidOperationException("Simulated Download Error.");
            await Task.Delay(3000);
            Console.WriteLine("Download 1 Complete");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
        
    }

    public async Task DownloadDataAsync2()
    {
        Console.WriteLine("Download data started...");
        await Task.Delay(2000);
        Console.WriteLine("Download 2 Complete");
    }

    public static async Task Main(string[] args)
    {
        Program program = new Program();
        Task task1 = program.DownloadDataAsync();
        Task task2 = program.DownloadDataAsync2();
        await Task.WhenAll(task1, task2);
        Console.WriteLine("All downloads complete completed");

    }

}

