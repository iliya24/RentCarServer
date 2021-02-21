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
        private readonly ILogger<Users> _logger;
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
        public WorkerController(ILogger<Users> logger,RecordService recordService,
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
        public IActionResult ReturnCar(int userdId ,[FromBody] ReturnCar car)
        {
            if (_authService.IsInRole(Role.Worker, userdId))
            {
                return Ok(_carService.ReturnCar(car));
            }
            return Ok(StatusCodes.Status401Unauthorized);
        }
 
    }
}


