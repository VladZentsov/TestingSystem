using AutoMapper;
using BLL.DTO;
using BLL.Models;
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

            CreateMap<Test, TestNames>()
                .ReverseMap();

            CreateMap<TestDescriptionModel, Test>()
                .ReverseMap();

            CreateMap<QuestionDto, Question>()
                .ReverseMap();

            CreateMap<AnswerDto, Answer>()
                .ReverseMap();
        }
    }
}
