using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RolesAndUsers.Data;
using RolesAndUsers.Dtos;
using RolesAndUsers.Models;
using RolesAndUsers.Repositories;
using RolesAndUsers.Services;

namespace RolesAndUsers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
            return Ok(await _roleService.GetRoles());
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRole(Guid id)
        {
            return await _roleService.GetRole(id);
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(Guid id, [FromBody] RoleDto roleDto)
        {
            await _roleService.UpdateRole(id, roleDto);

            return NoContent();
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(RoleDto roleDto)
        {
            try
            { 
                var createdRole = await _roleService.AddRole(roleDto);

                return CreatedAtAction("GetRole", new { id = createdRole.Id }, createdRole);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new role record");
            }
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            await _roleService.DeleterRole(id);

            return NoContent();
        }
    }
}
