using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.DataAccess;
using Api.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }



        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersProfile>>> GetUsers()
        {
            var Users = await _context.Users.ToListAsync();
            List<UsersProfile> list = new List<UsersProfile>();

            foreach(var item in Users)
            {
                list.Add(new UsersProfile()
                {
                    Id = item.User_ID,
                    Username = item.Username,
                    isOnline = item.IsOnline == "true" ? true : false
                });
            }

            return list;
            //return await _context.Users.ToListAsync();
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> LoginAsync([FromBody] UsernamePasswordModel users)
        {
            if (UsernameExists(users.Username))
            {
                var user = _context.Users.FirstOrDefault(x => x.Username == users.Username);
                if (user.Password == users.Password)
                {
                    await UpdateUserIsOnline(user.User_ID, "true");
                    return StatusCode(202);
                }
            }

            return BadRequest();
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.User_ID)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers([FromBody] UsernamePasswordModel users)
        {
            if (!UsernameExists(users.Username))
            {

                _context.Users.Add(new Users()
                {
                    Username = users.Username,
                    Password = users.Password,
                    IsOnline = "false"
                }) ;
                await _context.SaveChangesAsync();

                return StatusCode(202);
            }
            else
                return StatusCode(204);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private async Task UpdateUserIsOnline(int id, string isOnline)
        {
            var user = _context.Users.First(x => x.User_ID == id);

            if(user != null)
            {
                user.IsOnline = isOnline;
                await _context.SaveChangesAsync();
            }
        }
        

        private bool UsernameExists(string Username)
        {
            return _context.Users.Any(x => x.Username == Username);
        }
        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.User_ID == id);
        }
    }
}
