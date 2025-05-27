using Task11.Model;

namespace Task11.Repositories.abstr;

public interface IDoctorRepository
{
    public Task<Doctor?> FindDoctorByIdAsync(int id, CancellationToken cancellationToken = default);
}