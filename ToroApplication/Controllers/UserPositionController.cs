using Domain.Model.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToroApplication.DTOs.Response;
using ToroApplication.Mappers;

namespace ToroApplication.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserPositionController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private IUserPositionService _userPositionService;

        public UserPositionController(ILogger<UserController> logger, IUserPositionService userPositionService)
        {
            _logger = logger;
            _userPositionService = userPositionService;
        }

        /// <summary>
        /// This Route is responsible to get the User Position of the user, it consists on the Current Balance, Consolidated Balance and the Shares owned by the authenticated user
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserPositionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserPosition()
        {
            try
            {
                var serviceResult = await _userPositionService.GetUserPosition(GetUserCpf()).ConfigureAwait(false);
                if (serviceResult.Result == null)
                    return NotFound();

                var userPositionDto = new UserPositionAdapter().Adapt(serviceResult.Result);

                return Ok(userPositionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        } 
    }
}
