namespace CAB201Take3;
// interface implemented by hospital



public interface IDataBase
{
    void RegisterUser(User user);

    List<Patient> GetAllPatients();

    List<Patient> GetCheckedPatientsWithoutRoom();

    bool IsCheckedIn(Patient patient);

    void SetCheckedInStatus(Patient patient, bool status);

    bool IsSurgeryCompleted(Patient patient);

    List<Surgeon> GetAvailableSurgeons();

    List<Patient> GetPatientWithoutSurgeon();

    void AssignSurgeonToPatient(Patient patient, Surgeon surgeon, DateTime surgeryDateTime);

    string? GetAssignedSurgeon(Patient patient);

    DateTime? GetSurgeryDateForPatient(Patient patient);

    List<Patient> GetPatientsForSurgeon(Surgeon surgeon);

    List<Patient> GetPatientsWithAssignedRooms();

    bool IsFloorAvailable(int floor);

    bool AreAllFloorsManaged();
    

    Floor GetFloor(int floorNumber);

    bool HasAvailableRoomsOnFloor(int floorNumber);

    List<int> GetAvailableRooms(int floorNumber);

    void AssignRoomToPatient(Patient patient, int floorNumber, int roomNumber);

    void UnassignRoomFromPatient(int floorNumber, Patient patient);

    (Floor?, Room?) GetRoomFloorForPatient(Patient patient);

    void ConductSurgery(Patient patient);

    bool IsStaffIdUnique(int staffId);

    bool IsEmailUnique(string email);
    

    User? FindUserByEmail(string email);
    
    bool IsThereRegisteredUsers();

}
