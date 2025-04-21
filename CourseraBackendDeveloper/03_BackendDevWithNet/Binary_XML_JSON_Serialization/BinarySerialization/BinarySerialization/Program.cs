using System;
using System.IO;

public class Person
{
    public string UserName { get; set; }
    public int UserAge { get; set; }
}

class Program
{
    static void Main()
    {
        Person samplePerson = new Person { UserName = "Alice", UserAge = 30 };
        // Binary serialization
        using (FileStream fs = new FileStream("person.dat", FileMode.Create))
        {
            BinaryWriter writer = new BinaryWriter(fs);
            writer.Write(samplePerson.UserName);
            writer.Write(samplePerson.UserAge);
        }
        Console.WriteLine("Binary serialization complete.");
    }
}