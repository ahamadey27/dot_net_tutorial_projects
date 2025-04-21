using System.IO;
using System.Xml.Serialization;
using System.Text.Json;

namespace SerializationExercise
{
    public class Person
    {
        required public string UserName { get; set; }
        required public int UserAge { get; set; }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var samplePerson = new SerializationExercise.Person { UserName = "Alex", UserAge = 35 };

        // Open a FileStream to create a file named person.dat
        using (FileStream fileStream = new FileStream("person.dat", FileMode.Create, FileAccess.Write))
        {
            // Instantiate a BinaryWriter using the FileStream object
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                // Serialize each property of the Person object manually
                writer.Write(samplePerson.UserName);
                writer.Write(samplePerson.UserAge);
            }
        }

        // Serialize the Person object to an XML file
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(SerializationExercise.Person));
        using (FileStream xmlFileStream = new FileStream("person.xml", FileMode.Create, FileAccess.Write))
        {
            xmlSerializer.Serialize(xmlFileStream, samplePerson);
        }

        // Serialize the Person object to a JSON string
        string jsonString = JsonSerializer.Serialize(samplePerson);

        // Save the JSON string to a .json file
        File.WriteAllText("person.json", jsonString);

        // Print confirmation messages
        Console.WriteLine("Person object has been serialized to person.xml");
        Console.WriteLine("Person object has been serialized to person.json");
    }
}
