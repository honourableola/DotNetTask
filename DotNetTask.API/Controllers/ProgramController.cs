using DotNetTask.API.Services.Interfaces;
using DotNetTask.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DotNetTask.API.Controllers
{
    [Route("api/programs")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;
        public ProgramController(IProgramService programService)
        {
            _programService = programService ?? throw new ArgumentNullException(nameof(programService));
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [HttpPost]
        public async Task<IActionResult> CreateProgram([FromBody] CreateProgramRequest request)
        {
            var response = await _programService.AddProgramAsync(request);
            return Ok(response);
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProgram(string id, [FromBody] UpdateProgramRequest request)
        {
            var response = await _programService.EditProgramAsync(id, request);
            return Ok(response);
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgramQuestions(string id)
        {
            var response = await _programService.GetProgramQuestionsByIdAsync(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}
