using Task11.Model;

namespace Task11.Repositories.abstr;

public interface IMedicamentRepository
{
    public Task<List<Medicament>> FindAllByIdAsync(List<int> ids, CancellationToken cancellationToken = default);
}