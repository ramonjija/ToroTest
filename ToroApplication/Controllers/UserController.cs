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
using ToroApplication.DTOs.Validation;
using ToroApplication.Mappers;

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
                var validationResult = new PostUserValidation().Validate(postUserDto);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors.Select(c => c.ErrorMessage));

                var serviceResult = await _userservice.CreateUser(postUserDto.CPF, postUserDto.Name, postUserDto.Password);
                if (!serviceResult.Success)
                    return BadRequest(serviceResult.Validator.ValidationMessages);

                var createdUser = new UserAdapter().Adapt(serviceResult.Result);

                return Created($"User: {createdUser.UserId}", createdUser);
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
                var validationResult = new LogUserValidation().Validate(logUserDto);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors.Select(c => c.ErrorMessage));

                var serviceResult = await _userservice.GetUser(logUserDto.CPF, logUserDto.Password);
                if (!serviceResult.Success)
                    return BadRequest(serviceResult.Validator.ValidationMessages);

                var token = TokenService.GenerateToken(serviceResult.Result);

                var loggedUser = new UserAdapter().Adapt(serviceResult.Result, token);

                return Ok(loggedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
