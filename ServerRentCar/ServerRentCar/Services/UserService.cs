﻿using Microsoft.Extensions.Logging;
using ServerRentCar.Auth;
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

        internal bool Register(RegisterModel registerModel)
        {
            try
            {
                var user = _dataAautoMapper.GetDTOInstance<RegisterModel, User>(registerModel);
                _rentdbContext.Add(user);
                _rentdbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Faield to create user due to :{ex}");
                return false;
            }

        }
    }
}