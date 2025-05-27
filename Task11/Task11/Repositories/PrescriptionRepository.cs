using Task11.Data;
using Task11.Model;
using Task11.Repositories.abstr;

namespace Task11.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly HospitalDbContext _context;

    public PrescriptionRepository(HospitalDbContext context)
    {
        _context = context;
    }
    
    public async Task<Prescription> CreatePrescriptionAsync(Prescription prescription, CancellationToken cancellationToken)
    {
        var res = await _context.Prescriptions.AddAsync(prescription, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return res.Entity;
    }
}