using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mytube.Models;

namespace mytube.Services
{
    public class UserItemsServiceImpl : ControllerBase,IUserItemsService
    {
        private readonly MytubeContext _context;

        public UserItemsServiceImpl(MytubeContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<UserItem>> CreateUser(UserItem userItem)
        {
            var checkForExistingUsername = _context.Users.FirstOrDefault(u => u.username == userItem.username);
            var checkForExistingEmail = _context.Users.FirstOrDefault(k => k.email == userItem.email);

            if (checkForExistingEmail != null)
            {
                return BadRequest(new UserItem());
            }

            if (checkForExistingUsername != null)
            {
                return BadRequest(new UserItem());
            }

            var user = await _context.Users.AddAsync(userItem);
            await _context.SaveChangesAsync();

            return Ok(user.Entity);
        }

        public async Task<ActionResult<string>> DeleteUserItem(long id)
        {
            if (_context.Users.Any(x => x.ID == id))
            {
                var userItem = await _context.Users.FindAsync(id);
                try
                {
                    _context.Users.Remove(userItem);
                    _context.SaveChanges();
                    return Ok(String.Format("Deleted user with id: {0}", userItem.ID));
                }
                catch (Exception e)
                {
                    Console.WriteLine(String.Format("Fatal error: {0}", e.Message));
                    Console.WriteLine(e.StackTrace);
                    return BadRequest(String.Format("Failed to delete user with id: {0}", id));
                }
            }
            return NotFound(String.Format("No user with ID: {0}", id));
        }

        public async Task<ActionResult<UserItem>> GetUserItem(long id)
        {
            if (_context.Users.Any(x => x.ID == id))
            {
                return Ok(await _context.Users.FindAsync(id));
            } else
            {
                return NotFound(new UserItem());
            }
        }

        public async Task<ActionResult<IEnumerable<UserItem>>> GetUsers()
        {
            var list = await _context.Users.ToListAsync();

            if (list.Count > 0)
            {
                return Ok(list);
            } else
            {
                return NotFound(list);
            }
        }

        public async Task<ActionResult<UserItem>> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.email== email);

            if (user == null)
            {
                UserItem tempUserItem = new UserItem();
                tempUserItem.email = email;
                tempUserItem.password = password;
                tempUserItem.username = email;
                tempUserItem.firstName = "Default";
                tempUserItem.lastName = "Default";
                tempUserItem.createdAt = DateTime.Now;
                tempUserItem.enabled= true;
                await PostUserItem(tempUserItem);
                await _context.SaveChangesAsync();
                return Ok(tempUserItem);
            } else if (user.password == password) {
                return Ok(user);
            } else
            {
                return BadRequest(new UserItem());
            }
        }

        public async Task<ActionResult<UserItem>> PostUserItem(UserItem userItem)
        {
            var user = _context.Users.Add(userItem);
            await _context.SaveChangesAsync();

            return Ok(user.Entity);
        }

        public async Task<ActionResult<UserItem>> PutUserItem(long id, UserItem userItem)
        {
            var userItemFromDB = await _context.Users.FindAsync(id);

            if (userItemFromDB== null)
            {
                return NotFound(userItemFromDB);
            }

            userItemFromDB = userItem;

            try
            {
                _context.Entry(userItemFromDB).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(userItemFromDB);
            }
            catch (Exception e)
            {
                //return null reference and handle in controller.
                UserItem tempItem = null;
                Console.WriteLine("Updating video with ID: {0} failed. See stack.");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return BadRequest(tempItem);
            }
        }

        public bool UserItemExists(long id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
