namespace App;
//class to manage all the different events happening. 
class EventHandler
{
    public static event Action<string, DateTime>? AppointmentBooked;

    public async Task BookAppointment(string PatientName, DateTime date, DateTime time)
    {
        Console.WriteLine("Appointmen booked for user day month time");
    }
}