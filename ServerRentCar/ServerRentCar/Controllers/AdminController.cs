using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class Admin : ControllerBase
    {
        private readonly ILogger<Admin> _logger;
        private RecordService _recordService;
        private AuthService _authService;
        private CarsService _carService;
        private UserService _userService;
        IConfiguration _configuration;
        /// <summary>
        /// Contractor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="rentdbContext"></param>
        /// <param name="recordService"></param>
        /// <param name="dataAautoMapper"></param>
        /// <param name="userService"></param>
        /// <param name="carService"></param>
        /// <param name="_configuration"></param>
        public Admin(ILogger<Admin> logger, RecordService recordService,
               UserService userService, CarsService carService, IConfiguration configuration,
               AuthService authService)

        {
            _logger = logger;
            _authService = authService;
            _recordService = recordService;
            _carService = carService;
            _configuration = configuration;
            _userService = userService;

        }
        /// <summary>
        /// Retrives users json test
        /// </summary>
        /// <param name="userdId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("usersjson/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsersJson(int userdId)
        {
            try
            {
                if (_authService.IsInRole(Role.Admin, userdId))
                {
                    return Ok(_userService.GetUsers());
                }
                return Unauthorized(StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in GetRecords due to :" + e);
                return BadRequest(StatusCodes.Status500InternalServerError);
            }

        }
        /// <summary>
        /// Retrives cars rent records
        /// </summary>
        /// <param name="userdId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("records/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetRecords(int userdId)
        {
            try
            {
                if (_authService.IsInRole(Role.Admin, userdId))
                {
                    return Ok(_recordService.GetAllrecord());
                }
                return Unauthorized(StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in GetRecords due to :" + e);
                return BadRequest(StatusCodes.Status500InternalServerError);
            }

        }
        /// <summary>
        /// Retrives cars rent record as Json
        /// </summary>
        /// <param name="userdId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Jsonrecords/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetRecordsJson(int userdId)
        {
            try
            {
                if (_authService.IsInRole(Role.Admin, userdId))
                {
                    return Ok(_recordService.GetAllrecordsAsJson());
                }
                return Unauthorized(StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in GetRecords due to :" + e);
                return BadRequest(StatusCodes.Status500InternalServerError);
            }

        }
        /// <summary>
        /// /Get all cars for rent
        /// </summary>
        /// <param name="userdId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("allcars/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAlllCars(int userdId)
        {
            try
            {
                if (_authService.IsInRole(Role.Admin, userdId))
                {
                    var ImageUri = _configuration.GetValue<string>("ImageUri");
                    return Ok(_carService.GetAlllCars(ImageUri));
                }
                return Unauthorized(StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in GetAlllCars due to :" + e);
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// /Get all cars for rent in json format
        /// </summary>
        /// <param name="userdId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("jsonallcars/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAlllCarsJson(int userdId)
        {
            try
            {
                if (_authService.IsInRole(Role.Admin, userdId))
                {
                    return Ok(_carService.GetAlllCarsJson());
                }
                return Unauthorized(StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in GetAlllCarsJson due to :" + e);
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// /Get all carstypes for rent in json format
        /// </summary>
        /// <param name="userdId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("jsonallcarstypes/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAlllCarsTypesJson(int userdId)
        {
            try
            {
                if (_authService.IsInRole(Role.Admin, userdId))
                {
                    return Ok(_carService.GetAlllCarsTypesJson());
                }
                return Unauthorized(StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in GetAlllCarsTypesJson due to :" + e);
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retrives cars rent records per user
        /// </summary>
        /// <param name="userdId"></param>
        /// /// <param name="filterByUserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("records/{userdId}/{filterByUserId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetRecordsPerUser(int userdId, int filterByUserId)
        {

            if (_authService.IsInRole(Role.Admin, userdId))
            {
                return Ok(_recordService.GetAllrecordsPerUser(filterByUserId));
            }
            return Unauthorized(StatusCodes.Status401Unauthorized);
        }

        /// <summary>
        /// Retrives cars rent records per user Json
        /// </summary>
        /// <param name="userdId"></param>
        /// /// <param name="filterByUserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("jsonrecords/{userdId}/{filterByUserId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetRecordsPerUserJson(int userdId, int filterByUserId)
        {

            if (_authService.IsInRole(Role.Admin, userdId))
            {
                return Ok(_recordService.GetAllrecordsPerUserJson(filterByUserId));
            }
            return Unauthorized(StatusCodes.Status401Unauthorized);
        }

        [HttpPost]
        [Route("editcars/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateCarsValue(int userdId, [FromBody] IEnumerable<CarTableDTO> car)
        {
            return Ok("Ok");
            //if (_authService.IsInRole(Role.Admin, userdId))
            //{
            //    return Ok(_carService.UpdateCar(car));
            //}
            //return Unauthorized(StatusCodes.Status401Unauthorized);
        }

        [HttpPost]
        [Route("editcartypes/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateCarTypesValue(int userdId, [FromBody] IEnumerable<CarModelsTableDTO> car)
        {
            return Ok("OK");
            //if (_authService.IsInRole(Role.Admin, userdId))
            //{
            //    return Ok(_carService.UpdateCarTypes(car));
            //}
            //return Unauthorized(StatusCodes.Status401Unauthorized);
        }

        [HttpPost]
        [Route("editrecord/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult EditRecord(int userdId, [FromBody] IEnumerable<NewCarRentRecordDTO> record)
        {
            return Ok("OK");
            //if (_authService.IsInRole(Role.Admin, userdId))
            //{
            //    return Ok(_carService.UpdateCarTypes(car));
            //}
            //return Unauthorized(StatusCodes.Status401Unauthorized);
        }

        [HttpPost]
        [Route("editruser/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult EditUser(int userdId, [FromBody] IEnumerable<UserDTO> record)
        {
            return Ok("OK");
            //if (_authService.IsInRole(Role.Admin, userdId))
            //{
            //    return Ok(_carService.UpdateCarTypes(car));
            //}
            //return Unauthorized(StatusCodes.Status401Unauthorized);
        }




        /// <summary>
        /// Delete car from inventory
        /// </summary>
        /// <param name="userdId"></param>
        /// <param name="licensePlate"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("remove/{userdId}/{licensePlate}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteCar(int userdId, string licensePlate)
        {
            if (_authService.IsInRole(Role.Admin, userdId))
            {
                return Ok(_carService.DeleteCar(licensePlate));
            }
            return Unauthorized(StatusCodes.Status401Unauthorized);
        }

        /// <summary>
        /// Delete carsTypesId from inventory by carsTypesId
        /// </summary>
        /// <param name="userdId"></param>
        /// <param name="carsTypesId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("remove/{userdId}/{carsTypesId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteCarType(int userdId, int carsTypesId)
        {
            if (_authService.IsInRole(Role.Admin, userdId))
            {
                return Ok(_carService.DeleteCarType(carsTypesId));
            }
            return Unauthorized(StatusCodes.Status401Unauthorized);
        }



    }
}


