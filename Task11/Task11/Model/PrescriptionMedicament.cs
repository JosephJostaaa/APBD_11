using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task11.Model;

[Table("Prescription_Medicament")]
[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
public class PrescriptionMedicament
{
    [ForeignKey(nameof(Medicament))]
    public int IdMedicament { get; set; }
    [ForeignKey(nameof(Prescription))]
    public int IdPrescription { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
    
    public Prescription Prescription { get; set; }
    public Medicament Medicament { get; set; }
}