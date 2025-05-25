namespace Task11.Dto;

public class PrescriptionDto
{
    public PatientDto patient { get; set; }
    public List<MedicamentDto> medicaments { get; set; }
    public int IdDoctor { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
    
}