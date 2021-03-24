using Microsoft.Extensions.Logging;
using ServerRentCar.Auth;
using ServerRentCar.Common.Enums;
using ServerRentCar.Common.Util;
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
        private GenericClinetTableBuilder _genericClinetTableBuilder;
        public UserService(ILogger<UserService> logger, rentdbContext rentdbContext,
            DataAautoMapper dataAautoMapper, GenericClinetTableBuilder genericClinetTableBuilder)
        {
            _rentdbContext = rentdbContext;
            _dataAautoMapper = dataAautoMapper;
            _genericClinetTableBuilder = genericClinetTableBuilder;
            _logger = logger;
        }
        internal bool UserExist(RegisterModel registerModel)
        {

            return _rentdbContext.Users.Where(obj => obj.UserName == registerModel.UserName).FirstOrDefault() == null ? false : true;
        }

        internal object Register(RegisterModel registerModel)
        {
            try
            {
                var user = _dataAautoMapper.GetInstance<RegisterModel, User>(registerModel);
                user.Role = (byte)Role.Customer;
                _rentdbContext.Add(user);
                _rentdbContext.SaveChanges();
                return new { UserId = user.Id, UserRole = user.Role };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create user due to :{ex}");
                return null;
            }
        }

        internal object GetUsers()
        {
            var usersDTOList = _dataAautoMapper.GetList<User, UserDTO>(_rentdbContext.Users.ToList());
            return _genericClinetTableBuilder.BuildJsonTable<UserDTO>(usersDTOList.ToList());            
        }
        internal User GetUser(string userName)
        {
            return _rentdbContext.Users.Where(usr => usr.UserName == userName).FirstOrDefault();

        }
    }
}
