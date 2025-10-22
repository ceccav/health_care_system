using System.ComponentModel;

namespace App;

//This class represents an appointment
class Appointment
{
    //patiens social security numer, is used to find journal
    public readonly string _ssn;
    public readonly string _patientName; //full name for the patient
    public readonly DateTime _startTime; //date and time for appointment

    //Constructor that sets the patientname and startime
    //when the object is created it cannot be manipulated beacuse they are set as readonly
    public Appointment(string ssn, string PatientName, DateTime startTime)
    {
        _ssn = ssn;
        _patientName = PatientName;
        _startTime = startTime;
    }
}