using BLL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthenticateService
    {
        public Task RegisterAsync(RegisterModel model);
        public Task<JwtSecurityToken> Login(LoginModel model);
    }
}
