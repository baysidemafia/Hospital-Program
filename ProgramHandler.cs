namespace CAB201Take3;
/// <summary>
/// This class is responsible for setting up the program and running it.
/// It creates the necessary objects and starts the program.
/// </summary>
public class ProgramHandler
{
   /// <summary>
   /// This method is responsible for setting up the program and running it.
   /// </summary>
   public void Run()
   {
      // Create the hospital and the data handler
      Hospital hospital = new Hospital();
      DataHandler dataHandler = new DataHandler(hospital);
      // Create the registration service
      RegistrationService registrationService = new RegistrationService(dataHandler);
      // Create the hospital operations and the operations factory
      HospitalOperations hospitalOperations = new HospitalOperations(dataHandler);
      UserMenuFactory operationsFactory = new UserMenuFactory(hospitalOperations);
      // Create the session manager
      SessionManager sessionManager = new SessionManager(dataHandler, operationsFactory);
      // Create the menu manager
      MenuManager menuManager = new MenuManager(
         sessionManager,
         registrationService
         );
      // Calls Menu Manager Run method
      menuManager.Run();
   }
}

