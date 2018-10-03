using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Library3.Models;
using Library3.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Library3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public AuthController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET api/values
        [HttpPost, Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginParam user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            //if this is not awaited, code will continue executing before results are returned, resulting in unauthorized 9/10 times
            var login = await _bookRepository.GetLogin(user.UserName, user.Password);
 
            if (login != null)
            {
                //2 standard steps
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:44394", //name of the web server that issues the token
                    audience: "http://localhost:44394", //value representing valid recipients
                    claims: new List<Claim>(), //a list of user roles
                    expires: DateTime.Now.AddMinutes(60), //date and time after which the token expires
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }

    public class LoginParam
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}