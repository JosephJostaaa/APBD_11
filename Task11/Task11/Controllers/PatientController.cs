using Microsoft.AspNetCore.Mvc;
using Task11.Service;

namespace Task11.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    
    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(int id, CancellationToken cancellationToken)
    {
        var patient = await _patientService.GetPatientByIdAsync(id, cancellationToken);
        
        return Ok(patient);
    }
}