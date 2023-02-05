using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserTestsRepository : IRepository<UserTests>
    {
        public Task<IEnumerable<UserTests>> GetAllByUserIdWithDetailsAsync(string userId);
    }
}
