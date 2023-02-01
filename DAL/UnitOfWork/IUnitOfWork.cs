using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IIdentityUserRepository IdentityUserRepository { get; }
        public ITestRepository TestRepository { get; }
        Task SaveAsync();
    }
}
