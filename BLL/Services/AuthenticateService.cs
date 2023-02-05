using AutoMapper;
using BLL.Helpers;
using BLL.Interfaces;
using BLL.Models;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthenticateService: IAuthenticateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticateService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<JwtSecurityToken> Login(LoginModel model)
        {
            var user = await _unitOfWork.IdentityUserRepository.GetByLoginInfo(model.Username, model.Password);
            var userRole = await _unitOfWork.IdentityUserRepository.GetRoleByUserId(user.Id);

            var authClaims = new List<Claim>
                {
                    new Claim("Name", user.UserName),
                    new Claim("id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            authClaims.Add(new Claim(ClaimTypes.Role, userRole));

            var token = TokenHelper.GetToken(authClaims, _configuration);

            return token;
        }
        public async Task RegisterAsync(RegisterModel model)
        {
            string id = Guid.NewGuid().ToString();

            string role = "User";

            string identityId = Guid.NewGuid().ToString();
            IdentityUser user = new IdentityUser()
            {
                Id = identityId,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            await _unitOfWork.IdentityUserRepository.AddAsync(user, model.Password, role);
        }

    }
}
