namespace App;

public static class Login_System
{
      //List holding all users in the memory
      public static List<User> users = new List<User>();

      public static void Init()
      {
            users = Utils.ReadLogins();
      }

      public static void RegisterUser()
      {
            Console.Write("Enter your username: ");
            string? newusername = Console.ReadLine();

            Console.Write("Enter your password : ");
            string? _newpassword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newusername) || string.IsNullOrWhiteSpace(_newpassword))
            {
                  Console.WriteLine("Username and password cannot be empty!");
                  return;
            }

            //Controls if the username already exists
            bool userExists = users.Exists(u => u.Username.Equals(newusername, StringComparison.OrdinalIgnoreCase));
            if (userExists)
            {
                  Console.WriteLine("That username is already taken.");
                  return;
            }

            //create new user and add to the list
            User newuser = new User(newusername, _newpassword);
            users.Add(newuser);

            //save user to the file
            Utils.SaveLogin(newusername, _newpassword);

            Console.WriteLine("Your account have been created");
      }
      public static void LoginUser()
      {
            Console.Write("Enter your username: ");
            string? loginUser = Console.ReadLine();

            Console.Write(" Enter your password: ");
            string? loginPassword = Console.ReadLine();

            bool loggedIn = false;
            User currentUser = null;

            //check if the user is in the list
            foreach (User user in users)
            {
                  if (user.TryLogin(loginUser, loginPassword))
                  {
                        loggedIn = true;
                        currentUser = user;
                        Console.WriteLine(" Welcome " + loginUser);
                        break;
                  }

            }

            if (loggedIn && currentUser != null)
            {
                  Console.WriteLine($"Welcome, {currentUser.Username}!");
            }
            else
            {
                  Console.WriteLine("Wrong username or password.");
            }


      }

}













/*namespace b;

List<User> user = new List<User>();

bool loginmeny = true;

while (loginmeny)
{
      Console.WriteLine("Select a option");
      Console.WriteLine("1. Create account");
      Console.WriteLine("2. Log in");
      Console.WriteLine("3. Quit");
      string option = Console.ReadLine();

      switch (option)
      {
            case "1":
                  Console.Write(" Type your username : ");
                  string newusername = Console.ReadLine();

Console.WriteLine(" Enter your password : ");
                  string newpassword = Console.ReadLine();

User newuser = new User(newusername, newpassword);
user.Add(newUser);

                  Console.WriteLine(" Account created! ");
                  break;
            case "2":
                  Console.Write(" Enter your username: ");
                  string loginUser = Console.ReadLine();

Console.Write(" Enter your password: ");
                  string loginPassword = Console.ReadLine();

                  foreach (User users in user)
                  {
                        if (users.TryLogin(loginUser, loginPassword))
                        {
                              loggedIn = true;
                              currentUser = users;
                              Console.WriteLine(" Welcome " + loginUser);
                              break;
                        }
                  }
                  break;
            case "3":
                  loginmeny = false;
                  break; 



      }
}
*/
