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
        public Admin(ILogger<Admin> logger,RecordService recordService,
               AuthService userService, CarsService carService, IConfiguration configuration)
        {
            _logger = logger;
            _authService = userService;
            _recordService = recordService;
            _carService = carService;
            _configuration = configuration;


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
                    return Ok(_recordService.GetAllrecords());
                }
                return Ok(StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in GetRecords due to :" + e);
                return Ok(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpPost]
        [Route("editcars/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateCarsValue(int userdId ,[FromBody] CarsDTO car)
        {
            try
            {
                if (_authService.IsInRole(Role.Admin, userdId))
            {
                return Ok(_carService.UpdateCar(car));
            }
            return Ok(StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in UpdateCarsValue due to :" + e);
                return Ok(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes car from inventory
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
            try
            {
                if (_authService.IsInRole(Role.Admin, userdId))
            {
                return Ok(_carService.DeleteCar(licensePlate));
            }
            return Ok(StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in DeleteCar due to :" + e);
                return Ok(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// /Get avalible cars for rent
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
            return Ok(StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in GetAlllCars due to :" + e);
                return Ok(StatusCodes.Status500InternalServerError);
            }
        }
    }
}


