namespace App;

public class AdminPermissions       //sets all to false by default
{
    private bool _canManagePermissions = false;
    private bool _canAssignRegions = false;
    private bool _canHandleRegistrations = false;





    public void EnablePermission() { _canManagePermissions = true; }
    public void EnableRegionAssignment() { _canAssignRegions = true; }

    public void EnableRegistrationHandling() { _canHandleRegistrations = true; }

}




