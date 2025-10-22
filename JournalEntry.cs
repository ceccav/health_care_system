namespace App;

//the class journal represents a journalpost in the system. It holds information about patient, date and notes
//note: are we going to implement prescriptions or let prescriptions be their own textfile?
class JournalEntry
{
    // Privat fields holding data about the name the journal belongs to, the name of the doctor who created the post
    //date of the day the journalnote was made and the note in the journal.
    public readonly string PatientName;
    public readonly string DoctorName;
    public readonly DateTime AppointmentTime;
    public readonly string Notes;

    //Konstruktor, creating a new journal object with information about patient, dr, date and note.
    public JournalEntry(string patientName, string doctorName, DateTime appointmentTime, string notes)
    {
        PatientName = patientName;
        DoctorName = doctorName;
        AppointmentTime = appointmentTime;
        Notes = notes;
    }
}