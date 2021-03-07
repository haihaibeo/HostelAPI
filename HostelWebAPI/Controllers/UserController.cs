﻿using HostelWebAPI.Models;
using HostelWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HostelWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenService tokenService;

        /// <summary>
        /// register user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("registerUser")]
        public async Task<IActionResult> RegisterUserAsync()
        {
            User user = new User()
            {
                Name = "User 2",
                Email = "user2@mail.com",
                UserName = "user2@mail.com"
            };

            var res = await userManager.CreateAsync(user, "password");
            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                await signInManager.SignInAsync(user, true);
                return Ok(new MessageResponse($"User {user.Name} registered!", null));
            }
            foreach (var error in res.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Ok(new MessageResponse("", ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))));
        }

        [HttpPost]
        [Authorize]
        [Route("registerOwner")]
        public async Task<IActionResult> RegisterOwnerAsync()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);

            if(user != null)
            {
                userManager.AddToRoleAsync(user, "Owner").Wait();
            }
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = AppRoles.Owner)]
        [Route("test-authen")]
        public async Task<object> Test()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);
            var role = await userManager.GetRolesAsync(user);
            return user.UserName;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest payload)
        {
            if (ModelState.IsValid) {
                var result = await signInManager.PasswordSignInAsync(payload.Email, payload.Password, true, false);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(payload.Email);
                    var roles = await userManager.GetRolesAsync(user);
                    var token = tokenService.TokenGenerator(user, roles);
                    return Ok(new TokenResponse(user.Id, token));
                }
            }
            return BadRequest("Something's wrong");
        }
    }
}