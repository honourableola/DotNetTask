﻿using DotNetTask.API.Services.Interfaces;
using DotNetTask.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DotNetTask.API.Controllers
{
    [Route("api/applications")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [HttpPost]
        public async Task<IActionResult> SubmitApplication(string programId, [FromBody] ApplicationRequest item)
        {
            var task = await _applicationService.AddApplicationAsync(programId, item);
            return Ok(task);
        }
    }
}
