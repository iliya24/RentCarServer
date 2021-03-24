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
                .ForMember(dst => dst.BirthDate, drc => drc.MapFrom(src => src.BirthDate.Value.ToString("yyyy-MM-dd")));               
                cfg.CreateMap<RegisterModel, User>();
                cfg.CreateMap<CarRentRecord, NewCarRentRecordDTO>()
                 .ForMember(dst => dst.StartRentDate, drc => drc.MapFrom(src => src.StartRentDate.ToString("yyyy-MM-dd")))
                 .ForMember(dst => dst.EndRentDate, drc => drc.MapFrom(src => src.EndRentDate.ToString("yyyy-MM-dd")));
               // .ForMember(dst => dst.ActualRentEndDate, drc => drc.MapFrom(src => src.ActualRentEndDate != null?src.ActualRentEndDate.Value.ToString("yyyy-MM-dd"):""));                
                cfg.CreateMap<CarRentRecord, ReturnCarRentRecordDTO>();
                cfg.CreateMap<CarsType, CarModelsTableDTO>()
                  .ForMember(dst => dst.YearRelease, drc => drc.MapFrom(src => src.YearRelease.ToString("yyyy-MM-dd")));
               

                
                cfg.CreateMap<Car,CarsAsTableDTO> ();
                cfg.CreateMap<CarsAsTableDTO, Car>();
                cfg.CreateMap<Car, CarTableDTO>();
                
                cfg.CreateMap<NewCarRentRecordDTO, CarRentRecord>();
            });
            return config.CreateMapper();
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
