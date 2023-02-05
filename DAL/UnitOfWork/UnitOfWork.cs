using DAL.Interfaces;
using DAL.Repositories;
using DAL.TestingSystemDBContext;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ITestingSystemDbContext _testingSystemDbContext;
        private IIdentityUserRepository _identityUserRepository;
        private ITestRepository _testRepository;
        private IUserTestsRepository _userTestsRepository;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UnitOfWork(ITestingSystemDbContext testingSystemDbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _testingSystemDbContext = testingSystemDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IIdentityUserRepository IdentityUserRepository
        {
            get
            {
                if (_identityUserRepository == null)
                {
                    _identityUserRepository = new IdentityUserRepository(_testingSystemDbContext, _userManager, _roleManager);
                }
                return _identityUserRepository;
            }
        }
        public ITestRepository TestRepository
        {
            get
            {
                if (_testRepository == null)
                {
                    _testRepository = new TestRepository(_testingSystemDbContext);
                }
                return _testRepository;
            }
        }

        public IUserTestsRepository UserTestsRepository
        {
            get
            {
                if (_userTestsRepository == null)
                {
                    _userTestsRepository = new UserTestsRepository(_testingSystemDbContext);
                }
                return _userTestsRepository;
            }
        }

        public Task SaveAsync()
        {
            _testingSystemDbContext.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
