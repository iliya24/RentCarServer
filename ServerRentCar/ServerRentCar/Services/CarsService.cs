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
        public IEnumerable<CarsDTO> GetFreeCars(string ImageUri)
        {
            var freecars = (from cars in _rentdbContext.Cars
                            join cartypes in _rentdbContext.CarsTypes on cars.CarsTypesId equals cartypes.CarsTypesId
                            where cars.IsFreeForRent == true && cars.IsFreeForRent
                            select new CarsDTO()
                            {
                                LicensePlate = cars.LicensePlate,
                                CarsTypesId = cars.CarsTypesId,
                                Kilometer = cars.Kilometer,
                                IsValidForRent = cars.IsFreeForRent,
                                IsFreeForRent = cars.IsFreeForRent,

                                BranchId = cars.BranchId,
                                CarImage = ImageUri + "/Images/" + cars.LicensePlate,
                                Manufacture = cartypes.Manufacture,
                                Model = cartypes.Model,
                                PricePerDay = cartypes.PricePerDay,
                                DelayCostPerDay = cartypes.DelayCostPerDay,
                                YearRelease = cartypes.YearRelease,
                                Gear = cartypes.Gear,

                            }
                ).ToList();
            if (freecars != null)
            {
                return freecars;//
            }
            return null;
        }
        public IEnumerable<CarsDTO> GetAlllCars(string ImageUri)
        {
            var freecars = (from cars in _rentdbContext.Cars
                            join cartypes in _rentdbContext.CarsTypes on cars.CarsTypesId equals cartypes.CarsTypesId
                            select new CarsDTO()
                            {
                                LicensePlate = cars.LicensePlate,
                                CarsTypesId = cars.CarsTypesId,
                                Kilometer = cars.Kilometer,
                                IsValidForRent = cars.IsFreeForRent,
                                IsFreeForRent = cars.IsFreeForRent,

                                BranchId = cars.BranchId,
                                CarImage = ImageUri + "/Images/" + cars.LicensePlate,
                                Manufacture = cartypes.Manufacture,
                                Model = cartypes.Model,
                                PricePerDay = cartypes.PricePerDay,
                                DelayCostPerDay = cartypes.DelayCostPerDay,
                                YearRelease = cartypes.YearRelease,
                                Gear = cartypes.Gear,

                            }
                ).ToList();
             
            return freecars;
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
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
