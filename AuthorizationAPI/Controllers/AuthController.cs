using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizationAPI.Model;
using AuthorizationAPI.Provider;
using AuthorizationAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthController));
        private readonly IPensionRepo repo;

        public AuthController(IConfiguration config,IPensionRepo _repo)
        {
            _config = config;
            repo = _repo;
        }

        /// <summary>
        /// Post method for Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Login([FromBody] PensionCredentials login)
        {
            _log4net.Info("Login initiated!");
            IActionResult response = Unauthorized();
            //login.FullName = "user1";
            var user = AuthenticateUser(login);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        /// <summary>
        /// Generating the token
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [NonAction]
        public string GenerateJSONWebToken(PensionCredentials userInfo)
        {
            _log4net.Info("Token Is Generated!");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);



            return new JwtSecurityTokenHandler().WriteToken(token);


        }
        /// <summary>
        /// Finding User with matching credentials
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [NonAction]
        public PensionCredentials AuthenticateUser(PensionCredentials login)
        {
            _log4net.Info("Validating the User!");

            //Validate the User Credentials 
            PensionCredentials usr = repo.GetPensionerCred(login);


            return usr;
        }
    }
}
