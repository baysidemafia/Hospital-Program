namespace CAB201Take3;
/// <summary>
/// The Floor class is used to define the floor object.
/// It contains rooms and methods to assign and unassign rooms to patients.
/// Floors are created with 10 rooms, and inside of the hospital.
/// </summary>
public class Floor 
{
    /// <summary>
    ///  Floor number to distinguish between floors
    /// </summary>
    public int FloorNumber { get; }
    
    /// <summary>
    ///  List of rooms contained within a floor
    /// </summary>
    private readonly List<Room> _rooms;
    
    /// <summary>
    /// Constructor for the Floor class
    ///  Initialises the floor with a floor number and creates 10 rooms for the floor.
    /// </summary>
    /// <param name="floorNumber">Floor number needs to be initalised on creation</param>
    public Floor(int floorNumber)
    {
        FloorNumber = floorNumber; // initialise floor number
        _rooms = new List<Room>(); // create list of rooms per floor
        
        // initialise 10 rooms per floor
        for (int i = 0; i < 10; i++)
        {
            _rooms.Add(new Room(i + 1));
        }
    }
    /// <summary>
    /// Method to get available rooms on a floor
    /// </summary>
    /// <returns>List of available rooms</returns>
    public List<int> GetAvailableRooms()
    {
        return _rooms
            .Where(room => !room.IsOccupied)
            .Select(room => room.RoomNumber)
            .ToList();
    }
    
    /// <summary>
    /// Method to get the room object given the room number
    /// </summary>
    /// <param name="roomNumber">roomNumber associated with particular room object</param>
    /// <returns>room object</returns>
    private Room? GetRoom(int roomNumber)
    {
        return _rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
    }
    
    /// <summary>
    /// Method to see if are rooms are occupied on a floor. 
    /// </summary>
    /// <returns>Bool value indicating whether or not there are free rooms</returns>
    public bool AreAllRoomsOccupied()
    {
        return _rooms.All(room => room.IsOccupied);
    }
    
    /// <summary>
    /// Method to assign a room to a patient
    /// If the room is not occupied, assign the patient to the room
    /// </summary>
    /// <param name="roomNumber">Room Number to be assigned</param>
    /// <param name="patient">Patient to be assigned</param>
    public void AssignRoomToPatient(int roomNumber, Patient patient)
    {
        var room = GetRoom(roomNumber);
        if (room != null && !room.IsOccupied) // if room exists and is not occupied
        {
            room.AssignPatient(patient); // assign the patient to the room
        }
    }
    /// <summary>
    /// Method to unassign a room from a patient
    /// </summary>
    /// <param name="patient">Patient whos room is being vacated</param>
    public void UnassignRoom(Patient patient)
    {
        var room = GetRoomForPatient(patient); // get the room associated with the patient
        if (room != null) // if the room exists
        {
            room.VacateRoom();  // Vacate the room
        }
        
    }
    /// <summary>
    /// Method to get the room associated with a patient
    /// Iterates through rooms to find the room with the patient
    /// </summary>
    /// <param name="patient">Patient to find room for</param>
    /// <returns>Room patient is assigned to</returns>
    public Room? GetRoomForPatient(Patient patient)
    {
        return _rooms.FirstOrDefault(room => room.Occupant == patient);
    }

    
    
}