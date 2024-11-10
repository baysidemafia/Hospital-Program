using System.Text.RegularExpressions;
namespace CAB201Take3;
/// <summary>
/// This is the parent class for all users
/// it defines the common attributes that all users have
/// </summary>
public abstract class User 
{
    /// <summary>
    /// The name of the user, with a getter.
    /// </summary>
    public string Name { get;}
    /// <summary>
    /// The age of the user, with a getter.
    /// </summary>
    public int Age { get; }
    /// <summary>
    /// The email of the user, with a getter.
    /// </summary>
    public string Email { get; }
    /// <summary>
    /// The password of the user, with a getter and private setter
    /// </summary>
    public string Password { get;  private set; }
    /// <summary>
    /// The mobile number of the user, with a getter.
    /// </summary>
    public string Mobile { get; }
    /// <summary>
    /// The Constructor for the User class.
    /// </summary>
    /// <param name="name">The name of the user</param>
    /// <param name="age">The age of the user</param>
    /// <param name="mobile">The mobile of the user</param>
    /// <param name="email">the email of a user</param>
    /// <param name="password">the password of a user</param>
    protected User(string name, int age, string mobile, string email, string password)
    {
        Name = name;
        Age = age;
        Mobile = mobile;
        Email = email;
        Password = password;
    }
    /// <summary>
    ///  Method to set the password
    /// </summary>
    /// <param name="newPassword">new password</param>
    internal void SetPassword(string newPassword)
    {
        Password = newPassword;
    }
   
}

