namespace CAB201Take3;
/// <summary>
/// Class to define the menu item object.
/// It contains a title and an action.
/// </summary>
public class MenuItem 
{
    /// <summary>
    ///  Function to get the title of the menu item.
    /// </summary>
    private Func<string> TitleFunc { get; }
    /// <summary>
    ///  Action to be performed when the menu item is selected.
    /// </summary>
    public Action Action { get; }
    /// <summary>
    ///  Constructor for the menu item object.
    /// This one is used by the patient menu to handle dynamic titles.
    /// </summary>
    /// <param name="titleFunc">Title function</param>
    /// <param name="action">action </param>
    public MenuItem(Func<string> titleFunc, Action action)
    {
        TitleFunc = titleFunc; 
        Action = action;
    }
    /// <summary>
    /// Constructor for the menu item object.
    /// This one is used elsewhere to handle static titles.
    /// </summary>
    /// <param name="title">Title function</param>
    /// <param name="action">Action</param>
    public MenuItem(string title, Action action)
    {
        TitleFunc = () => title;  
        Action = action;
    }
    /// <summary>
    ///  Property to get the title of the menu item.
    /// </summary>
    public string Title => TitleFunc();
}
