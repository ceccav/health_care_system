namespace App;

//the class journal represents a journalpost in the system. It holds information about ssn, patient, date and notes
//note: are we going to implement prescriptions or let prescriptions be their own textfile?
class JournalEntry
{
    // Privat fields holding data about the name the journal belongs to, the name of the doctor who created the post
    //date of the day the journalnote was made and the note in the journal.
    public readonly string SSN; //socialsecuritynumber works as a uniqe id
    public readonly string PatientName;
    public readonly string DoctorName; //the person writing the note
    public readonly DateTime AppointmentTime; //connects the visit with the journal
    public readonly string Notes; //the journal text written by a dr or nurse
    public readonly Regions Region; //region
    public readonly string Hospital;

    //creates a new journalpost with all the info set. ssn,patient,dr,appointment and notes
    public JournalEntry(string ssn, string patientName, string doctorName, DateTime appointmentTime, string notes, Regions regions, string hospital)
    {
        SSN = ssn;
        PatientName = patientName;
        DoctorName = doctorName;
        AppointmentTime = appointmentTime;
        Notes = notes;
        Region = regions;
        Hospital = hospital;
    }
}