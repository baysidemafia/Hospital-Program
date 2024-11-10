namespace CAB201Take3;
/// <summary>
/// This class is responsible for handling the registration of users.
/// </summary>
public class RegistrationService : IRegistrationService
{
    // Data handler for user registration
    private readonly IRegisterHospital _dataHandler;
    
    // Prefix for user input requests
    private const string RequestPrefix = "Please enter in your";
    
    // List of available specialties for surgeons
    private readonly List<string> _specialties = new List<string>()
    {
        "General Surgeon",
        "Orthopaedic Surgeon",
        "Cardiothoracic Surgeon",
        "Neurosurgeon"
    };
    
    // Constructor for RegistrationService
    public RegistrationService(IRegisterHospital dataHandler)
    {
        _dataHandler = dataHandler;
    }
    
    /// <summary>
    /// This method is used to register a user, it will prompt the user for the required information.
    /// The user object will then be created, and registered with the data handler.
    /// </summary>
    /// <param name="role">This is the role title passed by calling method, it identifies
    /// the user role type and the menu title to be displayed</param>
    public void RegisterUser(string role)
    {
        // Check if the role is "floor manager" and all floors are already managed
        if (role == "floor manager" && _dataHandler.AreAllFloorsManaged())
        {
            CmdLineUI.DisplayError("All floors are assigned");
            return;
        }

        CmdLineUI.DisplayMessage($"Registering as a {role}.");

        // Collect common user information
        string name = PromptForInput("name", ValidateName);
        int age = PromptForAge(role);
        string mobile = PromptForInput("mobile number", ValidateMobile);
        string email = PromptForUniqueEmail();
        string password = PromptForInput("password", ValidatePassword);

        // Initialize variables for role-specific information
        int? staffId = null;
        Floor? floor = null;
        string? specialty = null;

        // Collect role-specific information
        switch (role)
        {
            case "patient":
                // No additional information needed for patients
                break;
            case "floor manager":
                staffId = PromptForUniqueStaffId();
                floor = PromptForValidFloor();
                break;
            case "surgeon":
                staffId = PromptForUniqueStaffId();
                specialty = PromptForSpecialty();
                break;
            default:
                CmdLineUI.DisplayError("Invalid");
                return;
        }

        // Create the appropriate user based on role
        User newUser = CreateUser(role, name, age, mobile, email, password, staffId, floor, specialty);
        // Register the new user to DB
        _dataHandler.RegisterUser(newUser);
        
    }
    
    /// <summary>
    ///  This method is used to prompt the user for input, and validate it using the provided validation function.
    /// </summary>
    /// <param name="field">the user information/field that is required for registering</param>
    /// <param name="validate">The delegated method that validates the respective field</param>
    /// <returns>returns valid input</returns>
    private string PromptForInput(string field, Func<string, bool> validate)
    {
        while (true)
        {
            CmdLineUI.DisplayMessage($"{RequestPrefix} {field}:");
            string input = CmdLineUI.GetString();

            if (validate(input))
                return input;

            CmdLineUI.DisplayErrorAgain($"Supplied {field} is invalid");
        }
    }

    
    /// <summary>
    /// This method is used to prompt the user for age, and validate it based on the role's requirements.
    /// </summary>
    /// <param name="role">string that identifies the role type</param>
    /// <returns>returns valid age</returns>
    /// <exception cref="ArgumentException">Handles invalid role (Always valid as its not
    /// passed from user input, nice for future changes) </exception>
    private int PromptForAge(string role)
    {
        (int minAge, int maxAge) = role switch
        {
            "patient" => (0, 100),
            "floor manager" => (21, 70),
            "surgeon" => (30, 75),
            _ => throw new ArgumentException("Invalid") // always valid, beneficial for future changes to roles
        };

        while (true)
        {
            CmdLineUI.DisplayMessage($"{RequestPrefix} age:");
            string input = CmdLineUI.GetString();

            if (!int.TryParse(input, out int age))
            {
                CmdLineUI.DisplayErrorAgain("Supplied value is not an integer");
                continue;
            }

            if (age < minAge || age > maxAge)
            {
                CmdLineUI.DisplayErrorAgain($"Supplied age is invalid");
                continue;
            }

            return age;
        }
    }

    
    /// <summary>
    /// Prompts the user for a unique email address.
    /// If the email is already registered, the user will be prompted to enter a new email.
    /// </summary>
    /// <returns>valid email</returns>
    private string PromptForUniqueEmail()
    {
        while (true)
        {
            string email = PromptForInput("email", IsValidEmail);
            if (_dataHandler.IsEmailUnique(email))
                return email;

            CmdLineUI.DisplayErrorAgain("Email is already registered");
        }
    }

    
    /// <summary>
    /// Prompts the user for a unique staff ID.
    /// If the staff ID is already registered, or not in the valid range, the user will be reprompted.
    /// </summary>
    /// <returns>Valid staff ID</returns>
    private int PromptForUniqueStaffId()
    {
        while (true)
        {
            CmdLineUI.DisplayMessage($"{RequestPrefix} staff ID:");
            string input = CmdLineUI.GetString();

            if (int.TryParse(input, out int staffId) && staffId >= 100 && staffId <= 999) // staff ID must be 3 digits
            {
                if (_dataHandler.IsStaffIdUnique(staffId)) // check if staff ID is unique
                    return staffId;

                CmdLineUI.DisplayErrorAgain("Staff ID is already registered");
            }
            else
            {
                CmdLineUI.DisplayErrorAgain("Supplied staff identification number is invalid");
            }
        }
    }
    
    /// <summary>
    /// Prompts the user for a valid floor number and retrieves the corresponding Floor object.
    /// If the floor is already assigned to another floor manager, the user will be reprompted.
    /// </summary>
    /// <returns>Valid Floor object that the floor manager can be assigned to</returns>
    private Floor PromptForValidFloor()
    {
        while (true)
        {
            CmdLineUI.DisplayMessage($"{RequestPrefix} floor number:");
            int floorNumber = CmdLineUI.GetInt();

            if (floorNumber >= 1 && floorNumber <= 6) // valid floor numbers
            {
                Floor selectedFloor = _dataHandler.GetFloor(floorNumber); // get the floor object

                if (_dataHandler.IsFloorAvailable(selectedFloor.FloorNumber)) // check if floor is available
                {
                    return selectedFloor;
                }
                // if floor is not available, display error message
                CmdLineUI.DisplayErrorAgain("Floor has been assigned to another floor manager"); 
            }
            else // if floor number is invalid, display error message. Loop back to prompt
            {
                CmdLineUI.DisplayErrorAgain("Supplied floor is invalid");
            }
        }
    }
    
    /// <summary>
    /// Prompts the user to select a specialty from the list of available specialties.
    /// If the user selects an invalid option, they will be reprompted.
    /// </summary>
    /// <returns>valid specialty string</returns>
    private string PromptForSpecialty()
    {
        while (true)
        {
            CmdLineUI.DisplayMessage("Please choose your speciality:");
            for (int i = 0; i < _specialties.Count; i++)
            {
                CmdLineUI.DisplayMessage($"{i + 1}. {_specialties[i]}"); // display the specialties
            }

            CmdLineUI.DisplayMessage($"Please enter a choice between 1 and {_specialties.Count}.");
            int choice = CmdLineUI.GetInt();

            if (choice >= 1 && choice <= _specialties.Count)
            {
                return _specialties[choice - 1]; // return the selected specialty
            }

            CmdLineUI.DisplayErrorAgain("Non-valid speciality type"); // display error message, loop back to prompt.
        }
    }

    
    /// <summary>
    /// Validates the name input.
    /// </summary>
    /// <param name="name">user input for name</param>
    /// <returns>bool value if name is valid or not</returns>
    private bool ValidateName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
    }

    /// <summary>
    /// Validates the mobile number input.
    /// Checks if the mobile number is 10 digits long, starts with 0, and contains only digits.
    /// </summary>
    /// <param name="mobile">User input for mobile</param>
    /// <returns>bool value if mobile is valid or not</returns>
    private bool ValidateMobile(string mobile)
    {
        return mobile.Length == 10 && mobile.All(char.IsDigit) && mobile.StartsWith("0");
    }

    /// <summary>
    /// Validates the email input.
    /// Checks if the email contains an '@' symbol, has a local part and domain part, and contains only valid characters.
    /// </summary>
    /// <param name="email">user input for email</param>
    /// <returns>bool value if email is valid or not</returns>
    private bool IsValidEmail(string email)
    {
        int atPosition = email.IndexOf('@'); // find the position of the '@' symbol
        if (atPosition <= 0 || atPosition >= email.Length - 1) // check if '@' is in a valid position
            return false;

        string localPart = email.Substring(0, atPosition); // split the email into local and domain parts
        string domainPart = email.Substring(atPosition + 1); // split the email into local and domain parts
        // check if the local part contains only letters, and the domain part contains a '.' and valid characters
        return localPart.All(char.IsLetter) &&
               domainPart.Contains(".") &&
               domainPart.All(c => char.IsLetterOrDigit(c) || c == '.');
    }

    /// <summary>
    /// Validates the password input.
    /// Checks if the password is:
    /// at least 8 characters long,
    /// contains at least one digit,
    /// one uppercase letter,
    /// and one lowercase letter.
    /// </summary>
    /// <param name="password">Users inputted password</param>
    /// <returns>bool value if password is valid or not</returns>
    private bool ValidatePassword(string password)
    {
        return password.Length >= 8 && // check if password is at least 8 characters long
               password.Any(char.IsDigit) && // check if password contains a digit
               password.Any(char.IsLetter) && // check if password contains a letter
               password.Any(char.IsUpper) && // check if password contains an uppercase letter
               password.Any(char.IsLower); // check if password contains a lowercase letter
    }

    /// <summary>
    /// Creates a new user object based on the role, and the provided information.
    /// </summary>
    /// <param name="role">The role of the user to be created</param>
    /// <param name="name">the name of the user</param>
    /// <param name="age">the age of the user</param>
    /// <param name="mobile">the mobile of the user</param>
    /// <param name="email">the email of the user</param>
    /// <param name="password">the password of the user</param>
    /// <param name="staffId">The staff id for users of staff roles</param>
    /// <param name="floor">the floor object that the floor manager is responsible for</param>
    /// <param name="specialty">The specialty of the surgeon</param>
    /// <returns>Object representing the created user</returns>
    /// <exception cref="ArgumentException">is invalid or does not match expected roles</exception>
    private User CreateUser(string role, string name, int age, string mobile, string email, string password, int? staffId, Floor? floor, string? specialty)
    {
        return role switch
        {
            "patient" => new Patient(name, age, mobile, email, password), // create a new patient object
            "floor manager" => new FloorManager(name, age, mobile, email, password, staffId.Value, floor!), // create a new floor manager object
            "surgeon" => new Surgeon(name, age, mobile, email, password, staffId.Value, specialty!), // create a new surgeon object
            _ => throw new ArgumentException("Invalid")
        };
    }
    
}