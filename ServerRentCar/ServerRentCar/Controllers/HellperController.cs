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
    public class HellperController : ControllerBase
    {

        private readonly ILogger<Users> _logger;
        private rentdbContext _rentdbContext;
        private DataAautoMapper _dataAautoMapper;
        public HellperController(ILogger<Users> logger, rentdbContext rentdbContext, DataAautoMapper dataAautoMapper)
        {
            _logger = logger;
            _rentdbContext = rentdbContext;
            _dataAautoMapper = dataAautoMapper;
        }


        [HttpPost]
        public IActionResult AddImageTocar(Hellper hellper)
        {
            var car = _rentdbContext.Cars.Find(hellper.licensePlate);
            car.CarImage = System.IO.File.ReadAllBytes(hellper.filePath);
            _rentdbContext.SaveChanges();
            return File(car.CarImage, "image/png");
        }
    }

}
public class Hellper
{
    public string licensePlate { get; set; }
    public string filePath { get; set; }

}