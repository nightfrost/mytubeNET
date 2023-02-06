using Microsoft.AspNetCore.Mvc;
using mytube.Models;

namespace mytube.Services
{
    public interface IUserItemsService
    {
        public Task<ActionResult<IEnumerable<UserItem>>> GetUsers();

        public Task<ActionResult<UserItem>> GetUserItem(long id);

        public Task<IActionResult> PutUserItem(long id, UserItem userItem);

        public Task<ActionResult<UserItem>> PostUserItem(UserItem userItem);

        public Task<IActionResult> DeleteUserItem(long id);

        public bool UserItemExists(long id);
    }
}
