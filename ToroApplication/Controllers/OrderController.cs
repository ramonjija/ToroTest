using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToroApplication.DTOs.Request;
using ToroApplication.DTOs.Response;
using ToroApplication.DTOs.Validation;
using ToroApplication.Mappers;

namespace ToroApplication.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private IOrderService _orderService;

        public OrderController(ILogger<UserController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        /// <summary>
        /// This Route is responsible to buy Shares for the authenticated user
        /// </summary>
        /// <param name="buyShareDto">
        /// ShareSymbol = The symbol used to identify the share;
        /// Amount = The quantity of the share that will be bought;
        /// </param>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserPositionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuyShare([FromBody] BuyShareDto buyShareDto)
        {
            try
            {
                var validationResult = new BuyShareValidation().Validate(buyShareDto);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors.Select(c => c.ErrorMessage));

                var serviceResult = await _orderService.BuyShare(buyShareDto.ShareSymbol, buyShareDto.Amount, GetUserCpf());
                if (!serviceResult.Success)
                    return BadRequest(serviceResult.Validator.ValidationMessages);

                var userPositionDto = new UserPositionAdapter().Adapt(serviceResult.Result);

                return Created($"UserPosition: {userPositionDto}", userPositionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// This Route is responsible to add Balance (money) to the authenticated user
        /// </summary>
        /// <param name="addedBalance">
        /// Balance = The amount of money that the user wants to add to the balance
        /// </param>
        [Authorize]
        [HttpPost("balance")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserPositionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBalance([FromBody] AddBalanceDto addedBalance)
        {
            try
            {
                var serviceResult = await _orderService.AddBalance(addedBalance.Balance, GetUserCpf());
                if (!serviceResult.Success)
                    return BadRequest(serviceResult.Validator.ValidationMessages);

                var userPositionDto = new UserPositionAdapter().Adapt(serviceResult.Result);

                return Created($"UserPosition: {userPositionDto}", userPositionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
