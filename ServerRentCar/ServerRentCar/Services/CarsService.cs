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
                return _dataAautoMapper.GetList<Car, CarsDTO>(cars);
            }
            return null;
        }

        internal object UpdateCar(CarsDTO car)
        {
           
            var dalCar = _dataAautoMapper.GetInstance<CarsDTO, Car>(car);
            _rentdbContext.Cars.Update(dalCar);
            _rentdbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// /Deletes car from inventory
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns></returns>
        internal bool DeleteCar(string licensePlate)
        {
            try
            {
                var car = _rentdbContext.Cars.Find(licensePlate);
                var carTypes = _rentdbContext.CarsTypes.Find(car.CarsTypesId);
                _rentdbContext.Cars.Remove(car);
                _rentdbContext.CarsTypes.Remove(carTypes);
                _rentdbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
