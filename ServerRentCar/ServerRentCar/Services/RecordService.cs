using Microsoft.Extensions.Logging;
using ServerRentCar.Auth;
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
    public class RecordService
    {
        private readonly ILogger<UserService> _logger;
        private rentdbContext _rentdbContext;
        private DataAautoMapper _dataAautoMapper;
        private GenericClinetTableBuilder _genericClinetTableBuilder;
        public RecordService(ILogger<UserService> logger,
            rentdbContext rentdbContext, DataAautoMapper dataAautoMapper,
            GenericClinetTableBuilder genericClinetTableBuilder)
        {
            _rentdbContext = rentdbContext;
            _dataAautoMapper = dataAautoMapper;
            _logger = logger;
            _genericClinetTableBuilder = genericClinetTableBuilder;
        }


        internal string GetAllrecordsAsJson()
        {
            var records = _dataAautoMapper.GetList<CarRentRecord, NewCarRentRecordDTO>(_rentdbContext.CarRentRecords);
            return _genericClinetTableBuilder.BuildJsonTable<NewCarRentRecordDTO>(records.ToList());
        }

        /// <summary>
        /// Gets all records
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<NewCarRentRecordDTO> GetAllrecord()
        {
            return _dataAautoMapper.GetList<CarRentRecord, NewCarRentRecordDTO>(_rentdbContext.CarRentRecords);
        }

        /// <summary>
        /// Gets  records per user
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<NewCarRentRecordDTO> GetPerUser(int userId)
        {
            var records = _rentdbContext.CarRentRecords.Where(rec => rec.UserId == userId);
            if (records != null)
                return _dataAautoMapper.GetList<CarRentRecord, NewCarRentRecordDTO>(records);
            else return null;
        }

        /// <summary>
        /// Adding new order to system
        /// </summary>
        /// <param name="carRentRecordDTO"></param>
        /// <returns></returns>
        internal bool MakAnOrder(NewCarRentRecordDTO carRentRecordDTO, int userId)
        {
            try
            {
                var car = _rentdbContext.Cars.Find(carRentRecordDTO.LicensePlate);
                if (car != null)
                {
                    car.IsFreeForRent = false;
                    var order = _dataAautoMapper.GetInstance<NewCarRentRecordDTO, CarRentRecord>(carRentRecordDTO);
                    order.UserId = userId;
                    _rentdbContext.CarRentRecords.Add(order);
                    _rentdbContext.SaveChanges();
                    return true;
                }
                else
                {
                    _logger.LogError($"could not find car per LicensePlate:{carRentRecordDTO.LicensePlate}");

                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in OrderCar due to :" + e);

                return false;
            }
        }
        /// <summary>
        /// Find and create odeer summary per client
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns></returns>

        internal OrderSummaryDTO FindOrder(string licensePlate)
        {

            var order = _rentdbContext.CarRentRecords.Where(ord => ord.LicensePlate == licensePlate && ord.ActualRentEndDate == null).FirstOrDefault();
            if (order != null)
            {

                var days = Math.Abs((DateTime.Today - order.StartRentDate ).TotalDays);
                var car = _rentdbContext.Cars.Find(licensePlate);
                var carType = _rentdbContext.CarsTypes.Find(car.CarsTypesId);
                var orderSummaryDTO = new OrderSummaryDTO();
                if (order.EndRentDate <= DateTime.Today)
                {
                    orderSummaryDTO.TotalCost = ((decimal)days * carType.PricePerDay).ToString();
                }
                else
                {
                    days = Math.Abs((order.EndRentDate -order.StartRentDate).TotalDays);

                    orderSummaryDTO.TotalCost = ((decimal)days * carType.PricePerDay).ToString();
                    days = Math.Abs((DateTime.Today - order.EndRentDate).TotalDays);
                    orderSummaryDTO.TotalCost = (decimal.Parse(orderSummaryDTO.TotalCost)+ ((decimal)days * carType.DelayCostPerDay)).ToString();

                }
                orderSummaryDTO.StartRentDate = order.StartRentDate.Date.ToString("yyyy-MM-dd"); ;
                orderSummaryDTO.EndRentDate = order.EndRentDate.Date.ToString("yyyy-MM-dd"); ;
                orderSummaryDTO.ActualRentEndDate = DateTime.Today.Date.ToString("yyyy-MM-dd");
                orderSummaryDTO.OrderId = order.RentRecordId.ToString();
                orderSummaryDTO.DelayCostPerDay = carType.DelayCostPerDay.ToString();
                orderSummaryDTO.PricePerDay = carType.PricePerDay.ToString();
                orderSummaryDTO.Manufacture = carType.Manufacture;
                orderSummaryDTO.Model = carType.Model;
                orderSummaryDTO.YearRelease= carType.YearRelease.Date.ToString("yyyy-MM-dd");
                orderSummaryDTO.Gear = carType.Gear;
                return orderSummaryDTO;
            }
            return null;
        }
        /// <summary>
        /// Car return and update is free for rent
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>

        internal bool ReturnCar(OrderSummaryDTO order)
        {
            try
            {

                var record = _rentdbContext.CarRentRecords.Find(int.Parse(order.OrderId));
                record.TotalCost = decimal.Parse(order.TotalCost);
                record.ActualRentEndDate = DateTime.Parse(order.ActualRentEndDate);
                var car = _rentdbContext.Cars.Find(record.LicensePlate);
                car.IsFreeForRent = true;
                _rentdbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in Return car due to :" + e);
                return false;
            }

        }

        /// <summary>
        /// Get all records per user
        /// </summary>
        /// <param name="filterByUserId"></param>
        /// <returns></returns>
        internal IEnumerable<NewCarRentRecordDTO> GetAllrecordsPerUser(int filterByUserId)
        {
            try
            {
                var records = _rentdbContext.CarRentRecords.Where(rec => rec.UserId == filterByUserId).ToList();
                return _dataAautoMapper.GetList<CarRentRecord, NewCarRentRecordDTO>(records);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in GetAllrecordsPerUser due to :" + e);
                return null;
            }
        }

        /// <summary>
        /// Get all records per user in json format
        /// </summary>
        /// <param name="filterByUserId"></param>
        /// <returns></returns>
        internal string GetAllrecordsPerUserJson(int filterByUserId)
        {
            try
            {
                var records = _rentdbContext.CarRentRecords.Where(rec => rec.UserId == filterByUserId).ToList();
                var mappedRecords = _dataAautoMapper.GetList<CarRentRecord, NewCarRentRecordDTO>(records);
                return _genericClinetTableBuilder.BuildJsonTable<NewCarRentRecordDTO>(mappedRecords.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in GetAllrecordsPerUser due to :" + e);
                return null;
            }
        }
    }
}
