using Microsoft.AspNetCore.Mvc;
using medsys.Models;
using medsys.Entities;
using medsys.Services;
using static medsys.Models.ConsultationDto;
using medsys.Auth;

namespace medsys.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/consult")]
    public class ConsultationController : ControllerBase
    {
        private IUserService _userService;
        private IConsultationService _consultationService;
        
        public ConsultationController(IUserService userService, IConsultationService consultationService)
        {
            _userService = userService;
            _consultationService = consultationService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            return Ok(await _consultationService.GetAllConsultations());
        }

        [HttpPost("add")]
        public async Task<IActionResult> addConsultation([FromBody] AddConsultationDto request)
        {
            Consultation newConsult = new Consultation();
            newConsult.CreationDate = DateTime.UtcNow;
            newConsult.ConsultationDate = request.ConsultationDate;
            newConsult.PatitentId = request.PatitentId;
            newConsult.DoctorId = request.DoctorId;
            newConsult.additionalInfo = request.AdditionalInfo ?? "";
            newConsult.ConsultationId = Guid.NewGuid().ToString();
            await _consultationService.AddConsultation(newConsult);
            return Created();
        }
    }
}
