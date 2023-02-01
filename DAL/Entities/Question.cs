using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Question: BaseEntity
    {
        public string TestId { get; set; }
        public int Number { get; set; }
        public string QuestionText { get; set; }
        public Test Test { get; set; }
        public ICollection<Answer> Answers { get; set; }

    }
}
