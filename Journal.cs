namespace App;

//the class journal represents a journalpost in the system. It holds information about patient, date and notes
//note: are we going to implement prescriptions or let prescriptions be their own textfile?
class Journal
{
    // Privat fields holding data about the name the journal belongs to, the name of the doctor who created the post
    //date of the day the journalnote was made and the note in the journal.
    private readonly string PatientName;
    private readonly string DoctorName;
    private readonly DateTime Date;
    private readonly string Notes;

    //Konstruktor, creating a new journal object with information about patient, dr, date and note.
    public Journal(string patientName, string doctorName, DateTime date, string notes)
    {
        PatientName = patientName;
        DoctorName = doctorName;
        Date = date;
        Notes = notes;
    }
}