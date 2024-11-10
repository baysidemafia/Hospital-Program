using System.Text.RegularExpressions;

namespace CAB201Take3;

static class CmdLineUI
{
    /// <summary>
    /// displays a blank line
    /// </summary>
    public static void DisplayMessage()
    {
        Console.WriteLine();
    }
    // overladed method: Displays a string message to the command line
    /// <summary>
    /// displays message to command line
    /// </summary>
    /// <param name="msg">Message to display</param>
    public static void DisplayMessage(string msg)
    {
        Console.WriteLine(msg);
    }
    // Method to display an error message to the screen
    /// <summary>
    /// Displays an error message to the screen.
    /// </summary>
    /// <param name="msg">The error message to display</param>
    public static void DisplayError(string msg)
    {
        Console.WriteLine("#####");
        Console.WriteLine($"#Error - {msg}.");
        Console.WriteLine("#####");
    }
    // another method to display an error message to the screen
    /// <summary>
    /// Displays an error message to the screen and prompts the user to try again
    /// </summary>
    /// <param name="msg">Error message to display</param>
    public static void DisplayErrorAgain(string msg)
    {
        Console.WriteLine("#####");
        Console.WriteLine($"#Error - {msg}, please try again.");
        Console.WriteLine("#####");
    }
    
    // Reads user input from the console
    /// <summary>
    /// Reads in a string from console and returns it
    /// </summary>
    /// <returns>A string representation of the users input</returns>
    public static string GetString()
    {
        string input = Console.ReadLine();
        return input;
    }
    // Reads user input as a string, converts it to an integer
    /// <summary>
    /// Reads in a string from console, converts it to a Int32
    /// and returns the converted value
    /// </summary>
    /// <returns>A Int32 representation of the users input</returns>
    public static int GetInt()
    {
        string input = Console.ReadLine();
        int i = int.Parse(input);
        return i;
    }
    // prompts for an interger input, and tries to parse it, returns -1 if it fails
    /// <summary>
    /// Print the message and then read in a string from console, converts it to a Int32
    /// and returns the converted value
    /// </summary>
    /// <param name="msg">A message to print out</param>
    /// <returns>An Int32 representation of the users input</returns>      
    private static int GetInt(string msg)
    {
        Console.WriteLine($"{msg}");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int result))
        {
            return result;
        }
        
        return -1;
    }

    // // Loops until a valid date in the format "HH:mm dd/MM/yyyy" is entered
    /// <summary>
    /// Method to get a date from the user
    /// It will keep prompting the user until a valid date is entered
    /// The date must be in the format "HH:mm dd/MM/yyyy"
    /// </summary>
    /// <returns>valid DateTime</returns>
    public static DateTime GetDate()
    {
        DateTime dateTime = default;
        bool isValid = false;

        while (!isValid) // Loop until a valid date is entered
        {
            Console.WriteLine("Please enter a date and time (e.g. 14:30 31/01/2024).");
            string input = Console.ReadLine();

            string format = "HH:mm dd/MM/yyyy";

            if (DateTime.TryParseExact(input, format, 
                    System.Globalization.CultureInfo.InvariantCulture, 
                    System.Globalization.DateTimeStyles.None, out dateTime))
            {
                isValid = true;
            }
            else // If the date is invalid
            {
                Console.WriteLine("#####");
                Console.WriteLine($"#Error - Supplied value is not a valid DateTime.");
                Console.WriteLine("#####");
            }
        }

        return dateTime; // Return the valid date
    }
    // method for displaying a dynamic menu and getting user input
   /// <summary>
   /// This method will display a menu to the user and prompt them to select an option
   /// It is dynamic and can take any number of options, and varying behaviours
   /// It can be used to display the various menus in the program
   /// </summary>
   /// <param name="breakOnInvalid">Bool to see if it returns control on invalid entry</param> 
   /// <param name="title">title to be displayed</param>
   /// <param name="errorHandler">The error behaviour</param>
   /// <param name="errorMessage">The error message</param>
   /// <param name="options">Options to select from</param>
   /// <returns>selected option</returns>
   /// <remarks></remarks>
  
    public static int GetOption(bool breakOnInvalid, string title, Action<string> errorHandler, 
        string errorMessage, params object[] options)
    {
        if (options.Length <= 0)
        {
            return -1; // Handle no options
        }
        // Formatting menu and printing the title and options
        
        // Initially Print Menu; Only title and Options. This is because this program has two
        // distinct menu behaviours with one reprinting top to bottom, and the other
        // only displaying an error, and re-prompting for user input
        Console.WriteLine(title); 
        int digitsNeeded = (int)(1 + Math.Floor(Math.Log10(options.Length))); // Formatting for consistent display
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{(i + 1).ToString().PadLeft(digitsNeeded)}. {options[i]}"); // Displaying each option
        }
        
        
        // Input validation loop
        while (true)
        {
            // Ask the user for input
            int option = GetInt($"Please enter a choice between 1 and {options.Length}.");

            // Check for valid input
            if (option < 1 || option > options.Length)
            {
                // Display Error Message
                errorHandler(errorMessage);
                
                if (breakOnInvalid)
                {
                    return -1; // break loop hand control back
                }
                
                // Behaviour 2: If we dont return control, just prompt for input again
                continue; // Re-prompt the user
            }
    
            return option - 1; // Return valid option index
        }
            
        
    }
    /// <summary>
    /// Method to get the formatted name of user type with a space between each word
    /// In this case, the first letter of each word is capitalised
    /// </summary>
    /// <param name="user">User type</param>
    /// <returns>formatted string</returns>
    public static string  GetFormattedNameUpper(User user)
    {
        string userType = user.GetType().Name;
        string formattedName = Regex.Replace(userType, "(\\B[A-Z])", " $1");
        return Regex.Replace(formattedName, "(?<=\\s)[A-Z]", m => m.Value.ToUpper());
    }
    /// <summary>
    /// Method to get the formatted name of user type with a space between each word
    /// In this case, the first letter of each word is lower case
    /// </summary>
    /// <param name="user">user type</param>
    /// <returns>formatted string</returns>
    public static string  GetFormattedNameLower(User user)
    {
        string userType = user.GetType().Name;
        string formattedName = Regex.Replace(userType, "(\\B[A-Z])", " $1");
        return Regex.Replace(formattedName, "(?<=\\s)[A-Z]", m => m.Value.ToLower());
        
    }
    /// <summary>
    /// Method to get the menu options from a list of menu items
    /// It returns a tuple of two arrays, one of titles and one of actions
    /// </summary>
    /// <param name="menuItems">List of Menu items (strings and methods) </param>
    /// <returns>titles and actions</returns>
    public static (string[] Titles, Action[] Actions) GetMenuOptions(List<MenuItem> menuItems)
    {
        return (
            menuItems.Select(item => item.Title).ToArray(),
            menuItems.Select(item => item.Action).ToArray()
        );
    }
}