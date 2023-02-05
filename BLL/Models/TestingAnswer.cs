using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class TestingAnswer
    {
        public string TestId { get; set; }
        public ICollection<AnswerDto> Answers { get; set; }
    }
}
