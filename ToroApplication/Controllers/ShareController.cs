using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Http;
using ToroApplication.DTOs.Response;

namespace ToroApplication.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShareController : BaseController
    {
        private readonly ILogger<ShareController> _logger;
        private IShareService _shareService;
        public ShareController(IShareService shareService, ILogger<ShareController> logger)
        {
            _shareService = shareService;
            _logger = logger;
        }

        /// <summary>
        /// This route is responsible to return all the shares of the system
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ShareDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetShares()
        {
            try
            {
                var serviceResult = await _shareService.GetAllShares().ConfigureAwait(false);
                return Ok(serviceResult.Result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
