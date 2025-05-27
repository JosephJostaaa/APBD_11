using Microsoft.EntityFrameworkCore;
using Task11.Data;
using Task11.Model;
using Task11.Repositories.abstr;

namespace Task11.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly HospitalDbContext _hospitalDbContext;
    
    public PatientRepository(HospitalDbContext hospitalDbContext)
    {
        _hospitalDbContext = hospitalDbContext;
    }
    
    public async Task<Patient?> GetPatientByIdAsync(int patientId, CancellationToken cancellationToken)
    {
        return await _hospitalDbContext.Patients
            .FirstOrDefaultAsync(p => p.IdPatient == patientId, cancellationToken);
    }
    
    public async Task AddPatientAsync(Patient patient, CancellationToken cancellationToken)
    {
        await _hospitalDbContext.Patients.AddAsync(patient, cancellationToken);
        await _hospitalDbContext.SaveChangesAsync(cancellationToken);
    }
    
    
    
    
}