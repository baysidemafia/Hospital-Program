namespace CAB201Take3;
/// <summary>
/// Class for the FloorManagerMenu, which is a subclass of UserMenu.
/// Defines the menu items for the FloorManager.
/// Defines the capabilities of the FloorManager.
/// </summary>
public class FloorManagerMenu : UserMenu
{
    /// <summary>
    ///  Injects the respective operations for the FloorManager.
    /// </summary>
    private IFloorManagerOperations _hospitalOperations;
    /// <summary>
    /// Injects the respective FloorManager object.
    /// </summary>
    private readonly FloorManager _floorManager;
    /// <summary>
    ///  Constructor for the FloorManagerMenu.
    /// </summary>
    /// <param name="user">Floor manager</param>
    /// <param name="hospitalOperations">Floor manager operations</param>
    public FloorManagerMenu(User user, IFloorManagerOperations hospitalOperations) : base (user, hospitalOperations)
    {
        _hospitalOperations = hospitalOperations;
        _floorManager = user as FloorManager;
    }
    /// <summary>
    ///  Adds the menu items that are specific to the FloorManager.
    /// </summary>
    protected override void AddRoleSpecificMenuItems()
    {
        MenuItems.Add(new MenuItem("Assign room to patient", () => _hospitalOperations.AssignRoomToPatient(_floorManager)));
        MenuItems.Add(new MenuItem("Assign surgery", () => _hospitalOperations.AssignSurgeryToCheckedInPatient(_floorManager)));
        MenuItems.Add(new MenuItem("Unassign room", () => _hospitalOperations.UnassignRoomToCheckedPatient(_floorManager)));
    }
}