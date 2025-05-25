namespace Task11.Dto;

public class PatientResponse
{
    public int PatientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<PrescriptionResponse> Prescriptions { get; set; }
    
}