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
    public class CalendarsController : ControllerBase
    {
        private readonly KpzCalendarContext _context;
        private readonly IMapper _mapper;


        public CalendarsController(KpzCalendarContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Calendars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calendar.Models.Calendar>>> GetCalendars()
        {
            return await _context.Calendars.ToListAsync();
        }

        // GET: api/Calendars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calendar.Models.Calendar>> GetCalendar(int id)
        {
            var calendar = await _context.Calendars.FindAsync(id);

            if (calendar == null)
            {
                return NotFound();
            }

            return calendar;
        }

        // PUT: api/Calendars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalendar(int id, CalendarDto calendarDto)
        {
            var calendar = _mapper.Map<Calendar.Models.Calendar>(calendarDto);
            calendar.Id = id;

            _context.Entry(calendar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarExists(id))
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

        // POST: api/Calendars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Calendar.Models.Calendar>> PostCalendar(CalendarDto calendarDto)
        {
            var calendar = _mapper.Map<Calendar.Models.Calendar>(calendarDto);
            _context.Calendars.Add(calendar);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CalendarExists(calendar.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCalendar", new { id = calendar.Id }, calendar);
        }

        // DELETE: api/Calendars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendar(int id)
        {
            var calendar = await _context.Calendars.FindAsync(id);
            if (calendar == null)
            {
                return NotFound();
            }

            _context.Calendars.Remove(calendar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalendarExists(int id)
        {
            return _context.Calendars.Any(e => e.Id == id);
        }
    }
}
