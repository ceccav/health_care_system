// class for all our saves to files 
using System;
using System.IO;
using System.Collections.Generic;
namespace App;

// Class holding methods to save data
static class Save_System
{
    //searchpath for the file with user data
    private static readonly string UserFilePath = Path.Combine("data", "users.txt");

    //searchpatch for appointments
    private static readonly string AppointmentsFilePath = Path.Combine("data", "appointments.txt");

    //searchpatch for journals
    private static readonly string JournalsFilePath = Path.Combine("data", "journals.txt");

    //method to save user logindata to file, as a static void so that we can implement it easier in our code

    //----------------------------------------------------------------------------------------------------------------------------------------
    //USERS

    //Save a new user, append adds it to the list of the file.
    public static void SaveLogin(string ssn, string _password, string first_name, string last_name, Role role)
    {
        //append: true makes it possibly for us to add to the file without writing over anything
        using (StreamWriter writer = new StreamWriter(UserFilePath, append: true))
        {
            writer.WriteLine($"{ssn}; {_password}; {first_name}; {last_name}; {role}");     //writer.writeline only writes to userdata.txt not in the console
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
                writer.WriteLine($"{user.SSN}; {user.GetPasswordForSaving()}; {user.First_name}; {user.Last_name}; {user.Role}");
            }
        }
    }


    //Reads all the users from the file to a list
    public static List<User> ReadLogins()
    {
        List<User> users = new List<User>();

        //if the file doesn't exist return an empty list
        if (!File.Exists(UserFilePath))
            return users;

        //reads the file one row in a time
        using (StreamReader reader = new StreamReader(UserFilePath)) //instans of new streamreader
        {
            string? line;
            while ((line = reader.ReadLine()) != null) //while the user input is not empty
            {
                string[] parts = line.Split(';'); //splits the line in to two parts
                if (parts.Length >= 5)
                {

                    string ssn = parts[0].Trim();
                    string _password = parts[1].Trim();
                    string first_name = parts[2].Trim();
                    string last_name = parts[3].Trim();
                    string roleString = parts[4].Trim();

                    //validation, checks that the ennum is correct and that no fields is left empty.
                    if (Enum.TryParse(roleString, out Role role) && !string.IsNullOrWhiteSpace(ssn) && !string.IsNullOrWhiteSpace(_password) && !string.IsNullOrWhiteSpace(first_name) && !string.IsNullOrWhiteSpace(last_name))
                    {
                        User user = new User(ssn, _password, first_name, last_name, role);
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
    public static void SaveAppointment(string ssn, string patientName, DateTime startTime)
    {
        Directory.CreateDirectory("data"); //create the file if it doesn't already exist
        using (StreamWriter writer = new StreamWriter(AppointmentsFilePath, append: true))
        {
            writer.WriteLine(ssn + ";" + patientName + ";" + startTime.ToString("yyyy-MM-dd HH:mm"));
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
                if (parts.Length >= 3)
                {
                    string ssn = parts[0].Trim();
                    string name = parts[1].Trim();
                    string apptTime = parts[2].Trim();

                    if (DateTime.TryParse(apptTime, out DateTime time))
                    {
                        appointments.Add(new Appointment(ssn, name, time));
                    }
                }
            }
        }
        return appointments;

    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //JOURNALS

    //When an appointment has been, dr writes note about it and then we use this wo write it so its saved in journals.txt
    public static void SaveJournal(string ssn, string patientName, string doctorName, DateTime appointmentTime, string notes)
    {
        Directory.CreateDirectory("data"); //creates folder if it doesnt already exist
        using (StreamWriter writer = new StreamWriter(JournalsFilePath, append: true))
        {
            writer.WriteLine($"{ssn}; {patientName} ; {doctorName} ; {appointmentTime} ; {notes}"); //what will be written in journal.txt
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
                if (parts.Length >= 5)
                {
                    string ssn = parts[0].Trim();
                    string patient = parts[1].Trim();
                    string doctor = parts[2].Trim();
                    string apptTime = parts[3].Trim();
                    string notes = parts[4].Trim();

                    if (DateTime.TryParse(apptTime, out DateTime time))
                    {
                        journal.Add(new JournalEntry(ssn, patient, doctor, time, notes)); //adds to journal list
                    }
                }
            }

        }
        return journal; //for one specifik person
    }
}

