namespace App;
//class to manage all the different events happening.
// Event to book an appointment is added
class EventManager
{
    //When a booking is made it will trigger this event
    public static event Action<string, DateTime>? AppointmentBooked;

    //Readonly list holding all of the booked appointments, you can add to it but not change it.
    private readonly List<Appointment> _appointments = new List<Appointment>();

    //Method to book a new appointment
    public Appointment BookAppointment(string patientName, DateTime startTime)
    {

        Appointment appointment = new Appointment(patientName, startTime); // creates new appointment-object
        _appointments.Add(appointment); //adds to the list of all bookings

        //triggers the event ''AppointmentBooked'' if someone is subscribing to it, only runs if it isn't null
        AppointmentBooked?.Invoke(appointment._patientName, appointment._startTime);


        Console.WriteLine($"Appointment booked for {appointment._patientName}, on {appointment._startTime.ToString("yyyy-mm-dd HH:mm")}"); //writes out in console


        return appointment; //returns the booking
    }
}