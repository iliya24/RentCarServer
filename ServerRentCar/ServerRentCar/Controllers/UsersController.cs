using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerRentCar.Auth;
using ServerRentCar.Common.Enums;
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
        private AuthService _authService;
        private RecordService _recordService;
        private CarsService _carService;
        public Users(ILogger<Users> logger, rentdbContext rentdbContext, DataAautoMapper dataAautoMapper,
            UserService userService,AuthService authService,RecordService recordService, CarsService carService)
        {
            _logger = logger;
            _rentdbContext = rentdbContext;
            _dataAautoMapper = dataAautoMapper;
            _userService = userService;
            _authService = authService;
            _recordService = recordService;
            _carService = carService;
        }
                
        [HttpPost]
        [Route("Login")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Login(LogInModel login)
        {
            var user = _rentdbContext.Users.Find(login.UserName);
            if(user== null || user.Password != login.Password)
                return Ok(StatusCodes.Status401Unauthorized);
            return Ok(user);
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

        /// <summary>
        /// Retrives cars rent records per user
        /// </summary>
        /// <param name="userdId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("records/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetRecordPerUser(int userdId)
        {
            if (_authService.IsInRole(Role.Customer,userdId))
            {
                return Ok(_recordService.GetPerUser(userdId));
            }
            return Ok(StatusCodes.Status401Unauthorized);
        }

        /// <summary>
        /// /Get avalible cars for rent
        /// </summary>
        /// <param name="userdId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("freecars/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAvalibaleCars(int userdId)
        {
            if (_authService.IsInRole(Role.Customer, userdId))
            {
                return Ok(_carService.GetAvalibaleCars());
            }
            return Ok(StatusCodes.Status401Unauthorized);
        }
    }
}
 
