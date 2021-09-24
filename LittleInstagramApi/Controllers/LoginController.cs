using LittleInstagramApi.Interfaces;
using LittleInstagramApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LittleInstagramApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private IUserRepository _userRepository;

        public LoginController(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString, email = user.Email });
            }

            return response;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public IActionResult ListUsers()
        {
            try
            {
                IEnumerable<UserEntity> users = _userRepository.GetList();

                if (users != null)
                {
                    return Ok(users);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return NotFound();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("AddUser")]
        public IActionResult AddUser(UserLogin addUser)
        {
            try
            {
                UserEntity newUser = new UserEntity();
                newUser.UserEntityId = Guid.NewGuid();
                newUser.Email = addUser.Email;
                newUser.Password = addUser.Password;
                newUser.CreatedAt = DateTime.Now;
                _userRepository.Insert(newUser);
                _userRepository.Save();

                return Ok(newUser);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GenerateJSONWebToken(UserLogin userInfo)
        {
            try
            {
                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Issuer"];
                var expiry = DateTime.Now.AddMinutes(120);
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(issuer: issuer,
                audience: audience,
                expires: expiry,
                signingCredentials: credentials);

                var tokenHandler = new JwtSecurityTokenHandler();
                var stringToken = tokenHandler.WriteToken(token);

                return stringToken;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private UserLogin AuthenticateUser(UserLogin login)
        {
            try
            {
                //_userRepository.Insert(login.ToUserEntity(login));
                //_userRepository.Save();
                UserEntity user = _userRepository.GetByEmailAndPassword(login.Email, login.Password);

                if (user != null && !string.IsNullOrEmpty(user.Email) && user.UserEntityId != Guid.Empty)
                {
                    return new UserLogin(user.Email);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }
    }
}
