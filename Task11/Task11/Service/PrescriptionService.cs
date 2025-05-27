using Task11.Data;
using Task11.Dto;
using Task11.Model;
using Task11.Repositories.abstr;

namespace Task11.Service;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMedicamentRepository _medicamentRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IPrescriptionMedicamentRepository _prescriptionMedicamentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PrescriptionService(IPatientRepository patientRepository, IDoctorRepository doctorRepository, IMedicamentRepository medicamentRepository, IPrescriptionRepository prescriptionRepository, IPrescriptionMedicamentRepository prescriptionMedicamentRepository, IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _medicamentRepository = medicamentRepository;
        _prescriptionRepository = prescriptionRepository;
        _prescriptionMedicamentRepository = prescriptionMedicamentRepository;
        _unitOfWork = unitOfWork;
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
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        var patient = await _patientRepository.GetPatientByIdAsync(prescription.patient.IdPatient, cancellationToken);
        
        if (patient == null)
        {
            patient = new Patient
            {
                IdPatient = prescription.patient.IdPatient,
                FirstName = prescription.patient.FirstName,
                LastName = prescription.patient.LastName,
                BirthDate = prescription.patient.BirthDate
            };
            await _patientRepository.AddPatientAsync(patient, cancellationToken);
        }
        var doctor = _doctorRepository.FindDoctorByIdAsync(prescription.IdDoctor, cancellationToken).Result;
        if (doctor == null)
        {
            return false;
        }
        var medicaments = await _medicamentRepository.FindAllByIdAsync(prescription.medicaments.Select(m => m.IdMedicament).ToList(), cancellationToken);
        if (medicaments.Count != prescription.medicaments.Count)
        {
            throw new Exception("Some medicaments not found");
        }
        
        Prescription result = await _prescriptionRepository.CreatePrescriptionAsync(new Prescription
        {
            Patient = patient,
            IdDoctor = prescription.IdDoctor,
            Date = prescription.Date,
            DueDate = prescription.DueDate
        }, cancellationToken);
        
        foreach (var medicament in prescription.medicaments)
        {
            var prescriptionMedicament = new PrescriptionMedicament
            {
                IdPrescription = result.IdPrescription,
                IdMedicament = medicament.IdMedicament,
                Dose = medicament.Dose,
                Details = medicament.Details
            };
            await _prescriptionMedicamentRepository.AddAsync(prescriptionMedicament, cancellationToken);
        }
        await _unitOfWork.CommitTransactionAsync(cancellationToken);
        
        return true;
    }
    
    
    
    
}