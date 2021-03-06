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
    public class SeminarsController : ControllerBase
    {
        private readonly StsDbContext _context;

        public SeminarsController(StsDbContext context)
        {
            _context = context;
        }

        // GET: api/Seminars
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seminar>>> GetAllSeminars()
        {
           

            return await _context.Seminars.Include(s => s.Bookings).ToListAsync();
        }

        // GET: api/Seminars/5
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Seminar>> GetSeminarById(int id)
        {
            var seminar = await _context.Seminars.Include(s => s.Bookings).FirstOrDefaultAsync(s => s.Id == id);

            if (seminar == null)
            {
                return NotFound();
            }

            return seminar;
        }

        //[HttpGet("{title}")]
        //public async Task<ActionResult<Seminar>> GetSeminarByTitle(string title) {
           
        //       var seminar = await _context.Seminars.Include(s => s.Title).SingleOrDefaultAsync(s => s.Title == title);

        //     if (seminar == null)
        //       {
        //           return NotFound();
        //       }

        //       return seminar;

        //}

        // PUT: api/Seminars/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeminar(int id, Seminar seminar)
        {
            var dbSeminar = await _context.Seminars.FindAsync(id);
            if (dbSeminar == null)
            {
                return NotFound();
            }
            dbSeminar.Title = seminar.Title;
            dbSeminar.Speaker = seminar.Speaker;
            dbSeminar.SeminarDateTime = seminar.SeminarDateTime;
            dbSeminar.Sits = seminar.Sits;
            dbSeminar.Description = seminar.Description;

            await _context.SaveChangesAsync();

            return Ok(dbSeminar);
        }

        // POST: api/Seminars
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Seminar>> PostSeminar(Seminar seminar)
        {
            _context.Seminars.Add(seminar);
            await _context.SaveChangesAsync();

            return Ok(_context.Seminars);
        }

        // DELETE: api/Seminars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Seminar>> DeleteSeminar(int id)
        {
            var seminar = await _context.Seminars.FindAsync(id);
            if (seminar == null)
            {
                return NotFound();
            }

            _context.Seminars.Remove(seminar);
            await _context.SaveChangesAsync();

            return Ok(_context.Seminars);
        }

        private bool SeminarExists(int id)
        {
            return _context.Seminars.Any(e => e.Id == id);
        }
    }
}
