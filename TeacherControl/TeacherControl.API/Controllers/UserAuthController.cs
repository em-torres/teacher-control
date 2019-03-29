using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TeacherControl.API.Configurations;
using TeacherControl.API.Extensors;
using TeacherControl.Common.Enums;
using TeacherControl.Common.Extensors;
using TeacherControl.Common.Helpers;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.API.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        [HttpPost, Route("authenticate")]
        //TODO: use formatFilter for the params
        public IActionResult AuthenticateUser([FromBody] UserAuthCredentialDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) && string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("The Json body is can not be Empty");

            User user = _UserRepo.Authenticate(dto.Username, dto.Password);
            if (user is null) return NotFound("User is not registered");
            //TODO: validate if the email is valid with an wrd party svc

            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(_Options.Value.SecretKey);
            var claims = new Claim[]
            {
                //TODO: assing these values to the registered user
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, string.Empty),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { token = "Bearer", tokenId = tokenHandler.WriteToken(token) });

        }

        [AllowAnonymous]
        [HttpPost, Route("register")]
        public IActionResult Register([FromBody] UserCredentialDTO dto)
        {
            return this.Created(() =>
                _UserRepo.Add(dto).Equals((int)TransactionStatus.SUCCESS)
                    ? dto.ToJson()
                    : new JObject()
                );
        }

        [HttpPut, Route("update-password")] //TODO: add the regex to validate the string
        public IActionResult UpdatePassword([FromRoute] int UserId, [FromBody] string Password)
        {
            User user = _UserRepo.Find(i => i.Email.Equals(User.Identity.Name));
            if (user is null || user.Id <= 0) return BadRequest("User is Invalid or not Found");

            Password = CredentialHelper.CreatePasswordHash(Password, user.SaltToken);

            return this.NoContent(() => _UserRepo.Update(user, new { Password }).Equals((int) TransactionStatus.SUCCESS));
        }

        [HttpPost, Route("add-roles")]
        public IActionResult AddUserRoles([FromRoute] int UserId, [FromBody] IEnumerable<string> Roles)
        {
            User user = _UserRepo.Find(i => i.Email.Equals(User.Identity.Name));
            if (user is null || user.Id <= 0) return BadRequest("User is Invalid or not Found");

            return this.NoContent(() => _UserRepo.AddUserRoles(UserId, Roles).Equals((int)TransactionStatus.SUCCESS));
        }

        [HttpPut, Route("update-roles")]
        public IActionResult UpdateUserRoles([FromRoute] int UserId, [FromBody] IEnumerable<string> Roles)
        {
            User user = _UserRepo.Find(i => i.Email.Equals(User.Identity.Name));
            if (user is null || user.Id <= 0) return BadRequest("User is Invalid or not Found");

            return this.NoContent(() => _UserRepo.UpdateUserRoles(UserId, Roles).Equals((int) TransactionStatus.SUCCESS));
        }

        [HttpDelete, Route("revoke-roles")]
        public IActionResult RevokeUserRoles([FromRoute] int UserId, [FromBody] IEnumerable<string> Roles)
        {
            User user = _UserRepo.Find(i => i.Email.Equals(User.Identity.Name));
            if (user is null || user.Id <= 0) return BadRequest("User is Invalid or not Found");

            return this.NoContent(() => _UserRepo.RemoveUserRoles(UserId, Roles).Equals((int)TransactionStatus.SUCCESS));
        }
    }
}