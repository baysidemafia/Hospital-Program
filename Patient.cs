using System.Runtime.InteropServices.ComTypes;

namespace CAB201Take3;
/// <summary>
/// Class representing a patient
/// </summary>
public class Patient : User
{
    /// <summary>
    /// Constructor for the patient class
    /// </summary>
    /// <param name="name">Patiens name</param>
    /// <param name="age">Patients age</param>
    /// <param name="mobile">patients mobile</param>
    /// <param name="email">patients email</param>
    /// <param name="password">patients password</param>
    public Patient(string name, int age, string mobile, string email, string password) 
        : base(name, age, mobile, email, password) { }
    
}
