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
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            return await _userService.GetUser(id);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, [FromBody] UserDto userDto)
        {
            await _userService.UpdateUser(id,userDto);

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDto userDto)
        {
            try
            {
                var createdUser = await _userService.AddUser(userDto);

                return CreatedAtAction("GetUser", new { id = createdUser.Id }, createdUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new user record");
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        { 
            await _userService.DeleteUser(id);

            return NoContent();
        }
    }
}
