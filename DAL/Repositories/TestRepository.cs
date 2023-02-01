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
    public class TestRepository: ITestRepository
    {
        private ITestingSystemDbContext _dbContext;
        private readonly DbSet<Test> _tests;
        public TestRepository(ITestingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
            _tests = dbContext.Set<Test>();
        }

        public void Add(Test entity)
        {
            _tests.Add(entity);
        }

        public async Task DeleteByIdAsync(string id)
        {
            Delete(await GetByIdAsync(id));
        }


        public async Task<Test> GetByIdAsync(string id)
        {
            var result = new Test();

            if (_tests == null)
                throw new ArgumentNullException("DbSet is null", "_tests");

            result = await _tests.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
                throw new EntityNotFoundException("Given Id not found.", "id");

            return result;
        }

        public async Task<Test> GetByIdWithDetailsAsync(string id)
        {
            var result = new Test();

            if (_tests == null)
                throw new ArgumentNullException("DbSet is null", "_tests");

            result = await _tests
                .Include(x => x.Questions)
                .FirstOrDefaultAsync(z => z.Id == id);

            if (result == null)
                throw new EntityNotFoundException("Given Id not found.", "id");

            return result;
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await _tests.ToListAsync();
        }

        public void Update(Test entity)
        {
            _tests.Update(entity);
        }

        public void Delete(Test entity)
        {
            _tests.Remove(entity);
        }

        public async Task<IEnumerable<Test>> GetAllWithDetailsAsync()
        {
            //var result = new Test();

            if (_tests == null)
                throw new ArgumentNullException("DbSet is null", "_tests");

            var result = _tests
                .Include(x => x.Questions);

            if (result == null)
                throw new EntityNotFoundException("Given Id not found.", "id");

            return result;
        }
    }
}

