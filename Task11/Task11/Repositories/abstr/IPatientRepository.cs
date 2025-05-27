using Task11.Model;

namespace Task11.Repositories.abstr;

public interface IPatientRepository
{
    public Task<Patient?> GetPatientByIdAsync(int patientId, CancellationToken cancellationToken);
    public Task AddPatientAsync(Patient patient, CancellationToken cancellationToken);
}