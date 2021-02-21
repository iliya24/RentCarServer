using Microsoft.Extensions.Logging;
using ServerRentCar.Auth;
using ServerRentCar.DTO;
using ServerRentCar.Models;
using ServerRentCar.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.Services
{
    public class UserService
    {
        private readonly ILogger<UserService> _logger;
        private rentdbContext _rentdbContext;
        private DataAautoMapper _dataAautoMapper;
        public UserService(ILogger<UserService> logger, rentdbContext rentdbContext, DataAautoMapper dataAautoMapper)
        {
            _rentdbContext = rentdbContext;
            _dataAautoMapper = dataAautoMapper;
            _logger = logger;
        }
        internal bool UserExist(RegisterModel registerModel)
        {
            
            return _rentdbContext.Users.Where(obj => obj.UserName == registerModel.UserName).FirstOrDefault() == null ? false : true;
        }

        internal UserDTO Register(RegisterModel registerModel)
        {
            try
            {
                var user = _dataAautoMapper.GetInstance<RegisterModel, User>(registerModel);
                _rentdbContext.Add(user);
                _rentdbContext.SaveChanges();
               var userDTO= _dataAautoMapper.GetInstance<User, UserDTO>(user);
                return userDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Faield to create user due to :{ex}");
                return null;
            }

        }
    }
}
