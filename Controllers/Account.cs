using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RadMock2021.DataModel;
using Mock2021DataLayer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Mock2021DataLayer.View_Model;

namespace RadMock2021.Controllers
{
    [Route("[Accounts]")]
    [ApiController]
    public class Account : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public Account(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost("/Token")]

        // make user name and password is has been set up in the View_Model aka login view in data layer
        public async Task<IActionResult> Token([FromBody] View_Model model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null)
                {

                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.Succeeded)
                    {
                        // create roles
                        var _options = new IdentityOptions();

                        // create the token
                        var claims = new List<Claim>
                        {
                            new Claim(
                                JwtRegisteredClaimNames.Sub,
                                user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
                            new Claim(_options.ClaimsIdentity.UserIdClaimType,user.Id.ToString()),
                            new Claim(_options.ClaimsIdentity.UserIdClaimType,user.UserName),
                        };

                        //Role Claims
                        var userClaims = await _userManager.GetClaimsAsync(user);
                        var userRoles = await _userManager.GetRolesAsync(user);
                        claims.AddRange(userClaims);
                        foreach (var userRole in userRoles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, userRole));
                            var role = await _roleManager.FindByNameAsync(userRole);
                            if (role != null)
                            {
                                var roleClaims = await _roleManager.GetClaimsAsync(role);
                                foreach (Claim roleclaim in roleClaims)
                                {
                                    claims.Add(roleclaim);

                                }
                            }
                        }

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audiences"],
                            claims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: creds);


                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            userID = user.Id
                        };

                        return Created("", JsonConvert.SerializeObject(results));
                    }
                }
            }
            return BadRequest();
        }
    }
}
