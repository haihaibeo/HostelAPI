using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HostelWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/property")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IDbRepo repo;

        public PropertyController(IDbRepo repo)
        {
            this.repo = repo;
        }

        // GET: api/<PropertyController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await repo.Properties.GetAllAsync();
            return Ok(res);
        }

        // GET api/<PropertyController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var res = await repo.Properties.GetByIdAsync(id);
            if (res != null) return Ok(res);
            else return BadRequest();
        }

        // POST api/<PropertyController>
        [HttpPost]
        [Authorize(Roles = AppRoles.Owner)]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PropertyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PropertyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
