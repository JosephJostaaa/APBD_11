using Task11.Model;

namespace Task11.Repositories.abstr;

public interface IPrescriptionMedicamentRepository
{
    public Task AddAsync(PrescriptionMedicament prescriptionMedicament, CancellationToken cancellationToken);
}