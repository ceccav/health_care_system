using System.Net;

namespace App;

//This is the admin with sufficient permissions
// I need to be able to give other admins the permission to handle the permission system
// Assign admins to certain regions
// Give permissions to handle registrations
// Basically superadmin gives lots of permissions to admins

public class SuperAdminRole : IRole
{
    public Role GetRoleType() => Role.SuperAdmin;           //gives superadmin the role superadmin
    public bool HasPermission(string action)        //gives permission to the role superadmin
    {
        return true;        //says yes to give permission
    }
    public void GrantPermission(AdminUser admin)        //gives regular admin permissions
    {
        admin.Permissions().EnablePermission();
    }

    public void AssignAdminToRegion(AdminUser admin, string region)
    {
        admin.AssignRegion(region);
    }

    public void GrantRegistrationHandling(AdminUser admin)            // gives other admins permissions to handle registrations
    {
        admin.Permissions().EnableRegistrationHandling();
    }

}

