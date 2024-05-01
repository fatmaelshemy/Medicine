using Medicine.Dtos;
using Medicine.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Medicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost("register")]//api/controller/register
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser _newUser = new ApplicationUser()
                {
                    Email = registerDto.Email,
                    UserName = registerDto.UserName,
                    PasswordHash = registerDto.Password

                };
                //create account in db
                IdentityResult result = await _userManager.CreateAsync(_newUser, registerDto.Password);

                if (!result.Succeeded)
                {
                    return new BadRequestObjectResult(result.Errors);
                }
                var Role = registerDto.Role;
                var assignRoleResult = await _userManager.AddToRoleAsync(_newUser, Role);

                if (!assignRoleResult.Succeeded)
                {
                    return new BadRequestObjectResult(assignRoleResult.Errors);
                }

                return Ok("Register Success");
            }
            return BadRequest();

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            ApplicationUser? userfromdb = await _userManager.FindByNameAsync(loginDto.UserName);


            if (userfromdb is not null)
            {
                bool found = await _userManager.CheckPasswordAsync(userfromdb, loginDto.Password);

                if (found)
                {
                    List<Claim> tokenClaims = new List<Claim>();
                    tokenClaims.Add(new Claim(ClaimTypes.Name, userfromdb.UserName));
                    tokenClaims.Add(new Claim(ClaimTypes.NameIdentifier, userfromdb.Id));
                    tokenClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    var roles = await _userManager.GetRolesAsync(userfromdb);
                    foreach (var role in roles)
                    {
                        tokenClaims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fatmafatmafatmafatmafatmafatmafatmafatmafatmafatma"));
                    SigningCredentials signingCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
                    //create token
                    JwtSecurityToken token = new JwtSecurityToken(
                        issuer: "http://localhost:5221/",
                        audience: "http://localhost:4200",
                        claims: tokenClaims,
                    expires: DateTime.Now.AddHours(1),
                        signingCredentials: signingCredentials);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expired = token.ValidTo
                    });
                }

            }
            return Unauthorized("Invalid Account");
        }
    }
}
