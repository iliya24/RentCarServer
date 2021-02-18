using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using ServerRentCar.Models;
using Microsoft.EntityFrameworkCore;
using ServerRentCar.DTO;
using ServerRentCar.Auth;
using ServerRentCar.Common.Enums;

namespace ServerRentCar.Utils
{
    public class DataAautoMapper 
    {
        private static IMapper _mapper;
        public DataAautoMapper()
        {
            _mapper = Init();

        }
        public IMapper Init()
        {
            var config = new MapperConfiguration(cfg => {


                cfg.CreateMap<User, UserDTO>()
                .ForMember(dst => dst.Gender, opt => opt.MapFrom(srs => ConvertUserSex(srs.Gender)));
                cfg.CreateMap<RegisterModel, User>();
                cfg.CreateMap<CarRentRecord, CarRentRecordDTO>();
                cfg.CreateMap<Car,CarsDTO> ();
                cfg.CreateMap<CarsDTO, Car>();
            });
            return config.CreateMapper();
        }

        private UserGender ConvertUserSex(bool gender)
        {
            if (gender)
                return UserGender.Male;
            return UserGender.Female;
        }       
        internal IEnumerable<Treturn> GetList<Tin, Treturn>(IEnumerable<Tin> objects)
        {
            return  _mapper.Map<IEnumerable<Treturn>>(objects);
        }
        internal  Treturn GetInstance<Tin, Treturn>(Tin obj)
        {
            return _mapper.Map<Treturn>(obj);
        }
    }
}
