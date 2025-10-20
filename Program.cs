using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Security;
using App;
EventManager eventManager = new(); // instansiates the eventhandler class

//prenumate on the event
Action<string, DateTime> bookingHandler = (string name, DateTime time) =>
{
    Console.WriteLine("TESTING PRENUMERATION");
};

//Add prenumeration
EventManager.AppointmentBooked += bookingHandler;

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
                    //gives created account for patient the role patient
                    Role role = Role.Patient;

                    //create new user and add to the list
                    User newuser = new User(newssn, _newpassword, newfirst_name, newlast_name, role);
                    users.Add(newuser);

                    //save user to the file
                    Save_System.SaveLogin(newssn, _newpassword, newfirst_name, newlast_name, role);
                    Console.WriteLine("You have registrerd as a patient successfully.");

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

                //unsubscribes from Appointment event.
                EventManager.AppointmentBooked -= bookingHandler;
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
        if (active_user.IsAllowed(App.Permissions.ViewMyPersonal))
        {
            Console.WriteLine("[2] - View my journal");
            Console.WriteLine("[3] - Book an appointment");
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
            case "3":
                if (active_user.IsAllowed(App.Permissions.ViewMyPersonal))
                {
                    TryClear();
                    Console.WriteLine("=== Book an appointment ===");

                    Console.Write("Enter Date (YYYY-MM-DD): ");
                    string? dateInput = Console.ReadLine();

                    Console.Write("Enter time (HH:mm): ");
                    string? timeInput = Console.ReadLine();

                    DateTime datePart;
                    DateTime timePart;

                    //controls if the date is a valid date
                    if (!DateTime.TryParse(dateInput, out datePart))
                    {
                        Console.WriteLine("Not a valid date.");
                        break;
                    }

                    //controls if the user entered a valid time
                    if (!DateTime.TryParse(timeInput, out timePart))
                    {
                        Console.WriteLine("Not valid time.");
                        break;
                    }

                    //combines date and time to a completed value. .Date and .TimeOfDay shows only the date and the time isntead of the full year and so on.
                    DateTime startTime = datePart.Date + timePart.TimeOfDay;

                    //create patiens fullname for printOut in the console and storage
                    string fullName = active_user.First_name + " " + active_user.Last_name;

                    //Create the booking by calling the eventmanager
                    Appointment appointment = eventManager.BookAppointment(fullName, startTime);

                    Console.WriteLine();
                    Console.WriteLine("Press Enter to go back to menu");
                    Console.ReadLine();

                }
                break;

                //add code
        }
        if (active_user.IsAllowed(App.Permissions.ViewMyPersonal))
        {

        }

        if (active_user.IsAllowed(App.Permissions.AddLocations))
            
                     
        {
            Console.WriteLine("==Add Hospital==");
            List<string> hospitalList = new();
            Console.WriteLine("County : Skåne, Stockholm, Blekinge");
            Console.WriteLine("Add name of health center : ");
            string healtcenter = Console.ReadLine();
            Console.WriteLine("Ange län: ");
            string county = Console.ReadLine();

            hospitalList.Add(healtcenter + " (" + län + ")");

            Console.WriteLine("Du har lagt till: " + healtcenter + " i " + county);
            Console.WriteLine("Aktuella platser: ");

            foreach (string s in hospitalList)
            {
                Console.WriteLine("- " + s);
            }
        }
            
    }


}

void TryClear()
{
    try { Console.Clear(); } catch { }
}

//This is the code everyone should have










