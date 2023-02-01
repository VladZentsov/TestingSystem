using BLL.DTO;
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
        public Task<IEnumerable<string>> GetAvaliableTestNames(string userId);
    }
}
