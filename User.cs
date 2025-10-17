using System.Security;
using System;


namespace App;

class User
{
    public string SSN;
    string Password;
    public string First_name;
    public string Last_name;
    public Role Role;
    List<Permissions> Permissions = new();

    public User(string ssn, string password, string first_name, string last_name, Role role)
    {
        SSN = ssn;
        Password = password;
        First_name = first_name;
        Last_name = last_name;
        Role = role;
    }

    public bool TryLogin(string ssn, string password)
    {
        return ssn == SSN && password == Password;
    }

    public bool IsAllowed(Permissions permission)
    {
        return Permissions.Contains(permission);
    }

    public void GrantAllPermissions()
    {
        this.Permissions.Clear();
        foreach (App.Permissions currentPermission in Enum.GetValues<App.Permissions>())
        {
            this.Permissions.Add(currentPermission);
        }
    }

}

public enum Role
{
    SuperAdmin,
    Admin,
    Doctor,
    Patient,
    Nurse,
}