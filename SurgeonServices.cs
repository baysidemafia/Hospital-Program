namespace CAB201Take3;
/// <summary>
/// Class to handle the services that a surgeon can perform.
/// It implements the ISurgeonServices interface which
/// defines the capabilities of a surgeon
/// This setup allows for the capability of a surgeon to be extended in the future
/// And allows for a new user to do multiple roles
/// </summary>
public class SurgeonServices : StaffServices, ISurgeonServices
{
    /// <summary>
    ///  The data handler for the surgeon services
    /// </summary>
    private readonly ISurgeonHospital _dataHandler;
    /// <summary>
    ///  Constructor for the SurgeonServices class
    /// </summary>
    /// <param name="surgeonHospital">Initilises surgeon data handler</param>
    public SurgeonServices(ISurgeonHospital surgeonHospital) 
    {
        _dataHandler = surgeonHospital;
    }
    /// <summary>
    /// Method to Display details of the Surgeon
    /// </summary>
    /// <param name="loggedUser">Surgeon</param>
    public override void DisplayDetails(User loggedUser)
    {
        base.DisplayDetails(loggedUser);
        
        if (loggedUser is Surgeon surgeon)
        {
            CmdLineUI.DisplayMessage($"Speciality: {surgeon.Speciality}");
        }
        
    }
    /// <summary>
    /// Method to display the patients assigned to a surgeon
    /// </summary>
    /// <param name="surgeon">Surgeon to check their patients</param>
    public void DisplayAssignedPatients(Surgeon surgeon)
    {
        CmdLineUI.DisplayMessage("Your Patients.");
        
        List<Patient> assignedPatients = _dataHandler.GetPatientsForSurgeon(surgeon);  
        if (assignedPatients.Count == 0) // if there are no patients assigned, display message
        {
            
            CmdLineUI.DisplayMessage("You do not have any patients assigned.");
        }
        else // otherwise, display the patients
        {
            for (int i = 0; i < assignedPatients.Count; i++)
            {
                var patient = assignedPatients[i];  // Get the patient
                CmdLineUI.DisplayMessage($"{i + 1}. {patient.Name}");  // Display patient name
            }
        }
        
    }
    /// <summary>
    /// Method to display the schedule of a surgeon
    /// </summary>
    /// <param name="surgeon">Surgeon whos checking schedule</param>
    public void SurgeonSchedule(Surgeon surgeon)
    {
        List<Patient> assignedPatients = _dataHandler.GetPatientsForSurgeon(surgeon);
        
        CmdLineUI.DisplayMessage("Your schedule.");
        
        if (assignedPatients.Count == 0) // if the surgeon doesnt have any patients
        {
            CmdLineUI.DisplayMessage("You do not have any patients assigned.");
        }
        else // otherwise, display the schedule
        {
            assignedPatients = assignedPatients
                .Where(patient => _dataHandler.GetSurgeryDateForPatient(patient).HasValue)  // Filter out patients without a surgery date
                .OrderBy(patient => _dataHandler.GetSurgeryDateForPatient(patient).Value)   // Sort by surgery date (ascending)
                .ToList();

            // Display the schedule for each patient
            foreach (var patient in assignedPatients)
            {
                DateTime? surgeryDateTime = _dataHandler.GetSurgeryDateForPatient(patient);
                
                CmdLineUI.DisplayMessage($"Performing surgery on patient {patient.Name} on {surgeryDateTime.Value:HH:mm dd/MM/yyyy}");
            }
        }
    }
    /// <summary>
    /// Method to perform surgery on a patient, marking it as completed and displaying a message
    /// </summary>
    /// <param name="surgeon">Surgeon conducting surgery</param>
    public void PerformSurgery(Surgeon surgeon)
    {
        // get the patients assigned to the surgeon
        List<Patient> assignedPatients = _dataHandler.GetPatientsForSurgeon(surgeon); 
        // Filter out patients with completed surgeries
        List<Patient> patientsPendingSurgery = assignedPatients.Where(p => !_dataHandler.IsSurgeryCompleted(p)).ToList();
        
        if (patientsPendingSurgery.Count == 0) // if there are no patients with pending surgeries
        {
            CmdLineUI.DisplayMessage("You have no upcoming surgeries");
            return;
        }
        // Select a patient from the list of patients with pending surgeries
        Patient selectedPatient = SelectEligiblePerson(patientsPendingSurgery, "patient");
        // Perform the surgery, marking it as completed, and display a message
        _dataHandler.ConductSurgery(selectedPatient);
        CmdLineUI.DisplayMessage($"Surgery performed on {selectedPatient.Name} by {surgeon.Name}.");

    }
    
}