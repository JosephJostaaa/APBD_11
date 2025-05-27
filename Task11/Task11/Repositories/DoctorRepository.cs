using Microsoft.EntityFrameworkCore;
using Task11.Data;
using Task11.Model;
using Task11.Repositories.abstr;

namespace Task11.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly HospitalDbContext _context;
    
    
    public DoctorRepository(HospitalDbContext context)
    {
        _context = context;
    }
    
    public async Task<Doctor?> FindDoctorByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .FirstOrDefaultAsync(d => d.IdDoctor == id, cancellationToken);
    }
}