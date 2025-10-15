namespace App;

class User
{
    public string Username;
    private string Password;

    List<Permissions> Permissions = new();

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public bool TryLogin(string username, string password)
    {
        return username == Username && password == Password;
    }

    public bool IsAllowed(Permission permission)
    {
        return Permissions.Contains(permission);
    }
}

public enum Role
{
    SuperAdmin,
    Admin,
    Doctor,
    Patient
}