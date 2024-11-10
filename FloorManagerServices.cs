namespace CAB201Take3;
/// <summary>
/// The FloorManagerServices class is a subclass of the StaffServices class and represents the services
/// that a floor manager can perform. It implements the IFloorManagerServices interface
/// which defines the operations that a floor manager can perform.
/// This setup allows for the capability of a floor manager to be extended in the future.
/// It also allows for a new user to do multiple roles.
/// </summary>
public class FloorManagerServices : StaffServices, IFloorManagerServices
{
    /// <summary>
    ///  The data handler for the floor manager services
    /// </summary>
    private readonly IFloorManagerHospital _dataHandler;
    
    /// <summary>
    ///  Constructor for the FloorManagerServices class
    ///  Initialises the data handler for the floor manager services
    /// </summary>
    /// <param name="floorManagerHospital">Floor managers DB permissions</param>
    public FloorManagerServices(IFloorManagerHospital floorManagerHospital) 
    {
        _dataHandler = floorManagerHospital;
    }
    
    /// <summary>
    /// Method to Display details of the Floor Manager
    /// </summary>
    /// <param name="loggedUser">The logged user (Floor manager)</param>
    public override void DisplayDetails(User loggedUser) //Displaying floor managers details
    {
        base.DisplayDetails(loggedUser);
        
        if (loggedUser is FloorManager floorManager)
        {
            CmdLineUI.DisplayMessage($"Floor: {floorManager.Floor.FloorNumber}.");
        }
    }
    /// <summary>
    /// Method to assign a room to a patient
    /// It performs checks to ensure that this operations is possible
    /// It then prompts the user to select a patient and a room
    /// before assigning the room to the patient
    /// </summary>
    /// <param name="floorManager">Floor manager assigning the room</param>
    public void AssignRoomToPatient(FloorManager floorManager)
    {
        bool availableRoomsOnFloor = _dataHandler.HasAvailableRoomsOnFloor(floorManager.Floor.FloorNumber);
        
        if (!availableRoomsOnFloor) // reject further operations if there are no rooms available
        {
            CmdLineUI.DisplayError("All rooms on this floor are assigned");
            return;
        }

        List<Patient> registeredPatients = _dataHandler.GetAllPatients();
        
        if (registeredPatients.Count == 0) // reject further operations if there is no patients anyway
        {
            CmdLineUI.DisplayMessage("There are no registered patients."); 
            return;
        }

        List<Patient> checkedInPatientsWithoutRoom = _dataHandler.GetCheckedPatientsWithoutRoom();
        
        if (checkedInPatientsWithoutRoom.Count == 0) // reject if there is no eligible patients
        {
            CmdLineUI.DisplayMessage("There are no checked in patients.");
            return;
        }
        // Method to select a patient from a list of eligible patients, checked in and without a room
        Patient selectedPatient = SelectEligiblePerson(checkedInPatientsWithoutRoom, "patient");
        // Get available rooms on the floor
        List<int> availableRooms = _dataHandler.GetAvailableRooms(floorManager.Floor.FloorNumber);
        
        while (true) // loop until a valid room is selected
        {
            CmdLineUI.DisplayMessage($"Please enter your room (1-10):"); // prompt for room
            int roomNumber = CmdLineUI.GetInt();

            if (roomNumber < 1 || roomNumber > 10) // validate entry
            {
                CmdLineUI.DisplayErrorAgain("Supplied value is out of range");
                continue;
            }

            if (!availableRooms.Contains(roomNumber)) // validate room availability
            {
                CmdLineUI.DisplayErrorAgain("Room has been assigned to another patient");
                continue;
            }
            // if all checks pass, assign the room to the patient
            _dataHandler.AssignRoomToPatient(selectedPatient, floorManager.Floor.FloorNumber, roomNumber);
            CmdLineUI.DisplayMessage($"Patient {selectedPatient.Name} has been assigned to room number {roomNumber} on floor {floorManager.Floor.FloorNumber}.");
            return;
        }
    }
    /// <summary>
    /// Method to assign a surgery to a checked in patient
    /// It performs checks to ensure that this operations is possible
    /// It then prompts the user to select a patient and a surgeon
    /// Before assigning the surgeon to the patient
    /// </summary>
    /// <param name="floorManager">Floor manager assigning the surgery</param>
    public void AssignSurgeryToCheckedInPatient(FloorManager floorManager)
    {
        List<Patient> registeredPatients = _dataHandler.GetAllPatients();
        
        if (registeredPatients.Count == 0) // reject further operations if there are no patients
        {
            CmdLineUI.DisplayMessage("There are no registered patients.");
            return;
        }
        
        List<Patient> patientsWithoutSurgeon = _dataHandler.GetPatientWithoutSurgeon();
        
        if (patientsWithoutSurgeon.Count == 0) // reject further operations if there are no eligible patients
        {
            CmdLineUI.DisplayMessage("There are no patients ready for surgery.");
            return;
        }
        // Method to select a patient from a list of eligible patients, checked in and without a surgeon
        Patient selectedPatient = SelectEligiblePerson(patientsWithoutSurgeon, "patient");
        // Get available surgeons
        List<Surgeon> availableSurgeons = _dataHandler.GetAvailableSurgeons();
        // Method to select a surgeon from a list of eligible surgeons
        Surgeon selectedSurgeon = SelectEligiblePerson(availableSurgeons, "surgeon");
        // Get surgery date
        DateTime surgeryDateTime = CmdLineUI.GetDate();
        // Assign the surgeon to the patient
        _dataHandler.AssignSurgeonToPatient(selectedPatient, selectedSurgeon, surgeryDateTime);
        // Display the assignment
        CmdLineUI.DisplayMessage($"Surgeon {selectedSurgeon.Name} has been assigned to patient {selectedPatient.Name}.");
        CmdLineUI.DisplayMessage($"Surgery will take place on {surgeryDateTime.ToString("HH:mm dd/MM/yyy")}.");
    }
    /// <summary>
    /// Method to unassign a room from a checked in patient
    /// Performs checks to ensure that this operation is possible
    /// It then prompts the user to select a patient
    /// Before unassigning the room from the patient
    /// </summary>
    /// <param name="floorManager">Floor manager unassigning the room</param>
    public void UnassignRoomToCheckedPatient(FloorManager floorManager)
    {
        List<Patient> patientsWithRooms = _dataHandler.GetPatientsWithAssignedRooms();
        
        if (patientsWithRooms.Count == 0) // reject further operations if there are no patients with rooms
        {
            CmdLineUI.DisplayMessage("There are no patients ready to have their rooms unassigned.");
            return;
        }
        // Method to select a patient from a list of eligible patients, checked in and with a room
        Patient selectedPatient = SelectEligiblePerson(patientsWithRooms, "patient");
        // Get the floor and room objects for the patient
        var (floor, room) = _dataHandler.GetRoomFloorForPatient(selectedPatient);
        
        if (room !=null && floor !=null)
        {
            _dataHandler.UnassignRoomFromPatient(floorManager.Floor.FloorNumber, selectedPatient); // actually perform database operation
            CmdLineUI.DisplayMessage($"Room number {room.RoomNumber} on floor {floorManager.Floor.FloorNumber} has been unassigned.");
        }
        
    }
    
}