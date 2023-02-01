using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<TestDto, Test>()
                .ReverseMap();
        }
    }
}
