namespace CAB201Take3;
/// <summary>
/// The Room class represents a room in the hospital.
/// It has a room number and can have a patient assigned to it.
/// It also has methods to check if it is occupied, assign a patient to it, and vacate the room.
/// </summary>


public class Room 
{
    /// <summary>
    /// Room number, acts as a unique identifier for the room.
    /// </summary>
    public int RoomNumber { get; }

    /// <summary>
    ///  Patient object representing the patient currently occupying the room.
    /// </summary>
    private Patient? _occupant;
    
    /// <summary>
    /// Property to get the occupant of the room.
    /// </summary>
    public Patient? Occupant => _occupant;
    /// <summary>
    ///  Property to check if the room is occupied.
    /// </summary>
    public bool IsOccupied => Occupant != null;
    /// <summary>
    ///  Constructor for the room object.
    /// </summary>
    /// <param name="roomNumber">Room number is the unique identifier</param>
    public Room(int roomNumber)
    {
        RoomNumber = roomNumber; // room needs a number
        _occupant = null; // start room empty
    }
    
    
    /// <summary>
    /// Method to assign a patient to the room.
    /// Checks if the room is already occupied before assigning the patient.
    /// </summary>
    /// <param name="patient">Patient to be assigned</param>
    public void AssignPatient(Patient patient)
    {
        if (!IsOccupied)
        {
            _occupant = patient;
        }
    }
    /// <summary>
    /// Method to vacate the room.
    /// Resets the occupant to null.
    /// </summary>
    public void VacateRoom()
    {
        _occupant = null;
    }
    
    
}