using Microsoft.Extensions.Logging;
using ServerRentCar.Auth;
using ServerRentCar.Common.Enums;
using ServerRentCar.Models;
using ServerRentCar.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.Services
{
    public class AuthService
    {
        private readonly ILogger<UserService> _logger;
        private rentdbContext _rentdbContext;
         public AuthService(ILogger<UserService> logger, rentdbContext rentdbContext)
        {
            _rentdbContext = rentdbContext;
            
            _logger = logger;
        }
        /// <summary>
        /// Checks if user is in role  return true else exist or not admin false
        /// </summary>
        /// <param name="role"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal bool IsInRole(Role role,int userId)
        {
            var user = _rentdbContext.Users.Where(obj => obj.Id == userId).FirstOrDefault();
            if (user == null)
                return false;
            else if (user.Role == (byte)role)
            {
                return true;
            }
            return false;
        }

       

         
    }
}
