namespace CAB201Take3;
/// <summary>
/// The Hospital class is the main database for the system.
/// It stores a range of data including users, schedules events, floors (which contain rooms), and
/// provides methods to interact with the data.
/// </summary>
public class Hospital : IDataBase
{
    // Private fields to store the data
    // Stores all registered users
    private readonly List<User> _registeredUsers = new List<User>();
    // Stores all registered patients
    private readonly List<Patient> _registeredPatients = new List<Patient>();
    // Stores all registered surgeons
    private readonly List<Surgeon> _registeredSurgeons = new List<Surgeon>();
    // Stores all registered floor managers
    private readonly List<FloorManager> _registeredFloorManagers = new List<FloorManager>();
    // Stores surgeries
    private readonly Dictionary<Patient, (Surgeon surgeon, DateTime surgeryDateTime, bool surgeryCompleted)> _surgeries = new Dictionary<Patient, (Surgeon, DateTime, bool)>();
    // Stores checked in patients
    private readonly Dictionary<Patient, bool> _checkedInPatients = new Dictionary<Patient, bool>(); // Track checked-in status
    // Stores floor managers assigned to floors
    private readonly Dictionary<int, FloorManager> _assignedFloors = new Dictionary<int, FloorManager>();
    // Stores all floors
    private readonly List<Floor> _floors;
    
    /// <summary>
    /// Constructor to initialise the hospital with 6 floors, these floors contain 10 rooms
    /// </summary>
    public Hospital()
    {
        _floors = new List<Floor>();
        for (int i = 1; i < 7; i++)
        {
            _floors.Add(new Floor(i));
        }
    }
    
    /// <summary>
    /// Registers the user to the main database which is utilised for login purposes
    /// before calling a method to sort into respective types.
    /// </summary>
    /// <param name="user">User object to be registered</param>
    public void RegisterUser(User user)
    {
        _registeredUsers.Add(user);
        SortByUserType(user);
        CmdLineUI.DisplayMessage(
                $"{user.Name} is registered as a {user.GetType().Name.ToLower()}."); //need to update
    }
    /// <summary>
    /// Sorts the users into their respective lists
    /// </summary>
    /// <param name="user">User object to be sorted</param>
    private void SortByUserType(User user)
    {
        switch (user)
        {
            case Patient patient:
                _registeredPatients.Add(patient);
                break;
            case Surgeon surgeon:
                _registeredSurgeons.Add(surgeon);
                break;
            case FloorManager floorManager:
                _registeredFloorManagers.Add(floorManager);
                AssignFloorManagerToFloor(floorManager, floorManager.Floor.FloorNumber);
                break;
        }
    }

    /// <summary>
    /// Gets a list of all patients in the system
    /// </summary>
    /// <returns>List of registered patients (those with account)</returns>
    public List<Patient> GetAllPatients()
    {
        return _registeredPatients.ToList(); // Return a copy of the list
    }
    /// <summary>
    /// Gets a list of patients who are checked in but do not have a room assigned
    /// </summary>
    /// <returns>Copied list of patients, checked in, without rooms</returns>
    public List<Patient> GetCheckedPatientsWithoutRoom()
    {
        return _checkedInPatients
            .Keys
            .Where(p => IsCheckedIn(p) && GetRoomFloorForPatient(p).Item2 == null).ToList();
    }
    
    /// <summary>
    /// Checks if a patient is checked in.
    /// Searches the dictionary for the patient and returns the flag value.
    /// </summary>
    /// <param name="patient">Patient object requiring determination of
    /// their checked status</param>
    /// <returns>Bool value if checked in or not</returns>
    public bool IsCheckedIn(Patient patient)
    {
        return _checkedInPatients.ContainsKey(patient) && _checkedInPatients[patient];
    }

    /// <summary>
    /// Sets the checked in status of a patient.
    /// </summary>
    /// <param name="patient">Patient object is a field in the dictionary
    /// which references the specific patient object (key)</param>
    /// <param name="status">Bool value for checked in status to be set </param>
    public void SetCheckedInStatus(Patient patient, bool status)
    {
        _checkedInPatients[patient] = status;
    }

    /// <summary>
    /// Checks if a patient has had surgery completed.
    /// </summary>
    /// <param name="patient">Patient object (key) as the dependant variable
    /// for determining surgery status</param>
    /// <returns>Bool value for if surgery is completed or not</returns>
    public bool IsSurgeryCompleted(Patient patient)
    {
        return _surgeries.ContainsKey(patient) && _surgeries[patient].surgeryCompleted;
    }

    /// <summary>
    /// Gets a copied list of all registered surgeons.
    /// This list is mutable and can be changed.
    /// The internal list is not exposed to the outside world.
    /// </summary>
    /// <returns>List of registered surgeons</returns>
    public List<Surgeon> GetAvailableSurgeons()
    {
        return _registeredSurgeons.ToList(); // Return a copy of the list
    }

    /// <summary>
    /// Gets a list of patients who are eligible for surgeon assignment.
    /// This list is mutable and can be changed.
    /// However, the internal list is protected from outside changes.
    /// </summary>
    /// <returns>list of patients eligible for surgeon assignment</returns>
    public List<Patient> GetPatientWithoutSurgeon()
    {
        return _registeredPatients
            .Where(p => IsCheckedIn(p) && GetRoomFloorForPatient(p).Item2 != null && !_surgeries.ContainsKey(p))  // Check if patient is checked in and has a room
            .ToList();
    }

    /// <summary>
    /// Method for actually assigning surgery values. Directly
    /// stores the surgery information inside the hospital database.
    /// </summary>
    /// <param name="patient">Patient object to have surgery</param>
    /// <param name="surgeon">Surgeon object to perform said surgery</param>
    /// <param name="surgeryDateTime">Surgery DateTime value</param>
    public void AssignSurgeonToPatient(Patient patient, Surgeon surgeon, DateTime surgeryDateTime)
    {
        _surgeries[patient] = (surgeon, surgeryDateTime, false);
    }

    /// <summary>
    /// Gets the surgeon assigned to a patient.
    /// </summary>
    /// <param name="patient">Patient is the key in this case, defining
    /// factor in determining who the surgeon is</param>
    /// <returns>Surgeon object whos planned to perform said surgery</returns>
    public string? GetAssignedSurgeon(Patient patient)
    {
        return _surgeries.TryGetValue(patient, out var assignment)
            ? assignment.surgeon.Name 
            : null;
    }

    /// <summary>
    /// Gets just the DateTime value from the surgery information.
    /// </summary>
    /// <param name="patient">patient object is the key here,
    /// the dependant variable in outcome</param>
    /// <returns></returns>
    public DateTime? GetSurgeryDateForPatient(Patient patient)
    {
        return _surgeries.TryGetValue(patient, out var assignment) ? assignment.surgeryDateTime : null;
    }
    /// <summary>
    /// Gets the patients assigned to a surgeon.
    /// </summary>
    /// <param name="surgeon">Surgeon object is the key here,
    /// the dependant variable that determines the patients in an
    /// associative relationship with it.</param>
    /// <returns>List of patients</returns>
    public List<Patient> GetPatientsForSurgeon(Surgeon surgeon)
    {
        return _surgeries.Where(s => s.Value.surgeon == surgeon)
            .Select(s => s.Key)
            .ToList();
    }

    /// <summary>
    /// This method gets the patients with assigned rooms. 
    /// </summary>
    /// <returns>List of patient objects who ARE assigned rooms</returns>
    public List<Patient> GetPatientsWithAssignedRooms()
    {
        return _registeredUsers
            .OfType<Patient>()
            .Where(p => GetRoomFloorForPatient(p).Item2 != null)  // Check if the patient has an assigned room
            .ToList();
    }
    /// <summary>
    /// Checks if a particular floor entry is already managed or not.
    /// Used as validation logic for when a floor manager is registering.
    /// </summary>
    /// <param name="floor">floor number entry that registering
    /// floor manager inputted</param>
    /// <returns>Bool value if a floor is free or not</returns>
    
    public bool IsFloorAvailable(int floor)
    {
        return !_registeredUsers.OfType<FloorManager>().Any(fm => fm.Floor.FloorNumber == floor) && !_assignedFloors.ContainsKey(floor);
    }
    /// <summary>
    /// Simple method that checks if there's even any floors available.
    /// </summary>
    /// <returns>Bool value if there's floors available or not</returns>
    public bool AreAllFloorsManaged()
    {
        return _assignedFloors.Count >= 6;
    }

    /// <summary>
    /// Method for the database interaction that stores
    /// the floor manager to their respective managed floor.
    /// </summary>
    /// <param name="manager">Floor manager object who manages
    /// said floor</param>
    /// <param name="floorNumber">Floor number integer which identifies
    /// the actual instance of floor</param>
    public void AssignFloorManagerToFloor(FloorManager manager, int floorNumber)
    {
        _assignedFloors.Add(floorNumber, manager);
    }
    /// <summary>
    /// Gets the Floor object from the list of floors.
    /// </summary>
    /// <param name="floorNumber">The field that identifies a particular instance</param>
    /// <returns>actual Floor object</returns>
    public Floor GetFloor(int floorNumber)
    {
        return _floors.FirstOrDefault(f => f.FloorNumber == floorNumber)!;
    }
    /// <summary>
    /// Method to see if there are any avaliable rooms on the floor
    /// before allowing a manager to assign to a room.
    /// No point allowing the floor manager to select a patient
    /// and continue the process if there are no rooms available anyway
    /// </summary>
    /// <param name="floorNumber">Field to identify particular floor reference</param>
    /// <returns>Bool value if there is space or not</returns>
    public bool HasAvailableRoomsOnFloor(int floorNumber)
    {
        var floor = GetFloor(floorNumber);
        return !floor.AreAllRoomsOccupied();
    }
    /// <summary>
    /// Method to get a list of available rooms on a floor.
    /// </summary>
    /// <param name="floorNumber">Floor number to identify the floor to be assessed</param>
    /// <returns>List of available rooms</returns>
    public List<int> GetAvailableRooms(int floorNumber)
    {
        var floor = GetFloor(floorNumber);
        return floor.GetAvailableRooms().ToList();
    }
    /// <summary>
    /// Method to assign a room to a patient.
    /// It checks if the room is occupied before assigning the patient.
    /// </summary>
    /// <param name="patient"></param>
    /// <param name="floorNumber"></param>
    /// <param name="roomNumber"></param>
    public void AssignRoomToPatient(Patient patient, int floorNumber, int roomNumber)
    {
        var floor = GetFloor(floorNumber);
        floor.AssignRoomToPatient(roomNumber, patient);

    }
   /// <summary>
   /// Method to unassign a room from a patient.
   /// </summary>
   /// <param name="floorNumber">Floor number the floor manager manages</param>
   /// <param name="patient">Patient to be unassigned</param>
    public void UnassignRoomFromPatient(int floorNumber, Patient patient)
    {
        var floor = GetFloor(floorNumber); 
        floor.UnassignRoom(patient);
    }

    /// <summary>
    /// Method which checks all the floors and their rooms for
    /// our target patient, before capturing the value of
    /// room and floor and returning to calling method.
    /// Allows other methods to use the patient as dependant
    /// variable to figure out their room.
    /// </summary>
    /// <param name="patient">Patient object reference</param>
    /// <returns>Floor and room patient is assigned to</returns>
    public (Floor?, Room?) GetRoomFloorForPatient(Patient patient)
    {
        foreach (var floor in _floors)
        {
            var room = floor.GetRoomForPatient(patient);
            if (room != null)
            {
                return (floor, room); // Return the room if the patient is found
            }
        }
        
        return (null, null); // no room assigned.
    }
   
    /// <summary>
    /// Method for conducting surgery. a.k.a marking
    /// the patient's surgery status as true.
    /// </summary>
    /// <param name="patient">Patient who had surgery</param>
    public void ConductSurgery(Patient patient)
    {
        if (_surgeries.ContainsKey(patient))
        {
            var surgeryInfo = _surgeries[patient];
            _surgeries[patient] = (surgeryInfo.surgeon, surgeryInfo.surgeryDateTime, true);
        }
        
    }
    
    /// <summary>
    /// Checks if staff ID's are unique during registration
    /// </summary>
    /// <param name="staffId">Staff ID of the staff member attempting to register with
    /// said ID value, checks if its unique similar to checking if an account
    /// is trying to register with an email already being used for an account
    /// on the platform</param>
    /// <returns>Bool true means it is unique, false means it is not unique</returns>
    public bool IsStaffIdUnique(int staffId)
    {
        return !_registeredUsers.OfType<Staff>().Any(staff => staff.StaffID == staffId);
    }
    
    /// <summary>
    /// check for duplicate emails, similar to Staff ID check
    /// </summary>
    /// <param name="email">user email</param>
    /// <returns>bool flag/value for uniqueness/duplication</returns>
    public bool IsEmailUnique(string email)
    {
        return !_registeredUsers.Any(u => u.Email == email);
    }
    
    /// <summary>
    /// Method to check if there are any registered users
    /// Rejection of login if there are no registered users
    /// </summary>
    /// <returns>Flag if there is registered users or not</returns>
    public bool IsThereRegisteredUsers()
    {
        return _registeredUsers.Count > 0;
    }
    
    /// <summary>
    /// Method to find user by email
    /// It is used to find the user associated with the email to check credentials
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public User? FindUserByEmail(string email)
    {
        return _registeredUsers.FirstOrDefault(user => user.Email == email);
    }
}




