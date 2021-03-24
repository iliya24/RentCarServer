using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServerRentCar.Auth;
using ServerRentCar.Common;
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
    public class WorkerController : ControllerBase
    {
        private readonly ILogger<WorkerController> _logger;
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
        public WorkerController(ILogger<WorkerController> logger, RecordService recordService,
               AuthService userService, CarsService carService, IConfiguration configuration)
        {
            _logger = logger;
            _authService = userService;
            _recordService = recordService;
            _carService = carService;
            _configuration = configuration;


        }


        [HttpPost]
        [Route("returncar/{userdId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ReturnCar(int userdId, [FromBody] ReturnCar car)
        {
            if (_authService.IsInRole(Role.Worker, userdId))
            {
                return Ok(_carService.ReturnCar(car));
            }
            return Unauthorized(StatusCodes.Status401Unauthorized);
        }


        [HttpGet]
        [Route("findOrder/{userdId}/{licensePlate}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult FindOrder(int userdId, string licensePlate)
        {
            if (_authService.IsInRole(Role.Worker, userdId))
            {
                var record = _recordService.FindOrder(licensePlate);
                _recordService.ReturnCar(record);
                if (record != null)
                {
                    return Ok(record);
                }
                return BadRequest("Did not find any active record");
            }
            return Unauthorized(StatusCodes.Status401Unauthorized);
        }


        [HttpPost]
        [Route("return/{userdId}/{orderId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ReturnOrder(int userdId, [FromBody] OrderSummaryDTO order)
        {
            if (_authService.IsInRole(Role.Worker, userdId))
            {
                if (_recordService.ReturnCar(order))
                    return Ok();
                else return Ok(StatusCodes.Status500InternalServerError);

            }
            return Unauthorized(StatusCodes.Status401Unauthorized);
        }
    }
}


