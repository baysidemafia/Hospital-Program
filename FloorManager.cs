namespace CAB201Take3;
/// <summary>
/// The FloorManager class is a subclass of the Staff class and represents a floor manager in the hospital.
/// </summary>
public class FloorManager : Staff 
{
    /// <summary>
    /// Field for Floor object, with a getter.
    /// </summary>
    public Floor Floor { get; }
    
    /// <summary>
    /// The Constructor for the FloorManager class.
    /// </summary>
    /// <param name="name">The floor managers name</param>
    /// <param name="age">the floor managers age</param>
    /// <param name="mobile">the floor managers mobile</param>
    /// <param name="email">the floor managers email</param>
    /// <param name="password">the floor managers password</param>
    /// <param name="staffID">the floor managers staffID</param>
    /// <param name="floor">the floow managers assinged floor</param>
    public FloorManager(string name, int age, string mobile, string email, string password, int staffID, Floor floor) 
        : base(name, age, mobile, email, password, staffID)
    {
        Floor = floor;
    }
}