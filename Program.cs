using App;

// Reads in all users from data/users.txt
Login_System.Init();

bool loginmenu = true;

while (loginmenu)
{
    Console.WriteLine("Select a option");
    Console.WriteLine("1. Register as a patient");
    Console.WriteLine("2. Log in");
    Console.WriteLine("3. Quit");
    string option = Console.ReadLine();

    switch (option)
    {
        case "1":
            Login_System.RegisterUser();
            break;
        case "2":
            Login_System.LoginUser();
            break;
        case "3":
            loginmenu = false;
            break;
    }
}

