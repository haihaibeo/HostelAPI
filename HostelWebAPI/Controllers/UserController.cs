﻿using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
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
        private readonly IDbRepo repo;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IDbRepo repo)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.repo = repo;
        }
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenService tokenService;

        /// <summary>
        /// register user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterRequest request)
        {
            User user = new User()
            {
                Name = request.Name == null ? request.Email : request.Name,
                Email = request.Email,
                UserName = request.Email
            };

            var res = await userManager.CreateAsync(user, request.Password);
            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                await signInManager.SignInAsync(user, false);

                var roles = await userManager.GetRolesAsync(user);
                var token = tokenService.TokenGenerator(user, roles);
                return Ok(new TokenResponse(user.Id, token, user.Name, user.Email));
            }
            foreach (var error in res.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(new MessageResponse("", ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))));
        }

        [HttpGet("{ownerId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOwnerBasicInfo([FromRoute] string ownerId)
        {
            try
            {
                var owner = await userManager.FindByIdAsync(ownerId);
                if (owner == null) return NotFound(new { message = "User not found" });
                return Ok(owner);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [HttpGet("propId/{propId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOwnerBasicInfoByPropId([FromRoute] string propId)
        {
            var props = await repo.Properties.GetByIdAsync(propId);
            var owner = await userManager.FindByIdAsync(props.OwnerId);

            return Ok(new UserInfoResponse(owner));
        }

        [HttpPost]
        [Authorize]
        [Route("register-host")]
        public async Task<IActionResult> RegisterHostAsync()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);

            if (user != null)
            {
                userManager.AddToRoleAsync(user, AppRoles.Owner).Wait();

                var roles = await userManager.GetRolesAsync(user);
                var token = tokenService.TokenGenerator(user, roles);
                return Ok(new TokenResponse(user.Id, token, user.Name, user.Email));
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = AppRoles.Owner)]
        [Route("unregister-host")]
        public async Task<IActionResult> RemoveHostRole()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var res = await userManager.RemoveFromRoleAsync(user, AppRoles.Owner);
                if (res.Succeeded) return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest payload)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(payload.Email, payload.Password, true, false);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(payload.Email);
                    var roles = await userManager.GetRolesAsync(user);
                    var token = tokenService.TokenGenerator(user, roles);
                    return Ok(new TokenResponse(user.Id, token, user.Name, user.Email));
                }
                else return NotFound(new { message = "Login or password did not match" });
            }
            return BadRequest("Something's wrong");
        }

        [HttpGet]
        [Authorize]
        [Route("validate-token")]
        public async Task<IActionResult> ValidateToken()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);
            var roles = await userManager.GetRolesAsync(user);

            var token = tokenService.TokenGenerator(user, roles);
            return Ok(token);
        }
    }
}
