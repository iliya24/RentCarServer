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
            });
            return config.CreateMapper();
        }

        private UserSex ConvertUserSex(bool gender)
        {
            if (gender)
                return UserSex.Male;
            return UserSex.Female;
        }


        //internal IEnumerable<UserDTO> GetUserDTO(IEnumerable<User> users)
        //{
        //    return _mapper.Map<IEnumerable<UserDTO>>(users);
        //}
        internal IEnumerable<Treturn> GetDTOList<Tin, Treturn>(IEnumerable<Tin> objects)
        {


            return  _mapper.Map<IEnumerable<Treturn>>(objects);
        }
        internal  Treturn GetDTOInstance<Tin, Treturn>(Tin obj)
        {
            return _mapper.Map<Treturn>(obj);
        }


    }
}
