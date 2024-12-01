using Microsoft.AspNetCore.Mvc;
using ApiBurguerMania.Service.Interface;
using ApiBurguerMania.Dto.Status;
using System.Threading.Tasks;


namespace ApiBurguerMania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        // GET: api/Status
        [HttpGet]
        public async Task<IActionResult> GetAllStatus()
        {
            var statuses = await _statusService.GetAllStatusAsync();
            return Ok(statuses);
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusById(int id)
        {
            try
            {
                var status = await _statusService.GetStatusByIdAsync(id);
                return Ok(status);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Status não encontrado");
            }
        }

        // POST: api/Status
        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] StatusDTO statusDto)
        {
            var createdStatus = await _statusService.CreateStatusAsync(statusDto);
            return CreatedAtAction(nameof(GetStatusById), new { id = createdStatus.Id }, createdStatus);
        }

        // PUT: api/Status/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusDTO statusDto)
        {
            var success = await _statusService.UpdateStatusAsync(id, statusDto);
            if (!success)
            {
                return NotFound("Status não encontrado");
            }

            return NoContent();
        }

        // DELETE: api/Status/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var success = await _statusService.DeleteStatusAsync(id);
            if (!success)
            {
                return NotFound("Status não encontrado");
            }

            return NoContent();
        }
    }
}
