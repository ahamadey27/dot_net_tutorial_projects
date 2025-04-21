// By default, all characters available for use and a length of 16
// Will return a random password with the default settings 
using PasswordGenerator;

var pwd = new Password();
var password = pwd.Next(); 
Console.WriteLine(password.ToString()); 