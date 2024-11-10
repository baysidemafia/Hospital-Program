using System.Collections.Generic; //list
namespace CAB201Take3;
/// <summary>
/// The MenuManager class is responsible for managing the entry layers of the program.
/// It manages the high-level menu layers and the flow of the program after entry.
/// Provides a interface for navigating through menus before a user is logged in.
/// </summary>
public class MenuManager 
{
    // private fields to hold session and registration services
    // ISessionManager and IRegistrationService interfaces are used to minimise coupling
    private readonly ISessionManager _sessionManager;
    private readonly IRegistrationService _registrationService;
    // Lists to hold menu items for the main menu, register user type menu and staff type menu
    private List<MenuItem> _mainMenuItems = new List<MenuItem>();
    private List<MenuItem> _registerUserTypeMenu = new List<MenuItem>();
    private List<MenuItem> _staffTypeMenu = new List<MenuItem>();
    
    /// <summary>
    /// Constructor for the MenuManager class.
    /// Initialises the main menu items, register user type menu items and staff type menu items.
    /// Also initialises the session manager and registration service.
    /// </summary>
    /// <param name="sessionManager">Session manager to handle login operations</param>
    /// <param name="registrationService">Registration service to handle user registration</param>
    public MenuManager( SessionManager sessionManager, RegistrationService registrationService)
    {
        _sessionManager = sessionManager;
        _registrationService = registrationService;
        
        // Initialising main menu items
        _mainMenuItems.Add(new MenuItem("Login as a registered user", Login));
        _mainMenuItems.Add(new MenuItem("Register as a new user", DisplayRegisterTypeMenu));
        _mainMenuItems.Add(new MenuItem("Exit", ExitProgram));
        
        // initialising register as which type of user menu items
        _registerUserTypeMenu.Add(new MenuItem("Patient", () => RegisterUser("patient")));
        _registerUserTypeMenu.Add(new MenuItem("Staff", DisplayStaffType));
        _registerUserTypeMenu.Add(new MenuItem("Return to the first menu", ReturnToFirstMenu));
        
        // initialising register as which type of staff menu items
        _staffTypeMenu.Add(new MenuItem("Floor manager", () => RegisterUser("floor manager")));
        _staffTypeMenu.Add(new MenuItem("Surgeon", () => RegisterUser("surgeon")));
        _staffTypeMenu.Add(new MenuItem("Return to the first menu", ReturnToFirstMenu));
        
    }
    
    /// <summary>
    /// Starts the main menu loop of program.
    /// Displays the header and main menu, then loops until the program is exited.
    /// </summary>
    public void Run()
    {
        // Displays a welcome header
        DisplayHeader();
        // main menu loop that continues until the program is exited
        while (true)
        {
            // resets the logged user when control is returned to the main menu
            _sessionManager.ResetLoggedUser(); // a user cannot be logged in at this point
            // displays the main menu
            DisplayMainMenu();
        }
    }

    /// <summary>
    /// Displays a welcome header for the program.
    /// This message is displayed at the start of the program.
    /// </summary>
    private void DisplayHeader()
    {
        CmdLineUI.DisplayMessage("=================================");
        CmdLineUI.DisplayMessage("Welcome to Gardens Point Hospital");
        CmdLineUI.DisplayMessage("=================================");
    }

    /// <summary>
    /// Displays a menu with the given menu items and prompt message.
    /// Handles the user input and calls the appropriate action.
    /// </summary>
    /// <param name="menuItems">List of menu items to be displayed</param>
    /// <param name="promptMessage">Message to prompt user with</param>
    /// <param name="returnToMainMenuOnInvalidInput">Determines if the menu should
    /// return to main menu on invalid input</param>
    private void DisplayMenu(List<MenuItem> menuItems, string promptMessage, bool returnToMainMenuOnInvalidInput)
    {
        // loop until a valid selection is made
        while (true)
        {
            // get the menu options
            var (titles, actions) = CmdLineUI.GetMenuOptions(menuItems);
            int option = CmdLineUI.GetOption(true, promptMessage, CmdLineUI.DisplayErrorAgain, "Invalid Menu Option", titles);
            // If the option is valid, call the action
            if (option >= 0 && option < actions.Length)
            {
                actions[option]();
                return; // Exit loop after valid selection
            }
            // If the option is invalid, return to main menu if required
            if (returnToMainMenuOnInvalidInput)
            {
                return; // Return to main menu
            }
        }
    }
    /// <summary>
    /// Displays the main menu.
    /// </summary>
    private void DisplayMainMenu()
    {
        DisplayMenu(_mainMenuItems, "Please choose from the menu below:", false);
    }
    /// <summary>
    /// Displays the register user type menu.
    /// </summary>
    private void DisplayRegisterTypeMenu()
    {
        DisplayMenu(_registerUserTypeMenu, "Register as which type of user:", true);
    }
    /// <summary>
    /// Displays the staff type menu.
    /// </summary>
    private void DisplayStaffType()
    {
        DisplayMenu(_staffTypeMenu, "Register as which type of staff:", true);
    }

    /// <summary>
    /// Method to register a user.
    /// It calls the registerUser method from the registration service and passes the userType
    /// </summary>
    /// <param name="userType">The type of user to register</param>
    private void RegisterUser(string userType)
    {
        _registrationService.RegisterUser(userType);
    }
    
    /// <summary>
    /// Method to login to the system.
    /// Calls the login method from the session manager
    /// </summary>
    private void Login()
    {
        _sessionManager.Login();
    }
    /// <summary>
    /// Exits the program
    /// Displays a goodbye message and exits the program
    /// </summary>
    private void ExitProgram()
    {
        CmdLineUI.DisplayMessage("Goodbye. Please stay safe.");
        Environment.Exit(0);
    }
    /// <summary>
    /// Returns control to the first menu.
    /// This is achieved by effectively ending the current method without additional logic.
    /// </summary>
    private void ReturnToFirstMenu()
    {
        // do nothing, just returns control to the first menu (calling method)
    }
}
