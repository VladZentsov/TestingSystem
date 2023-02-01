using DAL.Interfaces;
using DAL.TestingSystemDBContext;
using DAL.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class IdentityUserRepository: IIdentityUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DbSet<IdentityUser> _users;

        private readonly ITestingSystemDbContext _dbContext;

        public IdentityUserRepository(ITestingSystemDbContext dbContext, UserManager<IdentityUser> userManager,
           RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _users = dbContext.Set<IdentityUser>();

        }
        public async Task AddAsync(IdentityUser entity, string password, string role)
        {
            var userNameExists = await _userManager.FindByNameAsync(entity.UserName);
            if (userNameExists != null)
                throw new UserAlreadyExistsException("User with such name is already exists");

            var userEmailExists = await _userManager.FindByEmailAsync(entity.Email);
            if (userEmailExists != null)
                throw new UserAlreadyExistsException("User with such email is already exists");

            var result = await _userManager.CreateAsync(entity, password);
            if (!result.Succeeded)
                throw new NotSucceededOperationException("Creation user error");

            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(entity, role);
        }

        public async Task<IdentityUser> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityUser> GetByLoginInfo(string Username, string Password)
        {
            var user = await GetByUserName(Username);

            if (await _userManager.CheckPasswordAsync(user, Password))
            {
                return user;
            }


            throw new EntityNotFoundException("Incorrect password");
        }

        public async Task<IdentityUser> GetByUserName(string Username)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.UserName == Username);
            //var user = await _userManager.FindByNameAsync(Username);

            if (user == null)
                throw new EntityNotFoundException("Incorrect username");

            return user;
        }

        public async Task<IdentityResult> Update(IdentityUser entity)
        {
            return await _userManager.UpdateAsync(entity);
            _dbContext.SaveChanges();
        }

        public async Task<IdentityResult> Delete(IdentityUser entity)
        {
            var result = entity != null;
            if (result)
            {
                return await _userManager.DeleteAsync(entity);
                _dbContext.SaveChanges();
            }
            return IdentityResult.Failed();
        }

        public async Task DeleteByIdAsync(string id)
        {
            await Delete(await _userManager.FindByIdAsync(id));
        }
        public async Task<IEnumerable<IdentityUser>> FindAll()
        {
            return await _userManager.Users.ToListAsync();
        }
        public async Task<string> GetRoleByUserId(string id)
        {
            var user = await GetByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles.FirstOrDefault();
        }
    }
}
