using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using TeacherControl.API.Configurations;
using TeacherControl.API.Extensors;
using TeacherControl.Common.Enums;
using TeacherControl.Common.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.API.Controllers
{
    [AllowAnonymous]
    [Route("api/users")]
    public class UserAuthController : Controller
    {
        protected readonly IUserRepository _UserRepo;
        protected readonly IOptions<AppSettings> _Options;

        public UserAuthController(IUserRepository userRepo, IOptions<AppSettings> options)
        {
            _UserRepo = userRepo;
            _Options = options;
        }

        [HttpPost, Route("authenticate")]
        public IActionResult AuthenticateUser([FromBody] UserCredentialsDTO dto)
        {
            if (dto is null) return BadRequest("The Json body is can not be Empty");

            User user = _UserRepo.Authenticate(dto.Username, dto.Password);
            if (user is null) return NotFound("User is not registered");

            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(_Options.Value.SecretKey);
            var claims = new Claim[]
            {
                new Claim("Username", user.Username),
                new Claim("UserId", user.Id.ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { token = "Bearer", tokenId = tokenHandler.WriteToken(token) });

        }

        [HttpPost, Route("register")]
        public IActionResult Register([FromBody] UserCredentialsDTO dto)
        {
            return this.Created(() =>
                _UserRepo.Add(dto).Equals((int)TransactionStatus.SUCCESS)
                    ? dto.ToJson()
                    : new JObject()
                );
        }
    }
}