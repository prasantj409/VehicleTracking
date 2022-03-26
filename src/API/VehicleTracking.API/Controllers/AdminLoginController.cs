using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Domain.Entities;
using Test.Services.UserServices;
using TestAPI.Security;
using VehicleTracking.Domain.DTO;
using VehicleTracking.Domain.Response;
using VehicleTracking.Service.Exceptions;

namespace TestAPI.Controllers
{
    [Route("api",Name ="Admin Login")]
    [ApiController]
    public class AdminLoginController : ControllerBase
    {
        private readonly IUserService _userService;
        public AdminLoginController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Login end point for Admin user. Default userid=>system and password=>system. There is no end point to register users
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("admin/login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GenericResponse<UserAuthResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Authenticate([FromBody] UserLoginDto resource)
        {
            var user = await _userService.Get(resource);

            if (user == null)
                throw new NotFoundException("User or password invalid");

            var token = TokenService.CreateToken(user);

            var authDto = new UserAuthResponseDto
            {
                UserName = user.Username,
                AccessToken = token
            };

            GenericResponse<UserAuthResponseDto> response = new GenericResponse<UserAuthResponseDto>(authDto);
            return Ok(response);
        }
    }
}
