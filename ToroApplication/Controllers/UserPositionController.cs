using Domain.Model.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToroApplication.DTOs.Response;

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

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserPositionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserPosition()
        {
            try
            {
                var serviceResult = await _userPositionService.GetUserPosition(GetUserCpf()).ConfigureAwait(false);
                if (serviceResult.Result == null)
                    return NotFound();

                var userPositionDto = new UserPositionDto()
                {
                    CheckingAccountAmount = serviceResult.Result.CheckingAccountAmount,
                    Consolidated = serviceResult.Result.Consolidated,
                    Positions = serviceResult.Result.Positions?.Select(c => new PositionDto()
                    {
                        Amount = c.Amout,
                        CurrentPrice = c.Share.CurrentPrice,
                        Symbol = c.Share.Symbol
                    }).ToList()
                };

                return Ok(userPositionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        } 
    }
}
