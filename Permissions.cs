namespace App;

public class AdminPermissions
{
    private bool _canManagePermissions;
    private bool _canAssignRegions;
    private bool _canHandleRegistrations;


    public AdminPermissions()
    {
        _canManagePermissions = true;
        _canAssignRegions = true;
        _canHandleRegistrations = true;
    }
    // check methods 
    public bool CanManagePermissions() => _canManagePermissions;
    public bool CanManageRegions() => _canAssignRegions;
    public bool CanHandleRegistrations() => _canHandleRegistrations;

    // 



}


