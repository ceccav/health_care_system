This is a simple medical health care system.
This is how to run the program as it is currently with all users having all permissions.

Start by typing dotnet run in your terminal.

Select an option
1. Register as a patient
2. Log in
3. Quit

Choose 1 to register as a new patient.
Enter your social security number, password, first name, last name and region.
Wait for an admin to apporve your registration.
Once approved, log in using option 2.
If you have an account, log in using option 2.
If you want to quit the program, press q

Once logged in:

1. View all users
2. View booked appointment
3. Book an appointment
4. Create account for personell
5. View users and their permissions
6. Add hospitals and their locations
7. Handle registrations
8. Write journal note
9. See my journal
Q. Logout

Choose 1 to display a list of every registered user (first and last name).

Choose 2 shows all upcoming appointments for the logged-in user.

Choose 3 to create a new appointment. Enter a date (YYYY-MM-DD) and time (HH:mm). The program validates the inputs and creates the appointment using the EventManager.

Choose 4 to create accounts for personnel. You will be asked to enter: social security number, password, first and last name, Role(Admin, Doctor, Nurse) and region. The system checks for duplicate social security numbers before saving.

Choose 5 to show each user along with their granted permissions.

Choose 6 to add new hospitals. Choose region from a numbered list. Enter: city, adress, postal code and hospital name. The data is saved to data/locations.txt.

Choose 7 to accept or deny pending patient registrations. Displays a list of all pending users. You will be asked to Enter the social security number of a pending user. Choose A to accept(promotes to Patient) or D to deny(removes the user).

Choose 8 to display a list of past appointments that do not yet have a journal entry. Choose the appointment number. Write a note describing the vist. The note is saved to the patient's journal(data/journal.txt).

Choose 9 to see your own journal notes(entries they have written.)

Choose Q to logout 


Implementations:
Role-based user system (Admin, Doctor, Nurse, Patient)
File-based data persistence using .txt files
Authentication(login and registration)
Menu-driven console interface
Seperation of logic through OOP (User, Appointment, Journal, etc.)
Permisson system via enums (Role, Permissions, Regions)(work in progress)

Current features:
User registration and login - patients can register, admins approve accounts
Appointment scheduling - book and view appointments
Medical journal management - doctors write notes, patients view them
Hospital and region management - add new locations
Persistant storage - all data saved to text files

Future improvements
Finish permissionsystem which would make it possible to get diffrent menu options for different roles and to make sure that not all diffrent users can do everything in the program as they can right now when everyone have all the permissions.