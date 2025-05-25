using Microsoft.VisualBasic;
using Task11.Data;
using Task11.Dto;
using Task11.Model;

namespace Task11.Service;

public class PrescriptionService : IPrescriptionService
{
    private readonly HospitalDbContext _context;
    public PrescriptionService(HospitalDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> CreatePrescription(PrescriptionDto prescription, CancellationToken cancellationToken)
    {
        if (prescription.DueDate < prescription.Date)
        {
            throw new Exception("Due date cannot be earlier than prescription date");
        }

        if (prescription.medicaments.Count > 10)
        {
            throw new Exception("Number of medicaments cannot be more than 10");
        }
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        var patient = _context.Patients.FirstOrDefault(p => p.IdPatient == prescription.patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                IdPatient = prescription.patient.IdPatient,
                FirstName = prescription.patient.FirstName,
                LastName = prescription.patient.LastName,
                BirthDate = prescription.patient.BirthDate
            };
            _context.Patients.Add(patient);
        }
        var doctor = _context.Doctors.FirstOrDefault(d => d.IdDoctor == prescription.IdDoctor);
        if (doctor == null)
        {
            return false;
        }
        var medicaments = _context.Medicaments
            .Where(m => prescription.medicaments.Any(pm => pm.IdMedicament == m.IdMedicament))
            .ToList();
        if (medicaments.Count != prescription.medicaments.Count)
        {
            throw new Exception("Some medicaments not found");
        }
        
        var result = _context.Prescriptions.Add(new Prescription
        {
            Patient = patient,
            IdDoctor = prescription.IdDoctor,
            Date = prescription.Date,
            DueDate = prescription.DueDate
        });
        
        foreach (var medicament in prescription.medicaments)
        {
            var prescriptionMedicament = new PrescriptionMedicament
            {
                IdPrescription = result.Entity.IdPrescription,
                IdMedicament = medicament.IdMedicament,
                Dose = medicament.Dose,
                Details = medicament.Details
            };
            _context.PrescriptionMedicaments.Add(prescriptionMedicament);
        }
        await _context.SaveChangesAsync(cancellationToken);
        
        await transaction.CommitAsync(cancellationToken);
        
        return true;
    }
    
    
    
    
}