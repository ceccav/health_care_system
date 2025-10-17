using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Security;
using App;
App.EventHandler eventHandler = new(); // instansiates the eventhandler class
List<User> users = new List<User>(); //List for all the users
bool running = true;
User? active_user = null; //active user set to null when the program starts
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

                }
                break;
            case "2":
                {
                    users = Save_System.ReadLogins();

                    Console.Write("Enter your SSN: ");
                    string? loginSSN = Console.ReadLine();

                    Console.Write(" Enter your password: ");
                    string? loginPassword = Console.ReadLine();

                    bool loggedin = false;


                    //check if the user is in the list
                    foreach (User user in users)
                    {
                        if (user.TryLogin(loginSSN, loginPassword))
                        {
                            user.GrantAllPermissions();
                            active_user = user;
                            Console.WriteLine($"Welcome {user.First_name} {user.Last_name}");
                            loggedin = true;
                            continue;
                        }
                    }

                    if (!loggedin)
                    {
                        Console.WriteLine("Invalid SSN or password");
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
        if (active_user.IsAllowed(App.Permissions.CreateAccountPersonnel))
        {
            Console.WriteLine("[4] - Create account for personnel");
        }
        if (active_user.IsAllowed(App.Permissions.ViewPermissions))
        {
            Console.WriteLine("[9] - View users and their permissions");
        }


        switch (Console.ReadLine())
        {
            case "1":       //if the user is allowed to view all users, show every user
                if (active_user.IsAllowed(App.Permissions.ViewAllUsers))        //kan använda && 
                {
                    Console.WriteLine("All users: ");

                    foreach (User user in users)        //for every user in my user list
                    {
                        Console.WriteLine($"{user.First_name} {user.Last_name}");       //show every user
                    }
                    Console.ReadLine();
                }
                break;

            case "4":
                if (active_user.IsAllowed(App.Permissions.CreateAccountPersonnel))
                {
                    Console.Write("Enter the SSN of the personell: ");
                    string? newssn = Console.ReadLine();

                    Console.Write("Enter the password to the personell: ");
                    string? _newpassword = Console.ReadLine();

                    Console.Write("Enter the first name of the personell: ");
                    string? newfirst_name = Console.ReadLine();

                    Console.Write("Enter the last name of the personell: ");
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
                    Console.WriteLine("The account have been created");
                }
                break;

            case "9":       //active user is allowed to view all users and their permissions
                if (active_user.IsAllowed(App.Permissions.ViewPermissions))     //if the user is allowed
                {
                    Console.WriteLine("All users and their permissions: ");

                    foreach (User user in users)        //run through the list of users and show them, + their permissions
                    {
                        Console.WriteLine($"{user.First_name} {user.Last_name} {string.Join(", ", user.Permissions)}");
                    }
                    Console.ReadLine();
                }
                break;
        }




        //add code
        // }
        // if (active_user.IsAllowed(App.Permissions.ViewMyPersonal))
        // {

    }
}




void TryClear()
{
    try { Console.Clear(); } catch { }
}










