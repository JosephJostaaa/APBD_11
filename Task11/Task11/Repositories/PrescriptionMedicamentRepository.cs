using Task11.Data;
using Task11.Model;
using Task11.Repositories.abstr;

namespace Task11.Repositories;

public class PrescriptionMedicamentRepository : IPrescriptionMedicamentRepository
{
    private readonly HospitalDbContext _context;

    public PrescriptionMedicamentRepository(HospitalDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(PrescriptionMedicament prescriptionMedicament, CancellationToken cancellationToken)
    {
        await _context.PrescriptionMedicaments.AddAsync(prescriptionMedicament, cancellationToken);
    }
}