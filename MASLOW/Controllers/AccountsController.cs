using MASLOW.Entities.Users;
using MASLOW.Models;
using MASLOW.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtHandler _jwtHandler;

        public AccountsController(UserManager<User> userManager, JwtHandler jwtHandler) 
        { 
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("Login")] 
        public async Task<IActionResult> Login(UserLoginModel userModel) 
        { 
            var user = await _userManager.FindByEmailAsync(userModel.Email); 
            
            if (user != null && await _userManager.CheckPasswordAsync(user, userModel.Password)) 
            { 
                var signingCredentials = _jwtHandler.GetSigningCredentials(); 
                var claims = _jwtHandler.GetClaims(user); 
                var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims); 
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions); 
                return Ok(token); 
            } 
            return Unauthorized("Invalid Authentication"); 
        }
    }
}
