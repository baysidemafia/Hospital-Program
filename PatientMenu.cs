namespace CAB201Take3;
/// <summary>
/// Class to manage the menu for a patient.
/// </summary>
public class PatientMenu : UserMenu
{
    /// <summary>
    /// Injected dependency for patient operations.
    /// </summary>
    private readonly IPatientOperations _patientOperations;
    /// <summary>
    ///  Reference to the patient object.
    /// </summary>
    private readonly Patient _patient;

    /// <summary>
    ///  Constructor for the PatientMenu class.
    /// </summary>
    /// <param name="user">Passed User (patient)</param>
    /// <param name="patientOperations">patient operations</param>
    public PatientMenu(User user, IPatientOperations patientOperations)
        : base(user, patientOperations)
    {
        _patientOperations = patientOperations;
        _patient = user as Patient;
    }
    /// <summary>
    /// Method to add role specific menu items for the patient.
    /// </summary>
    protected override void AddRoleSpecificMenuItems()
    {
        MenuItems.Add(new MenuItem(
            () => _patientOperations.IsCheckedIn(_patient) ? "Check out" : "Check in",  // Dynamic title
            () => _patientOperations.CheckedOption(_patient)  // Single action for both check-in and check-out
        ));


        MenuItems.Add(new MenuItem("See room", () => _patientOperations.SeeRoom(_patient)));
        MenuItems.Add(new MenuItem("See surgeon", () => _patientOperations.SeeSurgeon(_patient)));
        MenuItems.Add(new MenuItem("See surgery date and time", () => _patientOperations.SeeSurgeryDetails(_patient)));
    }
}