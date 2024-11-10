namespace CAB201Take3;
/// <summary>
/// Class to define the staff services, extending the user services.
/// </summary>
public class StaffServices : UserServices
{
    /// <summary>
    /// Method for staff to select an eligible person for further operations
    /// </summary>
    /// <param name="eligiblePeople">passed list of eligible people</param>
    /// <param name="personType">Type of ppl to be selected e.g. patient</param>
    /// <typeparam name="T">Type is a user</typeparam>
    /// <returns>Eligible Users</returns> 
    protected static T SelectEligiblePerson<T>(List<T> eligiblePeople, string personType) where T : User
    {
        // Create a list of menu items for each eligible person
        List<MenuItem> menuItems = eligiblePeople.Select(person => new MenuItem(person.Name, () => { })).ToList();
        var (titles, actions) = CmdLineUI.GetMenuOptions(menuItems);
        // Get the option selected by the user
        int option = CmdLineUI.GetOption(
            false,
            $"Please select your {personType}:",
            CmdLineUI.DisplayErrorAgain,
            "Supplied value is out of range",
            titles
        );
        // Return the selected person
        if (option >= 0 && option < eligiblePeople.Count)
        {
            return eligiblePeople[option];
        }
        // Return null if no person is selected
        return null!;
    }
    /// <summary>
    /// Method to display the details of a staff, adding the staff ID.
    /// </summary>
    /// <param name="loggedUser"></param>
    public override void DisplayDetails(User loggedUser)
    {
        base.DisplayDetails(loggedUser);

        if (loggedUser is Staff staff)
        {
            CmdLineUI.DisplayMessage($"Staff ID: {staff.StaffID}");
        }
        
    }
   

  
}