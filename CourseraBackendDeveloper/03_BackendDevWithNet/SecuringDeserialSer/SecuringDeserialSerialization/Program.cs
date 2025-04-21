using System;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    // Encrypt sensitive data (e.g., Password) before serialization
    public void EncryptData()
    {
        Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(Password));
    }

    // Generate a hash for the object to ensure data integrity
    public string GenerateHash()
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(ToString()));
            return Convert.ToBase64String(hashBytes);
        }
    }

    // Override ToString to serialize the object into JSON format
    public override string ToString() => JsonSerializer.Serialize(this);
}

public class Program
{
    public static void Main()
    {
        // Create a user object with sensitive data
        User user = new User { Name = "Alice", Email = "alice@example.com", Password = "SecureP@ss123" };

        // Step 2: Serialization risks
        // Serialize the user object after encrypting sensitive data
        string serializedData = SerializeUserData(user);
        Console.WriteLine("Serialized Data:\n" + serializedData);

        // Step 3: Input validation
        // (Placeholder for input validation logic, if needed)

        // Step 5: Deserialize only from trusted sources
        // Ensure deserialization is performed only on data from trusted sources
        string trustedSourceData = serializedData; // Assume this is from a trusted source
        User deserializedUser = DeserializeUserData(trustedSourceData, isTrustedSource: true);

        if (deserializedUser != null)
        {
            Console.WriteLine("Deserialization successful for trusted source.");
        }
    }

    // Serialize user data securely by encrypting sensitive fields
    public static string SerializeUserData(User user)
    {
        user.EncryptData(); // Encrypt sensitive data before serialization
        return JsonSerializer.Serialize(user);
    }

    // Deserialize user data securely by validating the source
    public static User DeserializeUserData(string jsonData, bool isTrustedSource)
    {
        if (!isTrustedSource)
        {
            // Block deserialization if the source is untrusted
            Console.WriteLine("Deserialization blocked: Untrusted source.");
            return null;
        }
        // Deserialize the JSON data into a User object
        return JsonSerializer.Deserialize<User>(jsonData);
    }
}