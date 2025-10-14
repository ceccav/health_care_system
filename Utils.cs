// class for all our saves to files 
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
namespace App;

// Class holding methods to save data
public static class Utils
{
    public static void SaveLogin()
    {
        using (StreamWriter writer = new StreamWriter("userdata.txt"))
        {
            Console.WriteLine("Write your username");
            string Username = Console.ReadLine();
            Console.WriteLine("Write your password");
            string Password = Console.ReadLine();

            writer.WriteLine(Username, Password);     //writer.writeline only writes to userdata.txt not in the console
        }
    }
    //method to save user login data, as a static void so that we can implement it easier in our code
    public static void ReadLogin()
    {
        try
        {
            using (StreamReader sr = new StreamReader("userdata.txt")) //instans of new streamreader
            {
                string line;
                while ((line = sr.ReadLine()) != null) //while the user input is not empty
                {
                    Console.WriteLine(line); //writes in the data in the file
                }
            }
        }
        catch (Exception e) //if something goes wrong, shows error message.
        {
            Console.WriteLine("The file could not be read");
            Console.WriteLine(e.Message);
        }
    }
}

