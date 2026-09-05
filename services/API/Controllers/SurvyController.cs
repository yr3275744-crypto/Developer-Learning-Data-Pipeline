using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SurvyController : ControllerBase
    {
        private readonly SurvyService _survyService;
        public SurvyController(SurvyService survyService)
        {
            _survyService = survyService;
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<DeveloperAnswer>>> GetAllAsync()
        {
            var result = await _survyService.GetAll();
            return Ok(result);
        }
    }
}
