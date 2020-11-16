using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS_Seguimiento_Pedidos.Models;
using Newtonsoft.Json;

namespace MS_Seguimiento_Pedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguimientoController : ControllerBase
    {
        private readonly MS_SeguimientoContext _context;

        public SeguimientoController(MS_SeguimientoContext context)
        {
            _context = context;
        }

        // GET: api/Seguimiento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seguimiento>>> GetSeguimiento()
        {
            return await _context.Seguimiento.ToListAsync();
        }

        // GET: api/Seguimiento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seguimiento>> GetSeguimiento(int id)
        {
            var seguimiento = await _context.Seguimiento.FindAsync(id);

            if (seguimiento == null)
            {
                return NotFound();
            }

            return seguimiento;
        }

        // GET: api/Seguimiento/pedido/5
        [HttpGet("pedido/{id}")]
        public IQueryable SeguimientoPedido(int id)
        {
            /*var seguimiento = (from seg in _context.Seguimiento
                               where seg.NroPedido == id
                               orderby seg.Fecha ascending
                               select new
                               {
                                   seg.NroPedido,
                                   seg.Fecha,
                                   Estado = (from est in _context.Estado
                                             where est.Id == seg.EstadoId
                                             select new
                                             {
                                                 est.Id,
                                                 est.Descripcion
                                             })
                               }

                );
            */
            var seguimiento = _context.Seguimiento.Where(r => r.NroPedido == id).OrderBy(r=> r.Fecha);
            if (seguimiento == null)
            {
                return null;
            }

            //var result = JsonConvert.SerializeObject(seguimiento);
            return seguimiento;
        }

        // PUT: api/Seguimiento/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeguimiento(int id, Seguimiento seguimiento)
        {
            if (id != seguimiento.Id)
            {
                return BadRequest();
            }

            _context.Entry(seguimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeguimientoExists(id))
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

        // POST: api/Seguimiento
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Seguimiento>> PostSeguimiento(Seguimiento seguimiento)
        {
            _context.Seguimiento.Add(seguimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeguimiento", new { id = seguimiento.Id }, seguimiento);
        }

        // DELETE: api/Seguimiento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Seguimiento>> DeleteSeguimiento(int id)
        {
            var seguimiento = await _context.Seguimiento.FindAsync(id);
            if (seguimiento == null)
            {
                return NotFound();
            }

            _context.Seguimiento.Remove(seguimiento);
            await _context.SaveChangesAsync();

            return seguimiento;
        }

        private bool SeguimientoExists(int id)
        {
            return _context.Seguimiento.Any(e => e.Id == id);
        }
    }
}
