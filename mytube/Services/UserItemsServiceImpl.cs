using Microsoft.AspNetCore.Mvc;
using mytube.Models;

namespace mytube.Services
{
    public class UserItemsServiceImpl : IUserItemsService
    {
        private readonly MytubeContext _context;

        public UserItemsServiceImpl(MytubeContext context)
        {
            _context = context;
        }

        public Task<IActionResult> DeleteUserItem(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<UserItem>> GetUserItem(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<UserItem>>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<UserItem>> PostUserItem(UserItem userItem)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> PutUserItem(long id, UserItem userItem)
        {
            throw new NotImplementedException();
        }

        public bool UserItemExists(long id)
        {
            throw new NotImplementedException();
        }
    }
}
