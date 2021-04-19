using CRUD_NET5.Data.Entities;
using CRUD_NET5.ViewModels.Common;
using CRUD_NET5.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRUD_NET5.Application.Systems.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = configuration;
        }

        /// <summary>
        /// Get all list user
        /// </summary>
        /// <returns></returns>
        public async Task<APIResult<List<UserVM>>> GetAll()
        {
            var result = await _userManager.Users?.Select(x => new UserVM
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Dob = x.Dob,
                Email = x.Email,
                UserName = x.UserName,
                PhoneNumber = x.PhoneNumber
            }).ToListAsync();

            return new APISuccessResult<List<UserVM>>(result);
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<APIResult<bool>> RegisterUser(RegisterRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.UserName);
            if (user != null) 
                return new APIErrorResult<bool>("Tài khoản đã tồn tại");
              
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return new APIErrorResult<bool>("Địa chỉ email đã tồn tại");

            var userRegister = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dob = request.Dob,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email
            };
            var result = await _userManager.CreateAsync(userRegister);
            if (result != null) 
                return new APISuccessResult<bool>();

            return new APIErrorResult<bool>("Đăg ký không thành công");

        }

        /// <summary>
        /// Authenticate and return new jwt
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<APIResult<string>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new APIErrorResult<string>("Tài khoản không tồn tại");
            var checkLogin = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!checkLogin) return new APIErrorResult<string>("UserName or password không chính xác");

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Role, string.Join("", roles)),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issues"],
                                             _config["Tokens:Issues"],
                                             claims,
                                             expires: DateTime.Now.AddHours(3),
                                             signingCredentials: creds);

            return new APISuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
