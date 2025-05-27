using Task11.Model;

namespace Task11.Repositories.abstr;

public interface IPrescriptionRepository
{
    public Task<Prescription> CreatePrescriptionAsync(Prescription prescription, CancellationToken cancellationToken);
}