using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Security;
using App;

List<User> users = new List<User>();
bool running = true;
User? active_user = null;
//Reads in all users from data/users.txt
users = Save_System.ReadLogins();



while (running)
{
    if (active_user == null)
    {
        Console.WriteLine("Select a option");
        Console.WriteLine("1. Register as a patient");
        Console.WriteLine("2. Log in");
        Console.WriteLine("3. Quit");
        string? option = Console.ReadLine();

        switch (option)
        {

            case "1":
                {

                    Console.Write("Enter your SSN: ");
                    string? newssn = Console.ReadLine();

                    Console.Write("Enter your password: ");
                    string? _newpassword = Console.ReadLine();

                    Console.Write("Enter your first name: ");
                    string? newfirst_name = Console.ReadLine();

                    Console.Write("Enter your last name: ");
                    string? newlast_name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newssn) || string.IsNullOrWhiteSpace(_newpassword) || string.IsNullOrWhiteSpace(newfirst_name) || string.IsNullOrWhiteSpace(newlast_name))
                    {
                        Console.WriteLine("SSN, first name, last name and password cannot be empty!");
                        return;
                    }

                    //Controls if the username already exists
                    bool userExists = users.Exists(u => u.SSN.Equals(newssn, StringComparison.OrdinalIgnoreCase));
                    if (userExists)
                    {
                        Console.WriteLine("That SSN is already taken.");
                        return;
                    }

                    //create new user and add to the list
                    User newuser = new User(newssn, _newpassword, newfirst_name, newlast_name);
                    users.Add(newuser);

                    //save user to the file
                    Save_System.SaveLogin(newssn, _newpassword, newfirst_name, newlast_name);
                    Console.WriteLine("Your account have been created");
                    Console.Write("Enter your password: ");
                    string? loginPassword = Console.ReadLine();
                }
                break;
            case "2":
                {
                    Console.Write("Enter your SSN: ");
                    string? loginUser = Console.ReadLine();

                    Console.Write(" Enter your password: ");
                    string? loginPassword = Console.ReadLine();


                    //check if the user is in the list
                    foreach (User user in users)
                    {
                        if (user.TryLogin(loginUser, loginPassword))
                        {
                            active_user = user;
                            Console.WriteLine(" Welcome " + loginUser);
                            return;
                        }
                    }
                }
                break;
            case "3":
                running = false;
                break;
        }
    }
    else
    {
        TryClear();

        if (active_user.IsAllowed(App.Permissions.ViewAllUsers))
        {
            Console.WriteLine("[1] - View all users");
        }


        switch (Console.ReadLine())
        {
            case "1":
                if (active_user.IsAllowed(App.Permissions.ViewAllUsers))
                {

                }
                break;
        }
    }

}

void TryClear()
{
    try { Console.Clear(); } catch { }
}










