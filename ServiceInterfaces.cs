namespace CAB201Take3;
// interfaces implemented by the services classes


/// <summary>
/// Interface to define the capabilities of the patient services.
/// </summary>
public interface IPatientServices : IUserServices
{
    void CheckedOption(Patient patient );

    void SeeRoom( Patient patient);

    void SeeSurgeon(Patient patient );

    void SeeSurgeryDetails( Patient patient);

    bool IsCheckedIn(Patient patient);
}

/// <summary>
///  Interface to define the capabilities of the floor manager services.
/// </summary>
public interface IFloorManagerServices : IUserServices
{
    void AssignRoomToPatient(FloorManager floorManager);

    void AssignSurgeryToCheckedInPatient(FloorManager floorManager);

    void UnassignRoomToCheckedPatient(FloorManager floorManager);
    
}

/// <summary>
///  Interface to define the capabilities of the surgeon services.
/// </summary>

public interface ISurgeonServices : IUserServices
{
    void DisplayAssignedPatients(Surgeon surgeon);

    void SurgeonSchedule(Surgeon surgeon);

    void PerformSurgery(Surgeon surgeon);
}

/// <summary>
///  Interface to define the capabilities of the all user services.
/// </summary>
public interface IUserServices
{
    void DisplayDetails(User loggedUser );

    void ChangePassword(User loggedUser );

    void LogOut(User loggedUser);
}
