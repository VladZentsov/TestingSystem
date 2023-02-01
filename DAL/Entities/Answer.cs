using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Answer:BaseEntity
    {
        public string QuestionId { get; set; }
        public string AnswerText { get; set; }
        public bool IsAnswerCorrect { get; set; }
        public Question Question { get; set; }
    }
}
