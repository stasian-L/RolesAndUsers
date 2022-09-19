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

namespace RolesAndUsers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public RolesController(IMapper mapper, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
            var roles = await _roleRepository.GetRoles();

            return Ok(_mapper.Map<List<RoleDto>>(roles));
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRole(Guid id)
        {
            var role = await _roleRepository.GetRole(id);

            if (role == null)
            {
                return NotFound();
            }

            var roleDto = _mapper.Map<RoleDto>(role);

            return roleDto;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(Guid id, [FromBody] RoleDto roleDto)
        {
            if (id != roleDto.Id)
            {
                return BadRequest("Role ID mismatch");
            }

            var role = _mapper.Map<Role>(roleDto);

            var roleToUpdate = await _roleRepository.GetRole(id);

            if (roleToUpdate == null)
                return NotFound($"Role with Id = {id} not found");

            await _roleRepository.UpdateRole(role);


            return NoContent();
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(RoleDto roleDto)
        {
            try
            {
                if (roleDto == null)
                {
                    return BadRequest();
                }

                var role = _mapper.Map<Role>(roleDto);
                var createdRole = await _roleRepository.AddRole(role);

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
            var role = await _roleRepository.GetRole(id);

            if (role == null)
            {
                return NotFound();
            }

            await _roleRepository.DeleterRole(id);

            return NoContent();
        }
    }
}
