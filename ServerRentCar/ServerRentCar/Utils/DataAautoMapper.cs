using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using ServerRentCar.Models;
using Microsoft.EntityFrameworkCore;
using ServerRentCar.DTO;

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


                cfg.CreateMap<User, DTO.UserDTO>();
            });
            return config.CreateMapper();
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
