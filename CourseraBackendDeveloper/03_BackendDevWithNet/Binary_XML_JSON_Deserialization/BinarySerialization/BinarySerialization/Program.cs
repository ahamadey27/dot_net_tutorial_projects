using System;
using System.IO;

public class Person
{
    public string UserName { get; set; }
    public int UserAge { get; set; }
}

public class Program
{
    public static void Main()
    {
        // Serialize example for testing
        var samplePerson = new Person { UserName = "Alice", UserAge = 30 };

        // Binary serialization
        using (var fs = new FileStream("person.dat", FileMode.Create))
        using (var writer = new BinaryWriter(fs))
        {
            writer.Write(samplePerson.UserName);
            writer.Write(samplePerson.UserAge);
        }
        Console.WriteLine("Binary serialization complete.");

        // Binary deserialization
        Person deserializedPerson;
        using (var fs = new FileStream("person.dat", FileMode.Open))
        using (var reader = new BinaryReader(fs))
        {
            deserializedPerson = new Person
            {
                UserName = reader.ReadString(),
                UserAge = reader.ReadInt32()
            };
        }

        Console.WriteLine($"Binary Deserialization - UserName: {deserializedPerson.UserName}, UserAge: {deserializedPerson.UserAge}");
    }
}