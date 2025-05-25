using Task11.Dto;

namespace Task11.Service;

public interface IPrescriptionService
{
    public Task<bool> CreatePrescription(PrescriptionDto prescription, CancellationToken cancellationToken);
}