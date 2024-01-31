using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiBomberos.Data;
using WebApiBomberos.Models;

namespace WebApiBomberos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoServicioController : ControllerBase
    {
        private readonly WebApiBomberosContext _context;

        public TipoServicioController(WebApiBomberosContext context)
        {
            _context = context;
        }

        // GET: api/TipoServicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoServicio>>> GetTipoServicio()
        {
            return await _context.TipoServicio.ToListAsync();
        }

        // GET: api/TipoServicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoServicio>> GetTipoServicio(int id)
        {
            var tipoServicio = await _context.TipoServicio.FindAsync(id);

            if (tipoServicio == null)
            {
                return NotFound();
            }

            return tipoServicio;
        }

        // PUT: api/TipoServicio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoServicio(int id, TipoServicio tipoServicio)
        {
            if (id != tipoServicio.id)
            {
                return BadRequest();
            }

            _context.Entry(tipoServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoServicioExists(id))
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

        // POST: api/TipoServicio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoServicio>> PostTipoServicio(TipoServicio tipoServicio)
        {
            _context.TipoServicio.Add(tipoServicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoServicio", new { id = tipoServicio.id }, tipoServicio);
        }

        // DELETE: api/TipoServicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoServicio(int id)
        {
            var tipoServicio = await _context.TipoServicio.FindAsync(id);
            if (tipoServicio == null)
            {
                return NotFound();
            }

            _context.TipoServicio.Remove(tipoServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoServicioExists(int id)
        {
            return _context.TipoServicio.Any(e => e.id == id);
        }
    }
}
