using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using WebApiBomberos.Component;
using WebApiBomberos.components;
using WebApiBomberos.Components;
using WebApiBomberos.Data;
using WebApiBomberos.Models;

namespace WebApiBomberos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly WebApiBomberosContext _context;
        public Result<Estado> res = new Result<Estado>();
        string data;

        private readonly IConfiguration configuration;
        public EstadoController(WebApiBomberosContext context)
        {
            _context = context;
        }

        // GET: api/Estado
        // GET: api/Calle
        [HttpGet("paginate/{pagina},{cantidad},{nombre}")]
        public async Task<ActionResult<Result<Estado>>> GetCalle(int pagina, int cantidad, string nombre)
        {
            Paginate paginate = new Paginate();
            paginate.cantidadMostrar = cantidad;
            paginate.pagina = pagina;

            using (var DBcontext = _context)
            {
                try
                {
                    var queryable = DBcontext.Estado.AsNoTracking().AsQueryable();

                    // Agregar condición para búsqueda por nombre si el parámetro nombre no es nulo ni vacío
                    if (!string.IsNullOrEmpty(nombre))
                    {
                        queryable = queryable.Where(e => e.nombre.Contains(nombre));
                    }

                    double conteo = await queryable.CountAsync();
                    double TotalPaginas = Math.Ceiling(conteo / paginate.cantidadMostrar);

                    int totalPaginas = Convert.ToInt32(TotalPaginas);
                    int totalRegistros = Convert.ToInt32(conteo);

                    if (queryable.Any())
                    {
                        res.data = queryable.Paginar(paginate).ToList();
                        res.totalRegistros = totalRegistros;
                        res.totalPaginas = totalPaginas;
                        res.code = "200";
                        res.message = "Datos obtenidos correctamente";
                    }
                    else
                    {
                        res.code = "204";
                        res.message = "No existen datos que coincidan con la búsqueda";
                    }
                }
                catch (Exception ex)
                {
                    res.error = "Error al obtener el dato " + ex.Message;
                }


                data = JsonConvert.SerializeObject(res);

                return Ok(data);
            }
        }

        // GET: api/Estado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estado>> GetEstado(int id)
        {
            var estado = await _context.Estado.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            return estado;
        }

        // PUT: api/Estado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, Estado estado)
        {
            if (id != estado.id)
            {
                return BadRequest();
            }

            _context.Entry(estado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
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

        // POST: api/Estado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estado>> PostEstado(Estado estado)
        {
            _context.Estado.Add(estado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstado", new { id = estado.id }, estado);
        }

        // DELETE: api/Estado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var estado = await _context.Estado.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }

            _context.Estado.Remove(estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadoExists(int id)
        {
            return _context.Estado.Any(e => e.id == id);
        }
    }
}
