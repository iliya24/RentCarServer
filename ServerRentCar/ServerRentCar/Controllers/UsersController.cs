using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerRentCar.Auth;
using ServerRentCar.Common.Enums;
using Microsoft.Extensions.Configuration;
using ServerRentCar.Models;
using ServerRentCar.Services;
using ServerRentCar.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ServerRentCar.DTO;

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
        private IConfiguration _configuration;

        public Users(ILogger<Users> logger, rentdbContext rentdbContext, DataAautoMapper dataAautoMapper,
            UserService userService, AuthService authService, RecordService recordService, CarsService carService,
            IConfiguration configuration)
        {
            _logger = logger;
            _rentdbContext = rentdbContext;
            _dataAautoMapper = dataAautoMapper;
            _userService = userService;
            _authService = authService;
            _recordService = recordService;
            _carService = carService;
            _configuration = configuration;

        }
        /// <summary>
        /// login user to system an returns user order records
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("Login")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Login(LogInModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userService.GetUser(login.UserName);
                    if (user == null || user.Password != login.Password)
                        return Unauthorized(StatusCodes.Status401Unauthorized);
                    else
                    {
                        var records = _recordService.GetAllrecordsPerUser(user.Id);
                        return Ok(new
                        {
                            UserId = user.Id,
                            UserRole =
                           ((Role)Enum.Parse(typeof(Role), user.Role.ToString())).ToString(),
                            UserRecords = records
                        });

                    }
                }
                else return ValidationProblem("One of the fileds is wrong", "", StatusCodes.Status500InternalServerError);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in Login due to :" + e);
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// login user to system an returns user order records Json Wise
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("JsonLogin")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult LoginJson(LogInModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userService.GetUser(login.UserName);
                    if (user == null || user.Password != login.Password)
                        return Unauthorized(StatusCodes.Status401Unauthorized);
                    else
                    {
                        var records = _recordService.GetAllrecordsPerUserJson(user.Id);
                        return Ok(new
                        {
                            UserId = user.Id,
                            UserRole =
                           ((Role)Enum.Parse(typeof(Role), user.Role.ToString())).ToString(),
                            UserRecords = records
                        });

                    }
                }
                else return ValidationProblem("One of the fileds is wrong", "", StatusCodes.Status500InternalServerError);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in Login due to :" + e);
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register(RegisterModel registerModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (_userService.UserExist(registerModel))
                        return BadRequest(StatusCodes.Status409Conflict);
                    else
                    {

                        var user = _userService.Register(registerModel);
                        if (user != null)
                        {
                            return Ok(user);
                        }
                        return BadRequest(StatusCodes.Status409Conflict);
                    }
                }
                else
                {
                    return ValidationProblem("One of the fileds is wrong", "", StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in Register due to :" + e);
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetRecordPerUser(int userdId)
        {
            try
            {

                if (_authService.IsInRole(Role.Customer, userdId))
                {
                    return Ok(_recordService.GetPerUser(userdId));
                }

                return Unauthorized(StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in GetRecordPerUser due to :" + e);
                return Ok(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// /Get avalible cars for rent
        /// </summary>
        /// <param name="userdId"></param>
        /// <returns></returns>

        /// <summary>
        /// /Get avalible cars for rent
        /// </summary>
        /// <param name="userdId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("freecars")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAvalibaleCars()
        {
            var ImageUri = _configuration.GetValue<string>("ImageUri");
            return Ok(_carService.GetFreeCars(ImageUri));
        }
        /// <summary>
        /// Order crate api for cutomer
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="carRentRecordDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("order/{userId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult OrderCar(int userId, [FromBody] NewCarRentRecordDTO carRentRecordDTO)
        {
            try
            {
                if (_authService.IsInRole(Role.Customer, userId))
                {

                    if (_recordService.MakAnOrder(carRentRecordDTO, userId))
                    {

                        var records = _recordService.GetAllrecordsPerUser(userId);
                        return Ok(new
                        {

                           UserRecords = records
                        });

                    }
                    else return Ok(StatusCodes.Status500InternalServerError);

                }
                else
                {
                    return Unauthorized(StatusCodes.Status401Unauthorized);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in OrderCar due to :" + e);
                return Ok(StatusCodes.Status500InternalServerError);
            }
        }

    }
}

