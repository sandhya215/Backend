using KanbanBoard.DTO;
using KanbanBoard.Models;
using KanbanBoard.Contexts;
using KanbanBoard.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace KanbanBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // IRepository<Login> repo = new LoginRepository();
        private IUsersRepository userRepository = null;
        private ITeamMembersRepository TeamMembersRepository = null;
    

        public LoginController(IUsersRepository userRepository, ITeamMembersRepository TeamMembersRepository)
        {
            this.userRepository = userRepository;
            this.TeamMembersRepository=TeamMembersRepository;
    
        }
                [Route("UserLogin")]
        [HttpPost]
        public IActionResult UserLogin(Login login)
        {
            LoggedUserModel model = new LoggedUserModel();
            //Validating Login credentials
            Users users = userRepository.validateUsers(login);
            if (users != null)
            {
                string token = getTokenForUsers(users);
                model = new LoggedUserModel() { EmailID = users.userEmail, Token = token, Role = users.Role };

            }
            else
            {
                return BadRequest("Invalid Credentials");
            }

            return Ok(model);
        }


        //Logging in as TeamMembers
        [Route("TeamMembersLogin")]
        [HttpPost]
        public IActionResult TeamMembersLogin(Login login)
        {
            LoggedUserModel model = new LoggedUserModel();
            //Validating Login credentials
            TeamMembers teammembers = TeamMembersRepository.validateTeamMembers(login);
            if (teammembers != null)
            {
                string token = getTokenForTeamMembers(teammembers);
                model = new LoggedUserModel() { EmailID = teammembers.teammemberEmail, Token = token, Role = teammembers.Role };
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }

            return Ok(model);
        }



        //Getting token for Authorization for User
        private string getTokenForUsers(Users person)
        {
            var _config = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json").Build();
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(2);
            var securityKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials
        (securityKey, SecurityAlgorithms.HmacSha256);

            //    var token = new JwtSecurityToken(issuer: issuer,
            //audience: audience,

            //expires: DateTime.Now.AddMinutes(120),
            //signingCredentials: credentials);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                   {
                    new Claim(ClaimTypes.NameIdentifier, person.user_id.ToString()),
                    new Claim(ClaimTypes.Name, person.userEmail.ToString()),
                    new Claim(ClaimTypes.Role, person.Role)
                   }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

        //Getting token for Authorization for TeamMembers
        private string getTokenForTeamMembers(TeamMembers person)
        {
            var _config = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json").Build();
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(2);
            var securityKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials
        (securityKey, SecurityAlgorithms.HmacSha256);

            //    var token = new JwtSecurityToken(issuer: issuer,
            //audience: audience,

            //expires: DateTime.Now.AddMinutes(120),
            //signingCredentials: credentials);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                   {
                    new Claim(ClaimTypes.NameIdentifier, person.teammember_id.ToString()),
                    new Claim(ClaimTypes.Name, person.teammemberName.ToString()),
                    new Claim(ClaimTypes.Role, person.Role)
                   }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
   }
}
