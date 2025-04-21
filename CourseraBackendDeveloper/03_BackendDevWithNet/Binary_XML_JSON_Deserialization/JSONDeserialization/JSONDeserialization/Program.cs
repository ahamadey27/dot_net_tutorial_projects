using System;
using System.Text.Json;

public class Person
{
    public string UserName { get; set; }
    public int UserAge { get; set; }
}

public class Program
{
    public static void Main()
    {
        var jsonData = "{\"UserName\": \"Charlie\", \"UserAge\": 45}";
        var deserializedPerson = JsonSerializer.Deserialize<Person>(jsonData);

        Console.WriteLine($"JSON Deserialization - UserName: {deserializedPerson.UserName}, UserAge: {deserializedPerson.UserAge}");
    }
}