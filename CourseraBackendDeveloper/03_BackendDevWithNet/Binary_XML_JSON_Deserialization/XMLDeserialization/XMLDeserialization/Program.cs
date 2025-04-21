using System;
using System.IO;
using System.Xml.Serialization;

public class Person
{
    public string UserName { get; set; }
    public int UserAge { get; set; }
}

public class Program
{
    public static void Main()
    {
        var xmlData = "<Person><UserName>Bob</UserName><UserAge>30</UserAge></Person>";
        var serializer = new XmlSerializer(typeof(Person));

        using (var reader = new StringReader(xmlData))
        {
            var deserializedPerson = (Person)serializer.Deserialize(reader);
            Console.WriteLine($"XML Deserialization - UserName: {deserializedPerson.UserName}, UserAge: {deserializedPerson.UserAge}");
        }
    }
}