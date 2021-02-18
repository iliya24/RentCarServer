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
    public class RecordService
    {
        private readonly ILogger<UserService> _logger;
        private rentdbContext _rentdbContext;
        private DataAautoMapper _dataAautoMapper;
        public RecordService(ILogger<UserService> logger, rentdbContext rentdbContext, DataAautoMapper dataAautoMapper)
        {
            _rentdbContext = rentdbContext;
            _dataAautoMapper = dataAautoMapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets all records
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<CarRentRecordDTO> GetAllrecords()
        {
           return _dataAautoMapper.GetDTOList<CarRentRecord, CarRentRecordDTO> (_rentdbContext.CarRentRecords);
        }

        /// <summary>
        /// Gets  records per user
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<CarRentRecordDTO> GetPerUser(int userId)
        {
            var records = _rentdbContext.CarRentRecords.Where(rec => rec.UserId == userId);
            if (records != null)
                return _dataAautoMapper.GetDTOList<CarRentRecord, CarRentRecordDTO>(records);
            else return null;
        }
    }
}
