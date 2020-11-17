using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS_PQR.Models;

namespace MS_PQR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PqrsController : ControllerBase
    {
        private readonly MS_PQRContext _context;

        public PqrsController(MS_PQRContext context)
        {
            _context = context;
        }

        // GET: api/Pqrs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pqr>>> GetPqr()
        {
            return await _context.Pqr.ToListAsync();
        }

        // GET: api/Pqrs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pqr>> GetPqr(int id)
        {
            var pqr = await _context.Pqr.FindAsync(id);

            if (pqr == null)
            {
                return NotFound();
            }

            return pqr;
        }

        // PUT: api/Pqrs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPqr(int id, Pqr pqr)
        {
            if (id != pqr.Id)
            {
                return BadRequest();
            }

            _context.Entry(pqr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PqrExists(id))
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

        // POST: api/Pqrs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pqr>> PostPqr(Pqr pqr)
        {
            _context.Pqr.Add(pqr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPqr", new { id = pqr.Id }, pqr);
        }

        // DELETE: api/Pqrs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pqr>> DeletePqr(int id)
        {
            var pqr = await _context.Pqr.FindAsync(id);
            if (pqr == null)
            {
                return NotFound();
            }

            _context.Pqr.Remove(pqr);
            await _context.SaveChangesAsync();

            return pqr;
        }

        private bool PqrExists(int id)
        {
            return _context.Pqr.Any(e => e.Id == id);
        }
    }
}
