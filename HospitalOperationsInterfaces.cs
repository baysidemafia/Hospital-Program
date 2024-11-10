namespace CAB201Take3;
// Interfaces implemented by the Hospital operation classes

/// <summary>
/// Interface to define the user operations
/// </summary>
public interface IUserOperations
{
    void DisplayDetails(User user);

    void ChangePassword(User user);

    void LogOut(User user);
}


/// <summary>
/// Interface to define the patient operations
/// </summary>
public interface IPatientOperations : IUserOperations
{
    void CheckedOption(Patient patient);

    void SeeRoom(Patient patient);

    void SeeSurgeon(Patient patient);

    void SeeSurgeryDetails(Patient patient);

    bool IsCheckedIn(Patient patient);
}
/// <summary>
/// interface to define the floor manager operations
/// </summary>
public interface IFloorManagerOperations : IUserOperations
{
    void AssignRoomToPatient(FloorManager floorManager);

    void AssignSurgeryToCheckedInPatient(FloorManager floorManager);

    void UnassignRoomToCheckedPatient(FloorManager floorManager);
    
}
/// <summary>
/// interface to define the surgeon operations
/// </summary>
public interface ISurgeonOperations : IUserOperations
{
    void DisplayAssignedPatients(Surgeon surgeon);

    void SurgeonSchedule(Surgeon surgeon);

    void PerformSurgery(Surgeon surgeon);
}

