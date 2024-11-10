namespace CAB201Take3;
// interfaces for user management


/// <summary>
///  Interface to define the registration services capabilities for the hospital.
/// </summary>
public interface IRegistrationService
{
    void RegisterUser(string role);
}
/// <summary>
/// Interface to define the session managers capabilities for the hospital.
/// </summary>
public interface ISessionManager
{
    void Login();
    void ResetLoggedUser();
}
