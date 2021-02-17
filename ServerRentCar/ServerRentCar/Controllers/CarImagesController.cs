using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerRentCar.DTO;
using ServerRentCar.Models;
using ServerRentCar.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarImages : ControllerBase
    {

        private readonly ILogger<Users> _logger;
        private rentdbContext _rentdbContext;
        private DataAautoMapper _dataAautoMapper;
        public CarImages(ILogger<Users> logger, rentdbContext rentdbContext, DataAautoMapper dataAautoMapper)
        {
            _logger = logger;
            _rentdbContext = rentdbContext;
            _dataAautoMapper = dataAautoMapper;
        }

        [HttpGet("{licensePlate}")]
        public IActionResult Get(string licensePlate)
        {
            var car = _rentdbContext.Cars.Find(licensePlate);
            return File(car.CarImage, "image/png");
        }
    }
}
