using System;
using System.IO;
using System.Xml.Serialization;

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
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));

        using (StreamWriter writer = new StreamWriter("person.xml"))
        {
            xmlSerializer.Serialize(writer, samplePerson);
        }

        Console.WriteLine("XML serialization complete.");
    }
}