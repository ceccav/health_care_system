namespace App;
//class to manage all the different events happening.
// Event to book an appointment is added
class EventManager
{
    //Readonly list holding all of the booked appointments, you can add to it but not change it.
    private readonly List<Appointment> _appointments = new List<Appointment>();

    //Method to book a new appointment
    public Appointment BookAppointment(string patientName, DateTime startTime)
    {

        Appointment appointment = new Appointment(patientName, startTime); // creates new appointment-object
        _appointments.Add(appointment); //adds to the list of all bookings

        Save_System.SaveAppointment(patientName, startTime);
        Console.WriteLine($"Appointment booked for {appointment._patientName}, on {appointment._startTime.ToString("yyyy-mm-dd HH:mm")}"); //writes out in console

        return appointment;
    }
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
    public void AddJournalEntry(string patientName, string doctorName, DateTime appointmentTime, string notes)
    {
        Save_System.SaveJournal(patientName, doctorName, appointmentTime, notes);
    }

    public List<Appointment> GetAppointmentsForAddingNotes()
    {
        List<Appointment> pending = new List<Appointment>();
        List<JournalEntry> journals = Save_System.ReadJournal();
        DateTime now = DateTime.Now;

        foreach (Appointment appointment in _appointments)
        {
            if (appointment._startTime <= now)
            {
                bool hasNote = false;

                //is there any journal entry for this visit?
                foreach (JournalEntry journalEntry in journals)
                {
                    if (journalEntry.PatientName == appointment._patientName && journalEntry.AppointmentTime == appointment._startTime)
                    {
                        hasNote = true;
                        break;
                    }
                }
                if (!hasNote)
                {
                    pending.Add(appointment);
                }
            }
        }

        return pending;
    }
}