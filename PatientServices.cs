namespace CAB201Take3;
/// <summary>
/// Class to handle the services for the patient
/// It represents the services that a patient can perform
/// It implements the IPatientServices interface which
/// defines the operations that a patient can perform
/// This setup allows for the capability of a patient to be extended in the future
/// And allows for a new user to do multiple roles
/// </summary>
public class PatientServices : UserServices, IPatientServices
{
    /// <summary>
    ///  The data handler for the patient services
    /// </summary>
    private readonly IPatientHospital _dataHandler;
    /// <summary>
    ///  Constructor for the PatientServices class
    /// </summary>
    /// <param name="patientHospital">Patient data handler</param>
    public PatientServices(IPatientHospital patientHospital) 
    {
        _dataHandler = patientHospital;
    }
    /// <summary>
    /// Method to Display details of the Patient
    /// </summary>
    /// <param name="user">user (patient) to have their details displayed</param>
    public override void DisplayDetails(User user)
    {
        base.DisplayDetails(user);
    }
    
    /// <summary>
    /// Method to check if a patient is checked in
    /// It it used to change the title option in patient menu
    /// </summary>
    /// <param name="patient">Patient to check status for</param>
    /// <returns>Flag for checked in status</returns>
    public bool IsCheckedIn(Patient patient)
    {
        return _dataHandler.IsCheckedIn(patient);
    }
    /// <summary>
    /// Method to change the checked in status of a patient
    /// It also checks the respective validations for check in and check out
    /// 
    /// </summary>
    /// <param name="patient"></param>
    public void CheckedOption(Patient patient)
    {
        
        if (!_dataHandler.IsCheckedIn(patient)) // if they aren't checked in, either allow check in or validate surgery status
        {
            // if surgery is already done reject checkin
            if (_dataHandler.IsSurgeryCompleted(patient))
            {
                CmdLineUI.DisplayMessage("You are unable to check in at this time.");
                return;
            }
            // otherwise, allow check in. Set check in status to true and display message
            _dataHandler.SetCheckedInStatus(patient,true);
            CmdLineUI.DisplayMessage($"{patient.GetType().Name} {patient.Name} has been checked in.");
        }
        else // if patient already checked in
        {
            if (!_dataHandler.IsSurgeryCompleted(patient)) // checked in but pending surgery
            {
                CmdLineUI.DisplayMessage("You are unable to check out at this time.");
                return;
            }
            // allow check out and set the check status to false and display confirmation
            _dataHandler.SetCheckedInStatus(patient,false);
            CmdLineUI.DisplayMessage($"{patient.GetType().Name} {patient.Name} has been checked out.");
        }
        
        
    }
    /// <summary>
    /// Method to allow a patient to see their room
    /// </summary>
    /// <param name="patient">Patient to check their room</param>
    public void SeeRoom(Patient patient)
    {
        
        var (floor, room) = _dataHandler.GetRoomFloorForPatient(patient); // get the floor and room objects
    
        if (floor != null && room != null) // if there is a valid room + floor associated with this patient object
        {
            CmdLineUI.DisplayMessage($"Your room is number {room.RoomNumber} on floor {floor.FloorNumber}.");
        }
        else
        {
            CmdLineUI.DisplayMessage("You do not have an assigned room.");
        }
        
    }
    /// <summary>
    /// Method to allow a patient to see their assigned surgeon
    /// </summary>
    /// <param name="patient">Patient checking their surgeon</param>
    public void SeeSurgeon(Patient patient)
    {
        var assignedSurgeon = _dataHandler.GetAssignedSurgeon(patient); // get the assigned surgeon
    
        if (assignedSurgeon != null) // if there is a valid surgeon object associated with said patient
        {
            CmdLineUI.DisplayMessage($"Your surgeon is {assignedSurgeon}.");
        }
        else
        {
            CmdLineUI.DisplayMessage("You do not have an assigned surgeon.");
        }
        
    }
    /// <summary>
    /// Method to allow a patient to see their surgery details
    /// </summary>
    /// <param name="patient">Patient checking surgery</param>
    public void SeeSurgeryDetails(Patient patient)
    {
        
        var surgeryDateTime = _dataHandler.GetSurgeryDateForPatient(patient); // get the surgery date and time
    
        if (surgeryDateTime != null && surgeryDateTime != DateTime.MinValue)
        {
            CmdLineUI.DisplayMessage($"Your surgery time is {surgeryDateTime?.ToString("HH:mm dd/MM/yyyy")}.");
        }
        else
        {
            CmdLineUI.DisplayMessage("You do not have assigned surgery.");
        } 
        
    }
}