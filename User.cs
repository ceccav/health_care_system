using System.Security;
using System;


namespace App;

public class User
{
    public string SSN;
    string Password;
    public string First_name;
    public string Last_name;
    public Role Role;
    public List<Permissions> Permissions = new();
    public List<Role> roles = new List<Role> { Role.Pending };
    public Regions Regions;
    
    

    public User(string ssn, string password, string first_name, string last_name, Regions regions, Role role)
    {
        SSN = ssn;
        Password = password;
        First_name = first_name;
        Last_name = last_name;
        Role = role;
        Regions = regions;
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

    public string GetPasswordForSaving()
    {
        return Password;
    }

}

public enum Role
{
    SuperAdmin,
    Admin,
    Doctor,
    Patient,
    Nurse,
    Pending,
}