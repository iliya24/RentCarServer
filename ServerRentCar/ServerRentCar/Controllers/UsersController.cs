using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerRentCar.Auth;
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
    public class Users : ControllerBase
    {

        private readonly ILogger<Users> _logger;
        private rentdbContext _rentdbContext;
        private DataAautoMapper _dataAautoMapper;
        public Users(ILogger<Users> logger, rentdbContext rentdbContext, DataAautoMapper dataAautoMapper)
        {
            _logger = logger;
            _rentdbContext = rentdbContext;
            _dataAautoMapper = dataAautoMapper;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get()
        {
            return Ok(_dataAautoMapper.GetDTOList<User, UserDTO>(_rentdbContext.Users));
        }


        [HttpPost]
        [Route("Login")]
        [Produces("application/json")]
        public IActionResult Login(LogInModel login)
        {
            var user=_rentdbContext.Users.Find(login.UserName);
            return Ok(user != null && user.Password == login.Password);
            
        }
    }
}
