using Task11.Data;
using Task11.Dto;
using Task11.Model;
using Task11.Repositories;
using Task11.Repositories.abstr;

namespace Task11.Service;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<PatientResponse> GetPatientByIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        Patient? patient = await _patientRepository.GetPatientByIdAsync(patientId, cancellationToken);
        if (patient == null)
        {
            throw new Exception("Patient not found");
        }
        
        var response = new PatientResponse
        {
            PatientId = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
            
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionResponse
            {
                IdPrescription = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,
                
                Doctor = new DoctorResponse
                {
                    IdDoctor = p.Doctor.IdDoctor,
                    FirstName = p.Doctor.FirstName,
                },
                
                Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentResponse
                {
                    IdMedicament = pm.Medicament.IdMedicament,
                    Name = pm.Medicament.Name,
                    Dose = pm.Dose,
                    Description = pm.Medicament.Description
                }).ToList()
            }).ToList()
        };
        return response;
    }
}