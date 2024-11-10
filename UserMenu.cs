using System.Text.RegularExpressions;
namespace CAB201Take3;
/// <summary>
/// The abstract UserMenu class is a base class for all user-specific menus once a user has logged in.
/// It defines the common menu items that all users can access (such as displaying their details and changing their password)
/// and requires subclasses to implement role-specific menu actions.
/// </summary>
public abstract class UserMenu
{
    /// <summary>
    /// The user object representing the currently logged in user.
    /// </summary>
    private readonly User _loggedUser;
    /// <summary>
    /// Interface for the operations that users can perform.
    /// </summary>
    private readonly IUserOperations _hospitalOperations;
    /// <summary>
    /// A list of menu items that the user can select from.
    /// This includes both common menu items and role-specific menu items.
    /// </summary>
    protected List<MenuItem> MenuItems { get; } = new List<MenuItem>();
    /// <summary>
    /// Initializes the UserMenu with the logged in user and the operations that the user can perform.
    /// It then adds the common menu items that all users can access.
    /// </summary>
    /// <param name="user">User</param>
    /// <param name="hospitalOperations">User operations</param>
    protected UserMenu(User user, IUserOperations hospitalOperations)
    {
        _loggedUser = user;
        _hospitalOperations = hospitalOperations;
        AddMenuItems();
    }
    /// <summary>
    /// Method to add the common menu items that all users can access.
    /// It injects the unique menu items for each user type.
    /// </summary>
    private void AddMenuItems()
    {
        MenuItems.Add(new MenuItem("Display my details", () => _hospitalOperations.DisplayDetails(_loggedUser)));
        MenuItems.Add(new MenuItem("Change password", () =>  _hospitalOperations.ChangePassword(_loggedUser)));
        
        AddRoleSpecificMenuItems(); //abstract method
        // add logout at end
        MenuItems.Add(new MenuItem("Log out", () => _hospitalOperations.LogOut(_loggedUser)));
    }
    
    /// <summary>
    /// Abstract method to add the menu items that are specific to the user's role.
    /// </summary>
    protected abstract void AddRoleSpecificMenuItems();
    /// <summary>
    /// Method to display the menu to the user.
    /// It is dynamic and displays the menu based on the user's role.
    /// </summary>
    public void DisplayMenu() 
    {
        bool continueMenu = true;
        
        while (continueMenu) // Display the menu until the user logs out
        {
            var (titles, actions) = CmdLineUI.GetMenuOptions(MenuItems);
            CmdLineUI.DisplayMessage();
            string formattedName = CmdLineUI.GetFormattedNameUpper(_loggedUser);
            CmdLineUI.DisplayMessage($"{formattedName} Menu."); // Display the type of user menu heading
            // Display the menu options and get the user's selection
            int option = CmdLineUI.GetOption(true,"Please choose from the menu below:", CmdLineUI.DisplayError,
                "Invalid option selected.", titles);
            // handle the user's selection
            if (option >= 0 && option < actions.Length)
            {
                actions[option](); // Execute the selected action
               
                if (titles[option] == "Log out")
                {
                    continueMenu = false;
                    
                }
            }
            
        }
    }
    
   
}