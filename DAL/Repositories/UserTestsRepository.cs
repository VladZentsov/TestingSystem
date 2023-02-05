using DAL.Entities;
using DAL.Interfaces;
using DAL.TestingSystemDBContext;
using DAL.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserTestsRepository : IUserTestsRepository
    {
        private ITestingSystemDbContext _dbContext;
        private readonly DbSet<UserTests> _tests;
        public UserTestsRepository(ITestingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
            _tests = dbContext.Set<UserTests>();
        }

        public void Add(UserTests entity)
        {
            _tests.Add(entity);
        }

        public async Task DeleteByIdAsync(string id)
        {
            Delete(await GetByIdAsync(id));
        }


        public async Task<UserTests> GetByIdAsync(string id)
        {
            var result = new UserTests();

            if (_tests == null)
                throw new ArgumentNullException("DbSet is null", "_tests");

            result = await _tests.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
                throw new EntityNotFoundException("Given Id not found.", "id");

            return result;
        }

        public async Task<UserTests> GetByIdWithDetailsAsync(string id)
        {
            var result = new UserTests();

            if (_tests == null)
                throw new ArgumentNullException("DbSet is null", "_tests");

            result = await _tests
                .Include(x => x.Test)
                .Include(x=>x.IdentityUser)
                .FirstOrDefaultAsync(z => z.Id == id);

            if (result == null)
                throw new EntityNotFoundException("Given Id not found.", "id");

            return result;
        }

        public async Task<IEnumerable<UserTests>> GetAllAsync()
        {
            return await _tests.ToListAsync();
        }

        public void Update(UserTests entity)
        {
            _tests.Update(entity);
        }

        public void Delete(UserTests entity)
        {
            _tests.Remove(entity);
        }

        public async Task<IEnumerable<UserTests>> GetAllByUserIdWithDetailsAsync(string userId)
        {
            if (_tests == null)
                throw new ArgumentNullException("DbSet is null", "_tests");

            var result = await _tests
                .Include(x => x.Test)
                .Include(x => x.IdentityUser)
                .Where(z => z.UserId == userId)
                .ToListAsync();

            if (result == null)
                throw new EntityNotFoundException("Given Id not found.", "id");

            return result;
        }
    }
}
