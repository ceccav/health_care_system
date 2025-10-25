namespace App;

class Location
{
    public string? Name;
    public string? City;
    public string? Address;
    public string? PostalCode;
    public Regions Region;

    public string Line()
    {
        return Name + ", " + City + ", " + Region + "|" + Address + ", " + PostalCode;
    }

}

enum Regions

{
    Skåne,
    Stockholm,
    Blekinge,
    Västergötland,
    Norrbotten,
    Kalmar,
    Jämtland,
    Lappland,
    Ångermanland,
    Halland,
    Öland,
    Gotland,
    Dalarna,
}

enum Cities
{
    Malmö,
    Stockholm,
    Blekinge,
    Göteborg,
    Luleå,
    Kalmar,
    Kiruna,
    Halmstad,
    Visby,
    Falun,
}