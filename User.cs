namespace App;

public class User
{
    public string Username;
    private string Password;

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public bool TryLogin(string username, string password)
    {
        return username == Username && password == Password;
    }
}

public enum Role
{
    SuperAdmin,
    Admin,
    Doctor,
    Patient
}