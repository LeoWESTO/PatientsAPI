using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientsAPI.Database.Models;
using PatientsAPI.DTOs;
using PatientsAPI.Services;

namespace PatientsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly PatientService _patientService;

        public PatientsController(
            IMapper mapper,
            PatientService patientService)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientResponse>>> GetPatients(string sortBy, bool orderAsc = true, int page = 1)
        {
            try
            {
                var patientDTOs = _mapper.Map<List<PatientResponse>>(await _patientService.GetPatientsAsync(sortBy, orderAsc, page));

                return Ok(patientDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientResponse>> GetPatient(Guid id)
        {
            try
            {
                var patient = await _patientService.GetPatientAsync(id);
                var patientDTO = _mapper.Map<PatientResponse>(patient);

                return Ok(patientDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PatientResponse>> PostPatient(PatientRequest patientDTO)
        {
            try
            {
                var patient = _mapper.Map<Patient>(patientDTO);
                await _patientService.AddAsync(patient);

                return Created("GetPatient", new { id = patient.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(Guid id, PatientRequest patientDTO)
        {
            try
            {
                var patient = _mapper.Map<Patient>(patientDTO);
                patient.Id = id;

                await _patientService.UpdateAsync(patient);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            try
            {
                await _patientService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
