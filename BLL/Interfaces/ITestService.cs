using BLL.DTO;
using BLL.Models;
using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITestService : ICrud<TestDto>
    {
        public Task<IEnumerable<TestNames>> GetAvaliableTestNames(string userId);
        public Task<TestDto> GetByIdWithDetailsAsync(string id);
        public Task<TestDescriptionModel> GetTestDescription(string testId);
        public Task<ResultModel> CheckTestAndGetResults(TestingAnswer testingAnswers);
    }
}
