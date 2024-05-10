﻿using Medicine.Dtos;
using Medicine.Models;
using Medicine.Repository;
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
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost("register")]//api/controller/register
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {


            if (registerDto.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(registerDto.ImageFile);
                if (fileResult.Item1 == 1)
                {
                    registerDto.ImageUrl = fileResult.Item2; // getting name of image
                }
            }
            if (ModelState.IsValid)
            {
                ApplicationUser _newUser = new ApplicationUser()
                {
                    Email = registerDto.Email,
                    UserName = registerDto.UserName,
                    PasswordHash = registerDto.Password,
                    //ImageUrl=registerDto.ImageUrl,
                    Gender = registerDto.Gender,


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
                        expired = token.ValidTo,
                        userId = userfromdb.Id,
                    });
                }

            }
            return Unauthorized("Invalid Account");
        }

        [HttpGet("getuser/{id}")]
        public async Task<ActionResult<UserData>> GetUser(string id)
        {
            ApplicationUser? userfromdb = await _userManager.FindByIdAsync(id);

            if (userfromdb != null)
            {
                return new UserData()
                {
                    UserName = userfromdb.UserName,
                    Id = userfromdb.Id,
                    ImageUrl = userfromdb.ImageUrl
                };
            }
            return NotFound("User Not Found");

        }
    }


}
