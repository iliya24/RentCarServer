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

        /// <summary>
        /// Contractor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="rentdbContext"></param>
        /// <param name="recordService"></param>
        /// <param name="dataAautoMapper"></param>
        /// <param name="userService"></param>
        public Admin(ILogger<Users> logger,RecordService recordService,
               AuthService userService)
        {
            _logger = logger;
            _authService = userService;
            _recordService = recordService;
           
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
    }
}


