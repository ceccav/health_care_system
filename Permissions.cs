namespace App;

class AdminPermissions       //sets all to false by default
{
    private bool _canManagePermissions = false;     // superadmin, admin
    private bool _canAssignRegions = false;         // superadmin, admin
    private bool _canHandleRegistrations = false;   // superadmin, admin
    private bool _canAddLocations = false;          // superadmin, admin
    private bool __canCreateAccounts = false;       // superadmin, admin

    private bool _canViewListOfPermissions = false;     // superadmin, admin 
    private bool _canAcceptUserRegistrations = false;   //superadmin, admin






    public void EnablePermission() { _canManagePermissions = true; }
    public void EnableRegionAssignment() { _canAssignRegions = true; }

    public void EnableRegistrationHandling() { _canHandleRegistrations = true; }

}




