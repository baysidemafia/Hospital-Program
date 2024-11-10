namespace CAB201Take3;
/// <summary>
/// Class to define the staff object.
/// It inherits from the User class and adds a staff ID.
/// </summary>
public class Staff : User
{
    /// <summary>
    /// Constructor for the staff object.
    /// </summary>
    /// <param name="name">Staff name</param>
    /// <param name="age">Staff age</param>
    /// <param name="mobile">Staff mobile</param>
    /// <param name="email">Staff email</param>
    /// <param name="password">Staff password</param>
    /// <param name="staffID">Staff ID</param>
    public Staff(string name, int age, string mobile, string email, string password, int staffID) 
        : base(name, age, mobile, email, password)
    {
        StaffID = staffID;
    }
    /// <summary>
    ///  Property to get the staff ID.
    /// </summary>
    public int StaffID { get; }
}