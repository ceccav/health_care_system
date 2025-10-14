using App;

bool loginmenu = true;

while(loginmenu)
{
    Console.WriteLine("Select a option");
    Console.WriteLine("1. Register as a patient");
    Console.WriteLine("2. Log in");
    Console.WriteLine("3. Quit");
    string option = Console.ReadLine();

    switch (option)
    {
        case "1":
            Userservice.RegisterUser();
            break;
        case "2":
            Userservice.LoginUser();
            break;
        case "3":
            loginmenu = false;
            break;
    }
}

