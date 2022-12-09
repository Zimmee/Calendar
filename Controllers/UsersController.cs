using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Calendar.Models;
using AutoMapper;
using Calendar.Dto;

namespace Calendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly KpzCalendarContext _context;
        private readonly IMapper _mapper;


        public UsersController(KpzCalendarContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.Users.Include(p=>p.Calendars).ThenInclude(e=>e.Events).FirstOrDefaultAsync(i=>i.Id==id);

            if (user == null)
            {
                return NotFound();
            }

            var result = new UserDto() { Name = user.Name, calendar = user.Calendars.Select((s) => new GetCalendar() { UserId = s.UserId, Id = s.Id, Events = s.Events.Select(a => _mapper.Map<EventDto>(a)).ToList() }).FirstOrDefault() };



            return result;
        }

        // GET: api/Users/mike
        [HttpGet("GetUserByUsername/{username}")]
        public async Task<ActionResult<UserDto>> GetUserByUsername(String username)
        {
            var user =  _context.Users.Include(p => p.Calendars).ThenInclude(e => e.Events).Where((u) => u.Name == username).FirstOrDefault();


            if (user == null)
            {
                return NotFound();
            }


            var result = new UserDto() { Name = user.Name, calendar = user.Calendars.Select((s) => new GetCalendar() { UserId = s.UserId,Id=s.Id, Events = s.Events.Select(a => _mapper.Map<EventDto>(a)).ToList() }).FirstOrDefault() };



            return result;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
