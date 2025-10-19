using System.ComponentModel;

namespace App;

//This class represents the creation of an appointment
class Appointment
{
    public readonly string _patientName;
    public readonly DateTime _startTime;

    //Constructor that sets the patientname and startime
    //when the object is created it cannot be manipulated
    public Appointment(string PatientName, DateTime startTime)
    {
        _patientName = PatientName;
        _startTime = startTime;
    }
}