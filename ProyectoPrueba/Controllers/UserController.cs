using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProyectoPrueba.DTOs;
using ProyectoPrueba.Interfaces;
using ProyectoPrueba.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectoPrueba.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController : ControllerBase
    {
        IConfiguration _configuration;
        IUserRepository _userRepository;
        public UserController(IConfiguration configuration, IUserRepository userRespository)
        {
            this._configuration = configuration;
            this._userRepository = userRespository;
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            try
            {
                if( user == null) throw new Exception("Datos inválidos");

                this._userRepository.insertUser(user);

                return StatusCode(StatusCodes.Status200OK, new { user = user });

            } catch (Exception e )
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new {message = e.Message});
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDTO login)
        {
            try
            {
                int userID = this._userRepository.ValidateUser(login);

                if ( userID > 0 )
                {
                    LoginResponseDTO loginResponse = new LoginResponseDTO();

                    string slug = this._userRepository.getSlugById(userID);

                    var configSection = this._configuration.GetSection("Jwt").Get<JwtDTO>();

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, configSection.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("id", userID.ToString()),
                        new Claim("email", login.Email),
                        new Claim("slug", slug)
                    };

                    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configSection.Key));
                    SigningCredentials signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    SecurityToken token = new JwtSecurityToken(configSection.Issuer, configSection.Audience, claims, expires: DateTime.Now.AddHours(5), signingCredentials: signInCred);

                    loginResponse.accessToken = (new JwtSecurityTokenHandler()).WriteToken(token);
                    loginResponse.tenants.Add(new LoginResponseDTO.Slug { slugTenant = slug });

                    return StatusCode(StatusCodes.Status200OK, new { statusText = "POST Request successful", data = loginResponse });
                }

                return StatusCode(StatusCodes.Status401Unauthorized);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }


        }

    }
}
