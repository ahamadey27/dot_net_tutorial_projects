public class Database
{
    private static Database instance;
    private static readonly object lockObject = new object();

    // Private constructor prevents instantiation from other classes
    private Database() { }

    public static Database GetInstance()
    {
        if (instance == null)
        {
            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = new Database();
                }
            }
        }
        return instance;
    }

    public void Connect()
    {
        Console.WriteLine("Database connected.");
    }
}
