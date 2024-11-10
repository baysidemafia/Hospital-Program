namespace CAB201Take3;

/// <summary>
/// This class is the abstraction for the operations that can be performed by the different types of users
/// It provides a layer of abstraction between the user interface and the services classes
/// The logic for the operations is implemented in the services classes
/// This abstraction allows the menu classes to perform operations without needing to know the implementation details
/// and allows for future scalability such as the introduction of a user who can perform multiple roles
/// </summary>
public class HospitalOperations : IPatientOperations, IFloorManagerOperations, ISurgeonOperations
{
    // Private fields for services to be used by the operations classes
    private readonly IFloorManagerServices _floorManagerServices;
    private readonly ISurgeonServices _surgeonServices;
    private readonly IPatientServices _patientServices;
    private readonly IUserServices _userServices;

    /// <summary>
    /// Constructor for HospitalOperations
    /// It initialises the services classes with the data handler
    /// Providing the menu classes with the ability to perform services through this abstraction
    /// </summary>
    /// <param name="dataHandler">abstraction for database access</param>
    public HospitalOperations(DataHandler dataHandler)
    {
        _patientServices = new PatientServices(dataHandler);
        _floorManagerServices = new FloorManagerServices(dataHandler);
        _surgeonServices = new SurgeonServices(dataHandler);
        _userServices = new UserServices();
    }

    // Floor manager operations
    
    /// <summary>
    /// Method to assign a room to a patient
    /// </summary>
    /// <param name="floorManager">FloorManager conducting action</param>
    public void AssignRoomToPatient(FloorManager floorManager)
    {
        _floorManagerServices.AssignRoomToPatient(floorManager);
    }
    /// <summary>
    /// Method to assign a surgery to a checked in patient
    /// </summary>
    /// <param name="floorManager">FloorManager conducting action</param>
    public void AssignSurgeryToCheckedInPatient(FloorManager floorManager)
    {
        _floorManagerServices.AssignSurgeryToCheckedInPatient(floorManager);
    }

    /// <summary>
    /// Method to unassign a room from a checked in patient
    /// </summary>
    /// <param name="floorManager">FloorManager conducting action</param>
    public void UnassignRoomToCheckedPatient(FloorManager floorManager)
    {
        _floorManagerServices.UnassignRoomToCheckedPatient(floorManager);
    }

    // surgeon operations
    
    /// <summary>
    /// Method to display the patients assigned to a surgeon
    /// </summary>
    /// <param name="surgeon">Surgeon conducting action</param>
    public void DisplayAssignedPatients(Surgeon surgeon)
    {
        _surgeonServices.DisplayAssignedPatients(surgeon);
    }

    /// <summary>
    /// Method to display the schedule of a surgeon
    /// </summary>
    /// <param name="surgeon">Surgeon conducting action</param>
    public void SurgeonSchedule(Surgeon surgeon)
    {
        _surgeonServices.SurgeonSchedule(surgeon);
    }
    /// <summary>
    /// Method to perform surgery on a patient
    /// </summary>
    /// <param name="surgeon">Surgeon conducting action</param>
    public void PerformSurgery(Surgeon surgeon)
    {
        _surgeonServices.PerformSurgery(surgeon);
    }
    
    // patient operations
    
    /// <summary>
    /// Method to check in a patient
    /// </summary>
    /// <param name="patient">Patient conducting actions</param>
    public void CheckedOption(Patient patient)
    {
        _patientServices.CheckedOption(patient);
    }

    /// <summary>
    /// Method to check if a patient is checked in
    /// </summary>
    /// <param name="patient">Patient to be assesed for status</param>
    /// <returns>bool value fo checked in or not</returns>
    public bool IsCheckedIn(Patient patient)
    {
        return _patientServices.IsCheckedIn(patient);
    }

    /// <summary>
    /// Method to see the room of a patient
    /// </summary>
    /// <param name="patient">Patient conducting action</param>
    public void SeeRoom(Patient patient)
    {
        _patientServices.SeeRoom(patient);
    }

    /// <summary>
    /// Method to see the surgeon assigned to a patient
    /// </summary>
    /// <param name="patient">Patient conducting action</param>
    public void SeeSurgeon(Patient patient)
    {
        _patientServices.SeeSurgeon(patient);
    }
    /// <summary>
    /// Method to see the surgery details of a patient
    /// </summary>
    /// <param name="patient">Patient conducting action</param>
    public void SeeSurgeryDetails(Patient patient)
    {
        _patientServices.SeeSurgeryDetails(patient);
    }
    
    // user operations

    /// <summary>
    /// Method to display the details of a user
    /// </summary>
    /// <param name="user">User object to check details</param>
    public void DisplayDetails(User user)
    {
        if (user is Patient)
        {
            _patientServices.DisplayDetails(user);
        }
        else if (user is FloorManager)
        {
            _floorManagerServices.DisplayDetails(user);
        }
        else if (user is Surgeon)
        {
            _surgeonServices.DisplayDetails(user);
        }
    }

    /// <summary>
    /// Method to change the password of a user
    /// </summary>
    /// <param name="user">User object changing password</param>
    public void ChangePassword(User user)
    {
        _userServices.ChangePassword(user);
    }
    /// <summary>
    /// Method to log out a user
    /// </summary>
    /// <param name="user">User being logged out</param>
    public void LogOut(User user)
    {
        _userServices.LogOut(user);
    }
    
}