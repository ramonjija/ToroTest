using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Security;
using ToroApplication.DTOs.Request;
using ToroApplication.DTOs.Response;

namespace ToroApplication.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private IUserService _userservice;

        public UserController(ILogger<UserController> logger, IUserService userservice)
        {
            _logger = logger;
            _userservice = userservice;
        }

        /// <summary>
        /// This Route is Used to Create Users
        /// </summary>
        /// <param name="postUserDto">
        /// Name = Name of the User
        /// CPF = CPF of the User
        /// Password = Password of the User
        /// </param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] PostUserDto postUserDto)
        {
            try
            {
                var serviceResult = await _userservice.CreateUser(postUserDto.Name, postUserDto.CPF, postUserDto.Password);
                if (!serviceResult.Success)
                    return BadRequest(serviceResult.ValidationMessages);

                var result = new UserDto()
                {
                    UserId = serviceResult.Result.UserId,
                    CPF = serviceResult.Result.CPF,
                    UserName = serviceResult.Result.UserName
                };

                return Created($"User: {result.UserId}", result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// This Route is Responsible for the Login of the User
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoggedUserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(LoggedUserDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LogUserDto logUserDto)
        {
            try
            {
                var serviceResult = await _userservice.GetUser(logUserDto.CPF, logUserDto.Password);
                if (!serviceResult.Success)
                    return BadRequest(serviceResult.ValidationMessages);

                var token = TokenService.GenerateToken(serviceResult.Result);

                return Ok(new LoggedUserDto(serviceResult.Result.UserName, serviceResult.Result.CPF, token));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
