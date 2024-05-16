using Microsoft.AspNetCore.Mvc;
using Usersitos.Data;
using Usersitos.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Usersitos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UsersDataBaseContext _context;

        public UsersController(UsersDataBaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetNotes()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUsers([Bind("Username,Password,Email,PhoneNumber")] User data)
        {
            User user = new User
            {
                Username = data.Username,
                Password = data.Password,
                CreationDate = DateTime.Now,
                Status = "Activo",
                Email = data.Email,
                PhoneNumber = data.PhoneNumber
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> PutUser(int id, User user)
        {
            try
            {
                var userSearch = await _context.Users.FindAsync(id);
                if (userSearch == null)
                {
                    return NotFound();
                }
                userSearch.Username = user.Username;
                userSearch.Password = user.Password;
                if (user.Status != null)
                {
                    userSearch.Status = user.Status;
                }
                userSearch.Email = user.Email;
                userSearch.PhoneNumber = user.PhoneNumber;

                _context.Entry(userSearch).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return userSearch;
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

    }
}
