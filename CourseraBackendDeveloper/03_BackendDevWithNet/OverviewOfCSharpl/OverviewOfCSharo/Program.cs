//Interface for discounted prices
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public interface IDiscountable
{
    decimal ApplyDiscount(decimal percentage);
}

//Base product class with fields, properties and static method
class Product
{
    //private field
    private decimal _price;

    //Public property
    public string Name { get; set; }

    //Public property with additional logic in setter
    public decimal Price
    {
        get { return _price; }
        set
        {
            if (value >= 0) _price = value;
        }
    }

    //Constructor
    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public virtual void DisplayProductDetails()
    {
        Console.WriteLine($"Product: {Name}, Price: {Price}");
    }

    //static method to calclulate discount
    public static decimal CalclulateDiscount(decimal price, decimal discountPercentage)
    {
        return price - (price * discountPercentage / 100);
    }
}

//Subclass: Clothing (Discountable)
class Clothing : Product, IDiscountable
{
    //Property to store the size as an integer
    public int Size { get; set; }

    public Clothing(string name, decimal price, int size) : base(name, price)
    {
        Size = size;
    }

    //Method to convert size from int to a size name
    public string GetSizeName()
    {
        return Size switch
        {
            1 => "SM",
            2 => "MD",
            3 => "LG",
            _ => "Unknown Size"
        };
    }

    //Override method to include size details
    public override void DisplayProductDetails()
    {
        base.DisplayProductDetails();
        Console.WriteLine($"Size: {GetSizeName()}");
    }

    //Implementation of IDiscountable interface
    public decimal ApplyDiscount(decimal percentage)
    {
        return CalclulateDiscount(Price, percentage);
    }
}

class Program
{
    static void Main()
    {
        
    }
}