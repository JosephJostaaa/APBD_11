using Task11.Model;

namespace Task11.Dto;

public class PrescriptionResponse
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<MedicamentResponse> Medicaments { get; set; }
    public DoctorResponse Doctor { get; set; }
}