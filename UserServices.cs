namespace CAB201Take3;
/// <summary>
/// This parent class is used to define the common operations that all users can perform.
/// </summary>
public class UserServices : IUserServices
{
    /// <summary>
    /// Method to change the password of a user.
    /// </summary>
    /// <param name="loggedUser">User conducting action</param>
    public void ChangePassword(User loggedUser)
    {
        CmdLineUI.DisplayMessage("Enter new password:");
        loggedUser.SetPassword(CmdLineUI.GetString());
        CmdLineUI.DisplayMessage("Password has been changed.");
    }

    /// <summary>
    /// Method to log out a user.
    /// </summary>
    /// <param name="loggedUser">User conducting action</param>
    public void LogOut(User loggedUser)
    {
        string formattedName = CmdLineUI.GetFormattedNameLower(loggedUser);
        CmdLineUI.DisplayMessage($"{formattedName} {loggedUser.Name} has logged out.");

    }

    /// <summary>
    /// Base method to display the details of a user.
    /// Contains common details that all users have.
    /// </summary>
    /// <param name="loggedUser">User conducting action</param>
    public virtual void DisplayDetails(User loggedUser)
    {
        CmdLineUI.DisplayMessage("Your details.");
        CmdLineUI.DisplayMessage($"Name: {loggedUser.Name}");
        CmdLineUI.DisplayMessage($"Age: {loggedUser.Age}");
        CmdLineUI.DisplayMessage($"Mobile phone: {loggedUser.Mobile}");
        CmdLineUI.DisplayMessage($"Email: {loggedUser.Email}");
    }
    
    
}