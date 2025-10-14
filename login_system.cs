namespace App;

class Userservice
{
      public static List<User> user = new List<User>();

      public static void RegisterUser()
      {
            Console.Write("Type your username: ");
            string newusername = Console.ReadLine();

            Console.Write(" Enter your password : ");
            string newpassword = Console.ReadLine();

            User newuser = new User(newusername, newpassword);
            user.Add(newuser);
            //SaveLogin(users)

            Console.WriteLine("Your account have been created");
      }
      public static void LoginUser()
      {
            Console.Write(" Enter your username: ");
            string loginUser = Console.ReadLine();

            Console.Write(" Enter your password: ");
            string loginPassword = Console.ReadLine();

            foreach (User users in user)
            {
                  bool loggedIn = false;
                  User currentUser = null;
                  if (users.TryLogin(loginUser, loginPassword))
                  {


                        loggedIn = true;
                        currentUser = users;
                        Console.WriteLine(" Welcome " + loginUser);
                        break;
                  }

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
