using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientsAPI.Database.Models;
using PatientsAPI.DTOs;
using PatientsAPI.Services;

namespace PatientsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DoctorService _doctorService;

        public DoctorsController(
            IMapper mapper,
            DoctorService doctorService)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorResponse>>> GetDoctors(string sortBy, bool orderAsc = true, int page = 1)
        {
            try
            {
                var doctorDTOs = _mapper.Map<List<DoctorResponse>>(await _doctorService.GetDoctorsAsync(sortBy, orderAsc, page));

                return Ok(doctorDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorResponse>> GetDoctor(Guid id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorAsync(id);
                var doctorDTO = _mapper.Map<DoctorResponse>(doctor);

                return Ok(doctorDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DoctorResponse>> PostDoctor(DoctorRequest doctorDTO)
        {
            try
            {
                var doctor = _mapper.Map<Doctor>(doctorDTO);
                await _doctorService.AddAsync(doctor);

                return Created("GetDoctor", new { id = doctor.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(Guid id, DoctorRequest doctorDTO)
        {
            try
            {
                var doctor = _mapper.Map<Doctor>(doctorDTO);
                doctor.Id = id;

                await _doctorService.UpdateAsync(doctor);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            try
            {
                await _doctorService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
