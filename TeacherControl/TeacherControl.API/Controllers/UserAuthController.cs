using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using TeacherControl.API.Extensors;
using TeacherControl.Common.Enums;
using TeacherControl.Common.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.API.Controllers
{
    [Route("api/users")]
    public class UserAuthController : Controller
    {
        protected IUserRepository _UserRepo;

        public UserAuthController(IUserRepository userRepo)
        {
            _UserRepo = userRepo;
        }

        [AllowAnonymous]
        [HttpPost, Route("authenticate")]
        public IActionResult AuthenticateUser([FromBody] UserCredentialsDTO dto)
        {
            //http://jasonwatmore.com/post/2018/06/26/aspnet-core-21-simple-api-for-authentication-registration-and-user-management
            if (dto is null) return BadRequest("The Json body is can not be Empty");

            User user = _UserRepo.Authenticate(dto.Username, dto.Password);
            if (user is null) return NotFound("User is not registered");

            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes("secret from the settings");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim("someusername", "1234_ID")}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { tokenId = tokenHandler.WriteToken(token) });

        }

        [AllowAnonymous]
        [HttpPost, Route("register")]
        public IActionResult Register([FromBody] UserCredentialsDTO dto)
        {
            return this.Created(() => 
                _UserRepo.Add(dto).Equals((int) TransactionStatus.SUCCESS)
                        ? dto.ToJson()
                    : new JObject()
                );
        }
    }
}