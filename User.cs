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
    public List<Permissions> Permissions = new();
    // public List<Role> roles = new List<Role> { Role.Pending };
    // public List<Regions> regions = new List<Regions>();
    public Regions Regions;
    public string Hospital;



    public User(string ssn, string password, string first_name, string last_name, Regions regions, Role role, string hospital)
    {
        SSN = ssn;
        Password = password;
        First_name = first_name;
        Last_name = last_name;
        Regions = regions;
        Role = role;
        Hospital = hospital;

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

enum Role
{
    Admin,
    Doctor,
    Patient,
    Nurse,
    Pending,
}