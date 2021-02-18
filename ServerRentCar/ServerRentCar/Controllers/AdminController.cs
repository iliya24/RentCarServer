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
    public class Admin : ControllerBase
    {
        private readonly ILogger<Users> _logger;
        private RecordService _recordService;
        private AuthService _authService;
        private CarsService _carService;
        /// <summary>
        /// Contractor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="rentdbContext"></param>
        /// <param name="recordService"></param>
        /// <param name="dataAautoMapper"></param>
        /// <param name="userService"></param>
        public Admin(ILogger<Users> logger,RecordService recordService,
               AuthService userService, CarsService carService)
        {
            _logger = logger;
            _authService = userService;
            _recordService = recordService;
            _carService = carService;


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
        public IActionResult Get(int userdId)
        {
            if(_authService.IsInRole(Role.Admin,userdId))
            {
               return Ok( _recordService.GetAllrecords());
            }
            return Ok(StatusCodes.Status401Unauthorized);
        }

        [HttpPost]
        [Route("editcars/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateCarsValue(int userdId ,[FromBody] CarsDTO car)
        {
            if (_authService.IsInRole(Role.Admin, userdId))
            {
                return Ok(_carService.UpdateCar(car));
            }
            return Ok(StatusCodes.Status401Unauthorized);
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
            if (_authService.IsInRole(Role.Admin, userdId))
            {
                return Ok(_carService.DeleteCar(licensePlate));
            }
            return Ok(StatusCodes.Status401Unauthorized);
        }

    }
}


