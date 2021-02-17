using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerRentCar.Auth;
using ServerRentCar.DTO;
using ServerRentCar.Models;
using ServerRentCar.Services;
using ServerRentCar.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ServerRentCar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Users : ControllerBase
    {

        private readonly ILogger<Users> _logger;
        private rentdbContext _rentdbContext;
        private DataAautoMapper _dataAautoMapper;
        private UserService _userService;
        public Users(ILogger<Users> logger, rentdbContext rentdbContext, DataAautoMapper dataAautoMapper,
            UserService userService)
        {
            _logger = logger;
            _rentdbContext = rentdbContext;
            _dataAautoMapper = dataAautoMapper;
            _userService = userService;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get()
        {
            return Ok(_dataAautoMapper.GetDTOList<User, UserDTO>(_rentdbContext.Users));
        }


        [HttpPost]
        [Route("Login")]
        [Produces("application/json")]
        public IActionResult Login(LogInModel login)
        {
            var user = _rentdbContext.Users.Find(login.UserName);
            return Ok(user != null && user.Password == login.Password);

        }
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register(RegisterModel registerModel)
        {
            if (_userService.UserExist(registerModel))
                return Ok(StatusCodes.Status409Conflict);
            if (_userService.Register(registerModel))
                return Ok(StatusCodes.Status201Created);
            else
                return Ok(StatusCodes.Status500InternalServerError);
        }
    }
}
