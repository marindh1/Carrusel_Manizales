using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS_Insumos.Models;

namespace MS_Insumos.Controllers
{
    //api/Insumos
    [Route("api/[controller]")]
    [ApiController]
    public class InsumosController : ControllerBase
    {
        private readonly MS_InsumosContext _context;

        public InsumosController(MS_InsumosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<Insumos> GetInsumos()
        {
            return _context.Insumos.Where(i => i.Estado == 1);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Insumos>> GetInsumos(int id)
        {
            var insumos = await _context.Insumos.FindAsync(id);

            if (insumos == null)
            {
                return NotFound();
            }

            return insumos;
        }

        [HttpGet("porcategoria/{id}")]
        public IQueryable<Insumos> GetInsumosByCategoria(int id)
        {
            var insumos = _context.Insumos.Where(r=> r.CategoriaId == id);

            if (insumos == null)
            {
                return null;
            }

            return insumos;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsumos(int id, Insumos insumos)
        {
            if (id != insumos.Id)
            {
                return BadRequest();
            }

            _context.Entry(insumos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsumosExists(id))
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
        public async Task<ActionResult<Insumos>> PostInsumos(Insumos insumos)
        {
            _context.Insumos.Add(insumos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsumos", new { id = insumos.Id }, insumos);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Insumos>> DeleteInsumos(int id)
        {
            var insumos = await _context.Insumos.FindAsync(id);
            if (insumos == null)
            {
                return NotFound();
            }

            _context.Insumos.Remove(insumos);
            await _context.SaveChangesAsync();

            return insumos;
        }

        private bool InsumosExists(int id)
        {
            return _context.Insumos.Any(e => e.Id == id);
        }
    }
}
