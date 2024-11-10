namespace CAB201Take3;
/// <summary>
/// This class is responsible for creating the correct menu for the user
/// It groups the different types of users into their respective menus
/// </summary>
public class UserMenuFactory
{
    private readonly HospitalOperations _hospitalOperations;

    /// <summary>
    /// The constructor for the UserMenuFactory
    /// It initialises the factory with the hospital operations to pass to the menu classes
    /// </summary>
    /// <param name="hospitalOperations">Hospital Operations class</param>
    public UserMenuFactory(HospitalOperations hospitalOperations)
    {
        _hospitalOperations = hospitalOperations;
    }
    /// <summary>
    /// This method creates the correct menu for the user based on their type
    /// Allowing dynamic dispatch to the correct menu once logged in
    /// </summary>
    /// <param name="user">User object</param>
    /// <returns>Correct User Menu</returns>
    /// <exception cref="ArgumentException">Invalid user</exception>
    public UserMenu CreateUserMenu(User user)
    {
        switch (user)
        {
            case Patient patient:
                return new PatientMenu(patient, _hospitalOperations); // create patient menu
            case Surgeon surgeon:
                return new SurgeonMenu(surgeon, _hospitalOperations); // create surgeon menu
            case FloorManager floorManager:
                return new FloorManagerMenu(floorManager, _hospitalOperations); // create floor manager menu
            default:
                throw new ArgumentException("Error"); // invalid user
        }
    }
}