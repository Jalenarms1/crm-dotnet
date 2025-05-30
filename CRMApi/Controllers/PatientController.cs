using CRMApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<IActionResult> List()
        {
            var patients = await _patientService.GetPatientsAsync();
            Console.WriteLine(patients.Count);
            return Ok(patients);
        }

    }
}
