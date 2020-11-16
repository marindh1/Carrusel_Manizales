using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS_Facturacion.Models;

namespace MS_Facturacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoFacturasController : ControllerBase
    {
        private readonly MS_FacturacionContext _context;

        public EstadoFacturasController(MS_FacturacionContext context)
        {
            _context = context;
        }

        // GET: api/EstadoFacturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoFactura>>> GetEstadoFactura()
        {
            return await _context.EstadoFactura.ToListAsync();
        }

        // GET: api/EstadoFacturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoFactura>> GetEstadoFactura(int id)
        {
            var estadoFactura = await _context.EstadoFactura.FindAsync(id);

            if (estadoFactura == null)
            {
                return NotFound();
            }

            return estadoFactura;
        }

        // PUT: api/EstadoFacturas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoFactura(int id, EstadoFactura estadoFactura)
        {
            if (id != estadoFactura.Id)
            {
                return BadRequest();
            }

            _context.Entry(estadoFactura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoFacturaExists(id))
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

        // POST: api/EstadoFacturas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EstadoFactura>> PostEstadoFactura(EstadoFactura estadoFactura)
        {
            _context.EstadoFactura.Add(estadoFactura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadoFactura", new { id = estadoFactura.Id }, estadoFactura);
        }

        // DELETE: api/EstadoFacturas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EstadoFactura>> DeleteEstadoFactura(int id)
        {
            var estadoFactura = await _context.EstadoFactura.FindAsync(id);
            if (estadoFactura == null)
            {
                return NotFound();
            }

            _context.EstadoFactura.Remove(estadoFactura);
            await _context.SaveChangesAsync();

            return estadoFactura;
        }

        private bool EstadoFacturaExists(int id)
        {
            return _context.EstadoFactura.Any(e => e.Id == id);
        }
    }
}
