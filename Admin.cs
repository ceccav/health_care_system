namespace App;

public class AdminRole : IRole
{
    public Role GetRoleType() => Role.Admin;

    public bool HasPermission(string action)
    {
        return false;
    }

    public void AssignRegion(AdminUser targetAdmin, string region, AdminUser actingAdmin)
    {
        if (actingAdmin.Permissions().CanAssignRegions())
        {
            targetAdmin.AssignRegion(region);
        }
        else
        {
            Console.WriteLine($"{actingAdmin.GetUsername()}does not have permissions to assign regions");
        }
    }
}

public class AdminUser
{
    private string _username;
    private string _region;

    private AdminPermissions _permissions = new AdminPermissions();

    public AdminUser(string username)
    {
        _username = username;
    }

    public string GetUsername() => _username;
    public string GetRegion() => _region;
    public void AssignRegion(string region) => _region = region;
    public AdminPermissions Permissions() => _permissions;
}