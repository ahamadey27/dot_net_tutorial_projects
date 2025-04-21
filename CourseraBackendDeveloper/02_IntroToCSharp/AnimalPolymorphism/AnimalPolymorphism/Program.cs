// Define an interface named IAnimal
// This interface declares a method signature for Eat(), which must be implemented by any class that implements this interface.
public interface IAnimal
{
    void Eat();
}

// Define the Program class, which contains the Main method (entry point of the application).
public class Program
{
    static void Main(string[] args)
    {
        // Create an instance of the Dog class and call its MakeSound method.
        Dog dog = new Dog();
        dog.MakeSound();

        // Create an instance of the Cat class and call its MakeSound method.
        Cat cat = new Cat();
        cat.MakeSound();

        // Call the Eat method for both the Dog and Cat instances.
        dog.Eat();
        cat.Eat();
    }
}

// Define a base class named Animal that implements the IAnimal interface.
// This class provides default implementations for the Eat and MakeSound methods, which can be overridden by derived classes.
public class Animal : IAnimal
{
    // Virtual method Eat() provides a default implementation for eating behavior.
    public virtual void Eat()
    {
        Console.WriteLine("The animal is eating.");
    }

    // Virtual method MakeSound() provides a default implementation for making a sound.
    // This method is empty in the base class and is intended to be overridden by derived classes.
    public virtual void MakeSound()
    {
        // No default sound for a generic animal.
    }
}

// Define a Dog class that inherits from the Animal class.
// This class overrides the Eat and MakeSound methods to provide specific behavior for a dog.
public class Dog : Animal
{
    // Override the Eat method to provide a specific implementation for a dog.
    public override void Eat()
    {
        Console.WriteLine("The dog is eating.");
    }

    // Override the MakeSound method to provide a specific implementation for a dog.
    public override void MakeSound()
    {
        Console.WriteLine("Bark");
    }
}

// Define a Cat class that inherits from the Animal class.
// This class overrides the Eat and MakeSound methods to provide specific behavior for a cat.
public class Cat : Animal
{
    // Override the Eat method to provide a specific implementation for a cat.
    public override void Eat()
    {
        Console.WriteLine("The cat is eating.");
    }

    // Override the MakeSound method to provide a specific implementation for a cat.
    public override void MakeSound()
    {
        Console.WriteLine("Meow");
    }
}
