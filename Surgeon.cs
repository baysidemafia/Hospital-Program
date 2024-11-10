namespace CAB201Take3;
/// <summary>
/// The Surgeon class is a subclass of the Staff class and represents a surgeon in the hospital.
/// </summary>
public class Surgeon : Staff
{
    /// <summary>
    /// The speciality of the surgeon
    /// </summary>
    public string Speciality { get;  }
    
    /// <summary>
    /// Constructor for the Surgeon class.
    /// </summary>
    /// <param name="name">Surgeon name</param>
    /// <param name="age">Surgeon Age</param>
    /// <param name="mobile">Surgeon Mobile</param>
    /// <param name="email">Surgeon Email</param>
    /// <param name="password">Surgeon Password</param>
    /// <param name="staffID">Surgeon Staff ID</param>
    /// <param name="speciality">Surgeon Speciality</param>
    public Surgeon(string name, int age, string mobile, string email, string password, int staffID, string speciality) 
        : base(name, age, mobile, email, password, staffID)
    {
        Speciality = speciality;
    }
   
    

}