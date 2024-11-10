namespace CAB201Take3;
/// <summary>
///  Class to layout the surgeon menu
/// </summary>
public class SurgeonMenu : UserMenu
{
    /// <summary>
    ///  Injected dependency for surgeon operations.
    /// </summary>
    private readonly ISurgeonOperations _hospitalOperations;
    /// <summary>
    /// reference to the surgeon object.
    /// </summary>
    private readonly Surgeon _surgeon;

    /// <summary>
    /// Constructor for the SurgeonMenu class.
    /// </summary>
    /// <param name="user">Surgeon reference</param>
    /// <param name="surgeonOperations">Surgeon operations</param>
    public SurgeonMenu(User user, ISurgeonOperations surgeonOperations) : base(user, surgeonOperations)
    {
        _hospitalOperations = surgeonOperations;
        _surgeon = user as Surgeon;
    }
    /// <summary>
    /// Method to add role specific menu items for the surgeon.
    /// </summary>
    protected override void AddRoleSpecificMenuItems()
    {
        MenuItems.Add(new MenuItem("See your list of patients", () => _hospitalOperations.DisplayAssignedPatients(_surgeon)));
        MenuItems.Add(new MenuItem("See your schedule", () => _hospitalOperations.SurgeonSchedule(_surgeon)));
        MenuItems.Add(new MenuItem("Perform surgery", () => _hospitalOperations.PerformSurgery(_surgeon)));
    }
}