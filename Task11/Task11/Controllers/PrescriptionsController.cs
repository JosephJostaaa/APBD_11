using Microsoft.AspNetCore.Mvc;
using Task11.Dto;
using Task11.Service;

namespace Task11.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;
    
    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionDto prescriptionDto, CancellationToken cancellationToken)
    {
        if (await _prescriptionService.CreatePrescription(prescriptionDto, cancellationToken))
        {
            return Ok();
        }
        return BadRequest("Failed to create prescription");
    }
}