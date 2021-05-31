using HostelWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HostelWebAPI.Services
{
    public interface ITokenService
    {
        string TokenGenerator(User user, IEnumerable<string> roles);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration config;

        public TokenService(IConfiguration config)
        {
            this.config = config;
        }

        public string TokenGenerator(User user, IEnumerable<string> roles)
        {
            var roleclaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToArray();
            var roleclaimsByName = roles.Select(r => new Claim("roles", r)).ToArray();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("email", user.Email),
                new Claim("name", user.Name),
                new Claim("userId", user.Id),
            };
            claims.AddRange(roleclaims);
            claims.AddRange(roleclaimsByName);

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(config["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                issuer: config["JwtIssuer"],
                audience: config["JwtIssuer"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public interface IUserService
    {
        Task<User> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal);
        Task<User> GetUserByIdAsync(string userId);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;

        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public Task<User> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            return userManager.FindByEmailAsync(email);
        }

        public Task<User> GetUserByIdAsync(string userId)
        {
            return userManager.FindByIdAsync(userId);
        }
    }
}
