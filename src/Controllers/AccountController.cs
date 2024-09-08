
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Employee_Management_System_Backend.src.DTOs;
using Employee_Management_System_Backend.src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Employee_Management_System_Backend.src.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        /// Method to register a new user

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser([FromBody] dtoNewUser user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new()
                {
                    UserName = user.UserName,
                    Email = user.Email
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded)
                {
                    return Ok("Success");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return BadRequest(ModelState);
        }

        /// Method to log in the user

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] dtoLogIn LoginData)
        {
            if (ModelState.IsValid)
            {
                AppUser? user = await _userManager.FindByNameAsync(LoginData.UserName);

                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, LoginData.Password))
                    {
                        // List to store various claims (user information) that will be added to the token
                        // new Claim("name", "value")

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        // Get the roles of the user and add them to the claims

                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                        }

                        // Create signing credentials using the secret key

                        var secretKey = _configuration["JWT:SecretKey"];
                        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? string.Empty));
                        var sc = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                        // Create the JWT token, setting its claims, issuer, audience, expiration time, and signing credentials

                        var token = new JwtSecurityToken(
                            claims: claims,
                            issuer: _configuration["JWT:Issuer"],
                            audience: _configuration["JWT:Audience"],
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: sc
                        );

                        // Package the token and its expiration time in an object to return

                        var _token = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            username = user.UserName
                        };
                        return Ok(_token);

                    }

                }
                else
                {
                    return Unauthorized("Username or password is invalid");
                }
            }
            else
            {
                ModelState.AddModelError("", "Username is invalid");
            }

            return BadRequest(ModelState);
        }
    }
}
