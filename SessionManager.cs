using System.Transactions;

namespace CAB201Take3;
/// <summary>
///  Class to manage the session of the user
///  It handles the login process and displays the user menu
///  It also manages the logged in user
/// </summary>
public class SessionManager : ISessionManager
{
    // prefix for the request message
    private const string RequestPrefix = "Please enter in your";
    // stores the logged in user
    private User _loggedUser;
    // Dependencies for the dataHanlder and the menuFactory
    private ILoginHospital _dataHandler;
    private UserMenuFactory _menuFactory;
    
    /// <summary>
    /// Constructor for the SessionManager
    /// Initialises the loggedUser to null and sets the dataHandler and menuFactory
    /// </summary>
    /// <param name="dataHandler">The dataHanlder abstraction to interact with DB</param>
    /// <param name="userMenuFactory">Create menus based on user's role</param>
    public SessionManager(ILoginHospital dataHandler, UserMenuFactory userMenuFactory)
    {
        _loggedUser = null;
        _dataHandler = dataHandler;
        _menuFactory = userMenuFactory;
    }
    /// <summary>
    /// Manages the login process
    /// Displays a login menu and prompts the user for their information
    /// Performs validation and logs the user in if successful before displaying the user menu
    /// </summary>
    public void Login()
    {
        DisplayLoginMenu(); // Display the login menu
        
        if (!AreThereRegisteredUsers()) // Check if there are any registered users
        {
            CmdLineUI.DisplayError("There are no people registered");
            return;
        }
        
        string email = PromptForEmail(); // Prompt for email
        User user = _dataHandler.FindUserByEmail(email); // Find the user by email
        
        if (user == null) // Check if the user is not found
        {
            CmdLineUI.DisplayError("Email is not registered");
            return;
        }
        
        string password = PromptForPassword(); // Prompt for password
        
        if (IsPasswordValid(user, password)) // Check if password is valid
        {
            SetLoggedInUser(user); // Set the logged in user
            DisplayWelcomeMessage(user); // Display welcome message
            DisplayUserMenu(); // Display the user menu
        }
        else
        {
            CmdLineUI.DisplayError("Wrong Password"); // Display error message
        }
        
    }

    /// <summary>
    /// Method to display the login menu
    /// </summary>
    private void DisplayLoginMenu()
    {
        CmdLineUI.DisplayMessage();
        CmdLineUI.DisplayMessage("Login Menu.");
    }

    /// <summary>
    /// Method to check if there are any registered users
    /// </summary>
    /// <returns>Bool value if there is or is not any possible registered user
    /// accounts to be logged into</returns>
    private bool AreThereRegisteredUsers()
    {
        return _dataHandler.IsThereRegisteredUsers();
    }
    /// <summary>
    ///  Method to prompt the user for their email
    /// </summary>
    /// <returns>User input email</returns>
    private string PromptForEmail()
    {
        CmdLineUI.DisplayMessage($"{RequestPrefix} email:");
        return CmdLineUI.GetString();
    }
    /// <summary>
    /// Method to prompt the user for their password
    /// </summary>
    /// <returns>User input password</returns>
    private string PromptForPassword()
    {
        CmdLineUI.DisplayMessage($"{RequestPrefix} password:");
        return CmdLineUI.GetString();
    }
    /// <summary>
    /// Method to check if the password is valid
    /// </summary>
    /// <param name="user">User object associated with the email account</param>
    /// <param name="password">Password of the user object</param>
    /// <returns></returns>
    private bool IsPasswordValid(User user, string password)
    {
        return user.Password == password;
    }
    /// <summary>
    /// Method to set the logged in user
    /// </summary>
    /// <param name="user">Logged User</param>
    private void SetLoggedInUser(User user)
    {
        _loggedUser = user;
    }
    /// <summary>
    /// Method to display the welcome message
    /// </summary>
    /// <param name="user">User to be greeted</param>
    private void DisplayWelcomeMessage(User user)
    {
        CmdLineUI.DisplayMessage($"Hello {user.Name} welcome back.");
        CmdLineUI.DisplayMessage();
    }
    /// <summary>
    /// Method to display the user menu
    /// </summary>
    private void DisplayUserMenu()
    {
        UserMenu menu = _menuFactory.CreateUserMenu(_loggedUser); // Create the user menu
        menu.DisplayMenu(); // Dynamic Dispatch to display the menu
    }
    /// <summary>
    /// Method to log out the user
    /// </summary>
    public void ResetLoggedUser()
    {
        _loggedUser = null!;
    }
    
}
