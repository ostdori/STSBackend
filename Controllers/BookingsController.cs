using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendSignToSem.Context;
using BackendSignToSem.Models;
using Microsoft.AspNetCore.Cors;

namespace BackendSignToSem.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly StsDbContext _context;

        public BookingsController(StsDbContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
             
            return await _context.Bookings.Include(b => b.Seminar).ToListAsync();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return booking;
            //var booking = await _context.Bookings.Include(b => b.Seminar).FirstOrDefaultAsync(b => b.Id == id);

            //if (booking == null)
            //{
            //    return NotFound();
            //}

            //return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {

            var dbBooking = await _context.Bookings.FindAsync(id);
            if (dbBooking == null)
            {
                return NotFound();
            }
            dbBooking.FirstName = booking.FirstName;
            dbBooking.LastName = booking.LastName;
            dbBooking.Email = booking.Email;
            dbBooking.Mobile = booking.Mobile;
            dbBooking.Message = booking.Message;
            dbBooking.SeminarId = booking.SeminarId;

            await _context.SaveChangesAsync();

            return Ok(dbBooking);
        }

        // POST: api/Bookings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            //_context.Bookings.Add(booking);
            //await _context.SaveChangesAsync();

            //return Ok(_context.Bookings);
            var seminar = _context.Seminars.Where(s => s.Id == booking.SeminarId).FirstOrDefault();
            var _booking = new Booking
            {
               
                FirstName = booking.FirstName,
                LastName = booking.LastName,
                Email = booking.Email,
                Message = booking.Message,
                Mobile = booking.Mobile,
                Seminar = seminar
            };

            _context.Bookings.Add(_booking);
             await _context.SaveChangesAsync();

             return Ok(_booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Booking>> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
