// class for all our saves to files 
using System;
using System.IO;
using System.Collections.Generic;
namespace App;

// Class holding methods to save data
static class Save_System
{
    //searchpath for the file with user data
    private static readonly string FilePath = Path.Combine("data", "users.txt");

    //searchpatch for appointments
    private static readonly string AppointmentsFilePath = Path.Combine("data", "appointments.txt");

    //method to save user logindata to file, as a static void so that we can implement it easier in our code
    public static void SaveLogin(string ssn, string _password, string first_name, string last_name, Role role)
    {
        //append: true makes it possibly for us to add to the file without writing over anything
        using (StreamWriter writer = new StreamWriter(FilePath, append: true))
        {
            writer.WriteLine($"{ssn}; {_password}; {first_name}; {last_name}; {role}");     //writer.writeline only writes to userdata.txt not in the console
        }
    }
    //method to read all the users from the file
    public static List<User> ReadLogins()
    {
        List<User> users = new List<User>();

        //if the file doesn't exist return an empty list
        if (!File.Exists(FilePath))
            return users;

        //reads the file one row in a time
        using (StreamReader reader = new StreamReader(FilePath)) //instans of new streamreader
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

    public static void SaveAppointment(string patientName, DateTime startTime)
    {
        Directory.CreateDirectory("data"); //create the file if it doesn't already exist
        using (StreamWriter writer = new StreamWriter(AppointmentsFilePath, append: true))
        {
            writer.WriteLine(patientName + ";" + startTime.ToString("yyyy-MM-dd"));
        }
    }

    public static List<Appointment> ReadAppointments()
    {
        List<Appointment> appointments = new List<Appointment>();

        if (!File.Exists(AppointmentsFilePath))
            return appointments;

        //reads the file one row in a time
        using (StreamReader reader = new StreamReader(AppointmentsFilePath)) //instans of new streamreader
        {
            string? line;
            while ((line = reader.ReadLine()) != null) //while the user input is not empty
            {
                string[] parts = line.Split(';'); //splits the line in to two parts
                if (parts.Length == 2)
                {

                    string name = parts[0].Trim();
                    DateTime time;

                    if (DateTime.TryParse(parts[1].Trim(), out time))
                    {
                        appointments.Add(new Appointment(name, time));
                    }
                }

                line = reader.ReadLine(); //read next line
            }
        }
        return appointments;

    }
}

