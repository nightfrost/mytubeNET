using Microsoft.AspNetCore.Mvc;
using mytube.Models;

namespace mytube.Services
{
    public interface IUserItemsService
    {
        public Task<ActionResult<IEnumerable<UserItem>>> GetUsers();

        public Task<ActionResult<UserItem>> GetUserItem(long id);

        public Task<ActionResult<UserItem>> PutUserItem(long id, UserItem userItem);

        public Task<ActionResult<UserItem>> PostUserItem(UserItem userItem);

        public Task<ActionResult<String>> DeleteUserItem(long id);

        public bool UserItemExists(long id);

        public Task<ActionResult<UserItem>> Login(string email, string password);

        public Task<ActionResult<UserItem>> CreateUser(UserItem userItem);
    }
}
