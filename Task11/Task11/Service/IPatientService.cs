using Task11.Dto;

namespace Task11.Service;

public interface IPatientService
{
    public Task<PatientResponse> GetPatientByIdAsync(int patientId, CancellationToken cancellationToken);
}