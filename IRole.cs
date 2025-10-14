namespace App;

public interface IRole
{
    Role GetRoleType();     //returns which role it is
    bool HasPermission(string action);      //checks if the role has permission
    string GetRegion();    //vilken region den tillhör 
}

