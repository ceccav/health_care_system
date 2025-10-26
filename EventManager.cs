using System.Threading.Tasks.Dataflow;

namespace App;
//class to manage all the different events happening.
// Event to book an appointment is added
class EventManager
{
    //Readonly list holding all of the booked appointments, you can add to it but not change it. Is connected to journals.
    private readonly List<Appointment> _appointments = new List<Appointment>();

    public EventManager()
    {
        //when the program starts the program reads in the bookings from file and fills the list.
        List<Appointment> fromFile = Save_System.ReadAppointments();
        for (int i = 0; i < fromFile.Count; i++)
        {
            _appointments.Add(fromFile[i]);
        }
    }

    //Method to book a new appointment, creates an object, adds to the list of appointments and saves to file
    public Appointment BookAppointment(string ssn, string patientName, DateTime startTime, Regions regions, string hospital)
    {

        Appointment appointment = new Appointment(ssn, patientName, startTime, regions, hospital); // creates new appointment-object
        _appointments.Add(appointment); //adds to the list of all bookings

        Save_System.SaveAppointment(ssn, patientName, startTime, regions, hospital);
        Console.WriteLine($"Appointment booked for {appointment._patientName}, on {appointment._startTime:yyyy-MM-dd HH:mm} at {hospital}"); //writes out in console

        return appointment;
    }

    //get patients visit
    public List<Appointment> GetAppointmentsFor(string patientName, bool upcomingAppointment)
    {
        List<Appointment> matchingAppointments = new List<Appointment>();
        //if date is today or in the future
        DateTime today = DateTime.Today;

        //find the right appointments
        foreach (Appointment appointment in _appointments)
        {
            //filters out the old bookings, if uppcomingAppointment is true
            if (appointment._patientName == patientName && (!upcomingAppointment || appointment._startTime.Date >= today))
            {
                matchingAppointments.Add(appointment);

            }
        }

        return matchingAppointments;
    }
    //add journalpost for a specifik visit after an appointment
    public void AddJournalEntry(string ssn, string patientName, string doctorName, DateTime appointmentTime, string notes, Regions region, string hospital)
    {
        //writes to journals.txt
        Save_System.SaveJournal(ssn, patientName, doctorName, appointmentTime, notes, region, hospital);
    }

    //List all of the visits that has happened but that misses journalposts
    public List<Appointment> GetAppointmentsForAddingNotes()
    {
        List<Appointment> pending = new List<Appointment>(); //creates a list of appointments that has happened but does'nt have a note yet
        List<JournalEntry> journals = Save_System.ReadJournal(); //reads all journalposts from file
        DateTime now = DateTime.Now;

        foreach (Appointment appointment in _appointments)
        {
            //Filters out the appointments that hasn't taken place yet
            if (appointment._startTime <= now)
            {
                bool hasNote = false;

                //controls if there is a journalpost for the visit
                foreach (JournalEntry journalEntry in journals)
                {
                    if (journalEntry.PatientName == appointment._patientName && journalEntry.AppointmentTime == appointment._startTime)
                    {
                        hasNote = true;
                        break;
                    }
                }

                //if there is no note add it to the list of appointments to write journalnote for
                if (!hasNote)
                {
                    pending.Add(appointment);
                }
            }
        }

        return pending;
    }
}