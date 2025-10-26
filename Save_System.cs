// class for all our saves to files 
using System;
using System.IO;
using System.Collections.Generic;
using System.Formats.Asn1;
namespace App;

// Class holding methods to save data
class Save_System
{
    //searchpath for the file with user data
    private static readonly string UserFilePath = Path.Combine("data", "users.txt");

    //searchpatch for appointments
    private static readonly string AppointmentsFilePath = Path.Combine("data", "appointments.txt");

    //searchpatch for journals
    private static readonly string JournalsFilePath = Path.Combine("data", "journals.txt");

    private static readonly string LocationsFilePath = Path.Combine("data", "locations.txt");       //a searchpath for locations file 

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //USERS

    //method to save user logindata to file, as a static void so that we can implement it easier in our code
    public static void SaveLogin(string ssn, string _password, string first_name, string last_name, Regions regions, Role role, string hospital)
    {
        Directory.CreateDirectory("data");

        //append: true makes it possibly for us to add to the file without writing over anything
        using (StreamWriter writer = new StreamWriter(UserFilePath, append: true))
        {
            writer.WriteLine($"{ssn}; {_password}; {first_name}; {last_name}; {regions}; {role} ; {hospital}");     //writer.writeline only writes to userdata.txt not in the console
        }
    }

    //Rewrites testfile users.txt after accepting or denying a user
    public static void SaveUpdatedUsers(List<User> users)     //used to save accepted patient and to remove denied patients
    {
        string FilePath = Path.Combine("data", "users.txt");

        using (StreamWriter writer = new StreamWriter(FilePath, append: false))
        {
            foreach (User user in users)
            {
                writer.WriteLine($"{user.SSN}; {user.GetPasswordForSaving()}; {user.First_name}; {user.Last_name}; {user.Regions} ; {user.Role} ; {user.Hospital}");
            }
        }
    }


    //Reads all the users from the file to a list
    public static List<User> ReadLogins()
    {
        List<User> users = new List<User>();

        //if the file doesn't exist return an empty list
        if (!File.Exists(UserFilePath)) return users;

        //reads the file one row in a time
        using (StreamReader reader = new StreamReader(UserFilePath)) //instans of new streamreader
        {
            string? line;
            while ((line = reader.ReadLine()) != null) //while the user input is not empty
            {
                string[] parts = line.Split(';'); //splits the line in to two parts
                if (parts.Length >= 7)
                {

                    string ssn = parts[0].Trim();
                    string _password = parts[1].Trim();
                    string first_name = parts[2].Trim();
                    string last_name = parts[3].Trim();
                    string regionsString = parts[4].Trim();
                    string roleString = parts[5].Trim();
                    string hospital = parts[6].Trim();


                    if (Enum.TryParse(roleString, out Role role) && Enum.TryParse(regionsString, out Regions regions) && !string.IsNullOrWhiteSpace(ssn) && !string.IsNullOrWhiteSpace(_password) && !string.IsNullOrWhiteSpace(first_name) && !string.IsNullOrWhiteSpace(last_name) && !string.IsNullOrWhiteSpace(hospital))
                    {
                        User user = new User(ssn, _password, first_name, last_name, regions, role, hospital);
                        users.Add(user);
                    }
                }
            }
        }
        return users;

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //APPOINTMENTS


    //Save a booked appointment as a row in appointments.txt
    public static void SaveAppointment(string ssn, string patientName, DateTime startTime, Regions regions, string hospital)
    {
        Directory.CreateDirectory("data"); //create the file if it doesn't already exist
        using (StreamWriter writer = new StreamWriter(AppointmentsFilePath, append: true))
        {
            writer.WriteLine(ssn + ";" + patientName + ";" + startTime.ToString("yyyy-MM-dd HH:mm") + ";" + regions + ";" + hospital);
        }
    }

    //reads all the bookings from the file to a list
    public static List<Appointment> ReadAppointments()
    {
        List<Appointment> appointments = new List<Appointment>();

        if (!File.Exists(AppointmentsFilePath))
            return appointments;

        //reads the file one row in a time
        using (StreamReader reader = new StreamReader(AppointmentsFilePath)) //instans of new streamreader
        {
            string? line;
            while ((line = reader.ReadLine()) != null) //
            {
                string[] parts = line.Split(';'); //splits the line in to two parts
                if (parts.Length >= 5)
                {
                    string ssn = parts[0].Trim();
                    string name = parts[1].Trim();
                    DateTime time;
                    Regions regions;
                    string hospital = parts[4].Trim();

                    if (DateTime.TryParse(parts[2].Trim(), out time) && Enum.TryParse(parts[3].Trim(), true, out regions))
                    {
                        appointments.Add(new Appointment(ssn, name, time, regions, hospital));
                    }
                }
            }
        }
        return appointments;

    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //LOCATIONS

    public static void SaveLocation(string hospitalName, string city, Regions region, string address, string postalcode)        //method to save all locations to file
    {
        using (StreamWriter writer = new StreamWriter(LocationsFilePath, append: true))
        {
            writer.WriteLine($"{region}; {city}; {hospitalName}; {address}; {postalcode}");
        }

    }

    public static List<Location> ReadLocations()        //method to read all locations from file   
    {
        List<Location> locations = new List<Location>();

        if (!File.Exists(LocationsFilePath))        //if the file doesn't exist
            return locations;

        using (StreamReader reader = new StreamReader(LocationsFilePath))       //reads the file one row in a time

        {
            string? line;
            while ((line = reader.ReadLine()) != null)           //while userinput isn't empty
            {
                string[] parts = line.Split(";");       //line gets split into two parts
                if (parts.Length >= 5)
                {
                    string region = parts[0].Trim();
                    string city = parts[1].Trim();
                    string name = parts[2].Trim();
                    string address = parts[3].Trim();
                    string postalcode = parts[4].Trim();

                    Regions reg;
                    if (Enum.TryParse(region, out reg) && !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(city) && !string.IsNullOrWhiteSpace(address) && !string.IsNullOrWhiteSpace(postalcode))
                    {
                        Location loc = new Location();
                        loc.Name = name;
                        loc.City = city;
                        loc.Region = reg;
                        loc.Address = address;
                        loc.PostalCode = postalcode;

                        locations.Add(loc);
                    }
                }
            }
        }
        return locations;
    }



    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //JOURNALS

    //When an appointment has been, dr writes note about it and then we use this wo write it so its saved in journals.txt
    public static void SaveJournal(string ssn, string patientName, string doctorName, DateTime appointmentTime, string notes, Regions regions, string hospital)
    {
        Directory.CreateDirectory("data"); //creates folder if it doesnt already exist
        using (StreamWriter writer = new StreamWriter(JournalsFilePath, append: true))
        {
            writer.WriteLine($"{ssn}; {patientName} ; {doctorName} ; {appointmentTime} ; {notes} ; {regions} ; {hospital}"); //what will be written in journal.txt
        }
    }

    //method to read journal from journal.txt. Takes the values in the textfiles, and splits it into 4 parts
    //patient, dr, date and notes.
    public static List<JournalEntry> ReadJournal()
    {
        List<JournalEntry> journal = new List<JournalEntry>();

        if (!File.Exists(JournalsFilePath))
            return journal;

        using (StreamReader reader = new StreamReader(JournalsFilePath))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(';'); //splits the line 
                if (parts.Length >= 7)
                {
                    string ssn = parts[0].Trim();
                    string patient = parts[1].Trim();
                    string doctor = parts[2].Trim();
                    string apptTime = parts[3].Trim();
                    string notes = parts[4].Trim();
                    string regionText = parts[5].Trim();
                    string hospital = parts[6].Trim();

                    DateTime time;
                    Regions region;

                    if (DateTime.TryParse(apptTime, out time) && Enum.TryParse(regionText, true, out region))
                    {
                        journal.Add(new JournalEntry(ssn, patient, doctor, time, notes, region, hospital));
                    }
                }
            }

        }
        return journal; //for one specifik person
    }
}

