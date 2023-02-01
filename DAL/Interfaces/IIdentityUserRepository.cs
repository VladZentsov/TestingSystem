using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IIdentityUserRepository
    {
        Task<IEnumerable<IdentityUser>> FindAll();

        Task<IdentityUser> GetByIdAsync(string id);

        Task AddAsync(IdentityUser entity, string password, string role);

        Task<IdentityResult> Update(IdentityUser entity);

        Task<IdentityResult> Delete(IdentityUser entity);

        Task DeleteByIdAsync(string id);

        Task<IdentityUser> GetByLoginInfo(string Username, string Password);

        Task<IdentityUser> GetByUserName(string Username);
        Task<string> GetRoleByUserId(string id);

    }
}
