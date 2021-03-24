using Microsoft.Extensions.Logging;
using ServerRentCar.Auth;
using ServerRentCar.Common;
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
    public class CarsService
    {
        private readonly ILogger<CarsService> _logger;
        private rentdbContext _rentdbContext;
        private DataAautoMapper _dataAautoMapper;
        private GenericClinetTableBuilder _genericClinetTableBuilder;
        public CarsService(ILogger<CarsService> logger, rentdbContext rentdbContext,
            DataAautoMapper dataAautoMapper, GenericClinetTableBuilder genericClinetTableBuilder)
        {
            _rentdbContext = rentdbContext;
            _dataAautoMapper = dataAautoMapper;
            _logger = logger;
            _genericClinetTableBuilder = genericClinetTableBuilder;
        }
        /// <summary>
        /// Get avalible cars for rent
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CarsAsTableDTO> GetFreeCars(string ImageUri)
        {
            var freecars = (from cars in _rentdbContext.Cars
                            join cartypes in _rentdbContext.CarsTypes on cars.CarsTypesId equals cartypes.CarsTypesId
                            where cars.IsFreeForRent == true && cars.IsFreeForRent
                            select new CarsAsTableDTO()
                            {
                                LicensePlate = cars.LicensePlate,
                                //CarsTypesId = cars.CarsTypesId,
                                Kilometer = cars.Kilometer,
                                branchId = cars.BranchId,
                                IsFreeForRent = cars.IsFreeForRent,
                                IsValidForRent =cars.IsValidForRent,
                                CarImage = ImageUri + "/Images/" + cars.LicensePlate,
                                Manufacture = cartypes.Manufacture,
                                Model = cartypes.Model,
                                PricePerDay = cartypes.PricePerDay,
                                DelayCostPerDay = cartypes.DelayCostPerDay,
                                YearRelease = cartypes.YearRelease.ToString("yyyy"),
                                Gear = cartypes.Gear,

                            }
                ).ToList();


            if (freecars != null)
            {
                var branches = _rentdbContext.Branches.ToList();
                foreach (var car in freecars)
                {
                    car.BranchName = branches.Where(br => br.BranchId == car.branchId).Select(br => br.BranchName).FirstOrDefault();
                }
                return freecars;//
            }
            return null;
        }

        public IEnumerable<FullCarsAsTableDTO> GetAlllCars(string ImageUri)
        {
            try
            {
                var freecars = (from cars in _rentdbContext.Cars
                                join cartypes in _rentdbContext.CarsTypes on cars.CarsTypesId equals cartypes.CarsTypesId
                                select new FullCarsAsTableDTO()
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
                                    YearRelease = cartypes.YearRelease.ToString("yyyy"),
                                    Gear = cartypes.Gear,

                                }
                ).ToList();

                 
                return freecars;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in GetAlllCars due to :" + e);
                return null;
            }
        }

        public string GetAlllCarsJson()
        {
            try
            {
                var freecars = _rentdbContext.Cars.ToList();
                var mapped = _dataAautoMapper.GetList<Car, CarTableDTO>(freecars.ToList());
                return _genericClinetTableBuilder.BuildJsonTable<CarTableDTO>(mapped.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in GetAlllCarsJson due to :" + e);
                return null;
            }
        }

        public string GetAlllCarsTypesJson()
        {
            try
            {
                var freecars = _rentdbContext.CarsTypes.ToList();
                var mapped = _dataAautoMapper.GetList<CarsType, CarModelsTableDTO>(freecars.ToList());
                return _genericClinetTableBuilder.BuildJsonTable<CarModelsTableDTO>(mapped.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in GetAlllCarsTypesJson due to :" + e);
                return null;
            }
        }
        internal bool ReturnCar(ReturnCar returnCar)
        {
            try
            {
                var car = _rentdbContext.Cars.Find(returnCar.LicensePlate);
                car.IsFreeForRent = true;
                car.IsFreeForRent = true;
                _rentdbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in ReturnCar due to :" + e);

                return false;
            }
        }
        /// <summary>
        /// Updates cartypes object in data base
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        internal object UpdateCarTypes(CarsAsTableDTO car)
        {
            try
            {
                var editedCarType = _dataAautoMapper.GetInstance<CarsAsTableDTO, CarsType>(car);
                var orgCar = _rentdbContext.Cars.Find(car.LicensePlate);
                editedCarType.CarsTypesId = orgCar.CarsTypesId;
                _rentdbContext.CarsTypes.Update(editedCarType);
                _rentdbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in UpdateCarTypes due to :" + e);
                return false;
            }
        }

        /// <summary>
        /// Updates car object in data base
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>

        internal bool UpdateCar(CarsAsTableDTO car)
        {
            try
            {
                var editedCar = _dataAautoMapper.GetInstance<CarsAsTableDTO, Car>(car);
                var orgCar = _rentdbContext.Cars.Find(car.LicensePlate);
                editedCar.CarImage = orgCar.CarImage;
                _rentdbContext.Cars.Update(editedCar);
                _rentdbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in UpdateCar due to :" + e);
                return false;
            }
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
                _rentdbContext.Cars.Remove(car);
                _rentdbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in DeleteCar due to :" + e);

                return false;
            }
        }
        /// <summary>
        /// /Deletes carsTypes by carsTypesId from inventory
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns></returns>
        internal bool DeleteCarType(int carsTypesId)
        {
            try
            {
                var carType = _rentdbContext.CarsTypes.Find(carsTypesId);
                _rentdbContext.CarsTypes.Remove(carType);
                _rentdbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in DeleteCarType due to :" + e);

                return false;
            }
        }

    }
}
