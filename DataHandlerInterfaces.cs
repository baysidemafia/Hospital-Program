namespace CAB201Take3;
// interfaces implemented in DataHandler

/// <summary>
/// Interface to define the capabilities of the floor manager in the hospital DB
/// </summary>
public interface IFloorManagerHospital 
{
    bool HasAvailableRoomsOnFloor(int floorNumber);

    List<int> GetAvailableRooms(int floorNumber);

    List<Patient> GetAllPatients();

    List<Patient> GetCheckedPatientsWithoutRoom();

    void AssignRoomToPatient(Patient patient, int floorNumber, int roomNumber);

    List<Patient> GetPatientsWithAssignedRooms();

    List<Patient> GetPatientWithoutSurgeon();

    void AssignSurgeonToPatient(Patient patient, Surgeon surgeon, DateTime surgeryDateTime);

    void UnassignRoomFromPatient(int floorNumber, Patient patient);

    List<Surgeon> GetAvailableSurgeons();

    (Floor?, Room?) GetRoomFloorForPatient(Patient patient);
    

}

/// <summary>
/// Interface to define the capabilities of the surgeon in the hospital DB
/// </summary>
public interface ISurgeonHospital
{
    List<Patient> GetPatientsForSurgeon(Surgeon surgeon);

    DateTime? GetSurgeryDateForPatient(Patient patient);

    bool IsSurgeryCompleted(Patient patient);

    void ConductSurgery(Patient patient);

}

/// <summary>
/// Interface to define the capabilities of the patient in the hospital DB
/// </summary>
public interface IPatientHospital 
{
    bool IsCheckedIn(Patient patient);

    bool IsSurgeryCompleted(Patient patient);

    void SetCheckedInStatus(Patient patient, bool status);

    (Floor?, Room?) GetRoomFloorForPatient(Patient patient);
    
    string GetAssignedSurgeon(Patient patient);

    DateTime? GetSurgeryDateForPatient(Patient patient);

}

/// <summary>
/// Interface to define the capabilities of the Registration Service in the hospital DB
/// </summary>

public interface IRegisterHospital
{
    void RegisterUser(User user);
    
    bool AreAllFloorsManaged();
    
    bool IsEmailUnique(string email);
    
    bool IsStaffIdUnique(int staffId);
    
    Floor GetFloor(int floorNumber);
    
    bool IsFloorAvailable(int floor);
    
}
/// <summary>
///  Interface to define the capabilities of the session manager in the hospital DB
/// </summary>

public interface ILoginHospital
{
    User? FindUserByEmail(string email);
    
    bool IsThereRegisteredUsers();
    
}