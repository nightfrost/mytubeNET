using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mytube.Models;
using mytube.Services;

namespace mytube.Controllers
{
    //[EnableCors("AllowSpecificOrigin")] - Not sure if this is possible
    [EnableCors]
    [Route("api/user")]
    [ApiController]
    public class UserItemsController : ControllerBase
    {
        private readonly IUserItemsService _service;

        public UserItemsController(IUserItemsService service)
        {
            _service = service;
        }

        // GET: api/UserItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserItem>>> GetUsers()
        {
            return await _service.GetUsers();
        }

        [HttpPost("/login")]
        public async Task<ActionResult<UserItem>> Login(string email, string password)
        {
            return await _service.Login(email, password);
        }

        // GET: api/UserItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserItem>> GetUserItem(long id)
        {
            return await _service.GetUserItem(id);
        }

        // PUT: api/UserItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<UserItem>> PutUserItem(long id, UserItem userItem)
        {
            return await _service.PutUserItem(id, userItem);
        }

        // POST: api/UserItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserItem>> PostUserItem(UserItem userItem)
        {
            return await _service.PostUserItem(userItem);
        }

        // DELETE: api/UserItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteUserItem(long id)
        {
            return await _service.DeleteUserItem(id);
        }

        [HttpPost("userItems")]
        public async Task<ActionResult<UserItem>> CreateUser(UserItem userItem)
        {
            var result = await _service.CreateUser(userItem);

            if (result.Value == null) {
                return BadRequest(result.Value);
            } else
            {
                return CreatedAtAction("GetUserItem", new { id = result.Value.ID}, result.Value);
            }
        }
    }
}
