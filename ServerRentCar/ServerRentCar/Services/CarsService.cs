using Microsoft.Extensions.Logging;
using ServerRentCar.Auth;
using ServerRentCar.Common.Enums;
using ServerRentCar.DTO;
using ServerRentCar.Models;
using ServerRentCar.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.Services
{
    public class CarsService
    {
        private readonly ILogger<UserService> _logger;
        private rentdbContext _rentdbContext;
        private DataAautoMapper _dataAautoMapper;
        public CarsService(ILogger<UserService> logger, rentdbContext rentdbContext, DataAautoMapper dataAautoMapper)
        {
            _rentdbContext = rentdbContext;
            _dataAautoMapper = dataAautoMapper;
            _logger = logger;
        }
       /// <summary>
       /// Get avalible cars for rent
       /// </summary>
       /// <returns></returns>
        public IEnumerable<CarsDTO> GetAvalibaleCars()
        {
            var cars = _rentdbContext.Cars.Where(car => car.IsFreeForRent).ToList();
            if (cars != null)
            {
                return _dataAautoMapper.GetDTOList<Car, CarsDTO>(cars);
            }
            return null;
        }
    }
}
