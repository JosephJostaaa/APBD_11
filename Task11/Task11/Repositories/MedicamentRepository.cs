using Microsoft.EntityFrameworkCore;
using Task11.Data;
using Task11.Model;
using Task11.Repositories.abstr;

namespace Task11.Repositories;

public class MedicamentRepository : IMedicamentRepository
{
    private readonly HospitalDbContext _context;

    public MedicamentRepository(HospitalDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Medicament>> FindAllByIdAsync(List<int> ids, CancellationToken cancellationToken = default)
    {
        if (ids == null || !ids.Any())
        {
            return new List<Medicament>();
        }

        return await _context.Medicaments
            .Where(m => ids.Contains(m.IdMedicament))
            .ToListAsync(cancellationToken);
    }
}