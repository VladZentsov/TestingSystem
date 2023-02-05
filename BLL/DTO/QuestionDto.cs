using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class QuestionDto:BaseDto
    {
        public int Number { get; set; }
        public string QuestionText { get; set; }
        public ICollection<AnswerDto> Answers { get; set; }
    }
}
