namespace CAB201Take3;
/// <summary>
/// This class acts as an abstraction of the hospital database
/// It implements the interfaces for the different hospital roles
/// It controls the flow of data between the user interface and the database
/// </summary>
public class DataHandler : IFloorManagerHospital, ISurgeonHospital, ILoginHospital, IPatientHospital, IRegisterHospital
{
    // Abstraction of hospital database
    private readonly IDataBase _dataBase;
    /// <summary>
    /// Constructor for DataHandler
    /// </summary>
    /// <param name="dataBase">Initialises the database</param>
    public DataHandler(IDataBase dataBase)
    {
        _dataBase = dataBase;
    }
    // Floor Manager Methods
    
    /// <summary>
    /// Method to check if there are available rooms on a floor
    /// </summary>
    /// <param name="floorNumber">Floor number to be assesed</param>
    /// <returns>Flag value for availability</returns>
    public bool HasAvailableRoomsOnFloor(int floorNumber)
    {
        return _dataBase.HasAvailableRoomsOnFloor(floorNumber);
    }
    /// <summary>
    /// Method to get available rooms on a floor
    /// </summary>
    /// <param name="floorNumber">Floor number to be assessed</param>
    /// <returns>List of available rooms</returns>
    public List<int> GetAvailableRooms(int floorNumber)
    {
        return _dataBase.GetAvailableRooms(floorNumber);
    }

    /// <summary>
    /// Method to get all patients
    /// </summary>
    /// <returns>List of patients</returns>
    public List<Patient> GetAllPatients()
    {
        return _dataBase.GetAllPatients();
    }
    /// <summary>
    /// Method to get checked in patients without rooms
    /// </summary>
    /// <returns>List of patients without rooms</returns>
    public List<Patient> GetCheckedPatientsWithoutRoom()
    {
        return _dataBase.GetCheckedPatientsWithoutRoom();
    }
    /// <summary>
    /// Method to assign a room to a patient
    /// </summary>
    /// <param name="patient">Patient being assinged</param>
    /// <param name="floorNumber">Floor number of managed floor</param>
    /// <param name="roomNumber">room to be assigned to</param>
    public void AssignRoomToPatient(Patient patient, int floorNumber, int roomNumber)
    { 
        _dataBase.AssignRoomToPatient(patient, floorNumber, roomNumber);
    }
    /// <summary>
    /// Method to get patients with assigned rooms
    /// </summary>
    /// <returns>List of patients with rooms</returns>
    public List<Patient> GetPatientsWithAssignedRooms()
    {
        return _dataBase.GetPatientsWithAssignedRooms();
    }
    /// <summary>
    /// Method to get patients without surgeons
    /// </summary>
    /// <returns>List of patients without surgeons</returns>
    public List<Patient> GetPatientWithoutSurgeon()
    {
        return _dataBase.GetPatientWithoutSurgeon();
    }
    /// <summary>
    /// Method to assign a surgeon to a patient
    /// </summary>
    /// <param name="patient">patient to have a surgery assigned</param>
    /// <param name="surgeon">Surgeon to be assigned to patient</param>
    /// <param name="surgeryDateTime">time of surgery</param>
    public void AssignSurgeonToPatient(Patient patient, Surgeon surgeon, DateTime surgeryDateTime)
    {
        _dataBase.AssignSurgeonToPatient(patient, surgeon, surgeryDateTime);
    }
    /// <summary>
    /// Method to unassign a room from a patient
    /// </summary>
    /// <param name="floorNumber">managed Floor number</param>
    /// <param name="patient">patient being unassigned</param>
    public void UnassignRoomFromPatient(int floorNumber, Patient patient)
    {
         _dataBase.UnassignRoomFromPatient(floorNumber, patient);
    }
    /// <summary>
    /// Method to get available surgeons
    /// </summary>
    /// <returns>List of available surgeons</returns>
    public List<Surgeon> GetAvailableSurgeons()
    {
        return _dataBase.GetAvailableSurgeons();
    }
    /// <summary>
    /// Method to get the room and floor associated with a patient
    /// </summary>
    /// <param name="patient">Patient who we need the room and floor of assigned room</param>
    /// <returns>Floor and room that patient is on</returns>
    public (Floor?, Room?) GetRoomFloorForPatient(Patient patient)
    {
        return _dataBase.GetRoomFloorForPatient(patient);
    }
    /// <summary>
    /// Method to check if a floor is available
    /// Used to reject floor manager registration if floor is managed
    /// </summary>
    /// <param name="floor">desired floor number</param>
    /// <returns>flag if its taken or not</returns>
    public bool IsFloorAvailable(int floor)
    {
        return _dataBase.IsFloorAvailable(floor);
    }
    /// <summary>
    /// Method to get a floor object given the floor number
    /// </summary>
    /// <param name="floorNumber">Floor number of desired object</param>
    /// <returns>Floor object</returns>
    public Floor GetFloor(int floorNumber)
    {
        return _dataBase.GetFloor(floorNumber);
    }
    
    /// <summary>
    /// Method to get patients for a surgeon
    /// </summary>
    /// <param name="surgeon">Surgeon who needs their patients</param>
    /// <returns>List of patients</returns>
    public List<Patient> GetPatientsForSurgeon(Surgeon surgeon)
    {
        return _dataBase.GetPatientsForSurgeon(surgeon);
    }
    /// <summary>
    /// Method to get the surgery date for a patient
    /// </summary>
    /// <param name="patient">Patient object, to have their surgery date extracted</param>
    /// <returns>DateTime of surgery</returns>
    public DateTime? GetSurgeryDateForPatient(Patient patient)
    {
        return _dataBase.GetSurgeryDateForPatient(patient);
    }
    /// <summary>
    /// Method to check if a surgery is completed
    /// </summary>
    /// <param name="patient">Patient to check their surgery status</param>
    /// <returns>Flag of surgery status</returns>
    public bool IsSurgeryCompleted(Patient patient)
    {
        return _dataBase.IsSurgeryCompleted(patient);
    }
    /// <summary>
    /// Method to conduct a surgery on a patient, marking it as completed
    /// </summary>
    /// <param name="patient">Patient object, to mark their pending surgery</param>
    public void ConductSurgery(Patient patient)
    {
        _dataBase.ConductSurgery(patient);
    }
    
    /// <summary>
    /// Method to check if a patient is checked in
    /// </summary>
    /// <param name="patient">Patient to be checked</param>
    /// <returns>Flag value of checked status</returns>
    public bool IsCheckedIn(Patient patient)
    {
        return _dataBase.IsCheckedIn(patient);
    }
    /// <summary>
    /// Method to change the checked in status of a patient
    /// </summary>
    /// <param name="patient">Patient to have status changed</param>
    /// <param name="status">Desired status</param>
    public void SetCheckedInStatus(Patient patient, bool status)
    {
        _dataBase.SetCheckedInStatus(patient, status);
    }
    /// <summary>
    /// Method to get the assigned surgeon to a patient
    /// </summary>
    /// <param name="patient">Patient to check their surgeon</param>
    /// <returns>Surgeons name</returns>
    public string GetAssignedSurgeon(Patient patient)
    {
        return _dataBase.GetAssignedSurgeon(patient);
    }
    

    /// <summary>
    /// Method to find user by email
    /// In login and register interface
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public User? FindUserByEmail(string email)
    {
        return _dataBase.FindUserByEmail(email);
    }
    /// <summary>
    /// Method to check if there are registered users
    /// In login and register interface
    /// </summary>
    /// <returns></returns>
    public bool IsThereRegisteredUsers()
    {
        return _dataBase.IsThereRegisteredUsers();
        
    }
    /// <summary>
    /// Method to register a user to DB
    /// </summary>
    /// <param name="user">User being registered</param>
    public void RegisterUser(User user)
    {
        _dataBase.RegisterUser(user);
    }
    /// <summary>
    /// Method to check uniqueness of email
    /// </summary>
    /// <param name="email">Email inputted by user</param>
    /// <returns>Flag value of uniqueness</returns>
    public bool IsEmailUnique(string email)
    {
        return _dataBase.IsEmailUnique(email);
    }
    /// <summary>
    /// Check if all floors are managed
    /// </summary>
    /// <returns>Flag value of managed floors availiblity</returns>
    public bool AreAllFloorsManaged()
    {
        return _dataBase.AreAllFloorsManaged();
        
    }
    /// <summary>
    /// Method to check if a staff id is unique
    /// </summary>
    /// <param name="staffId">Staff ID Input to be checked</param>
    /// <returns>Flag value of uniqeuness</returns>
    public bool IsStaffIdUnique(int staffId)
    {
        return _dataBase.IsStaffIdUnique(staffId);
    }
    
}