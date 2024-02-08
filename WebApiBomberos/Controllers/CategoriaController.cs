using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class CategoriaController : ControllerBase
    {
        private readonly WebApiBomberosContext _context;
        public Result<Categoria> res = new Result<Categoria>();
        string data;

        private readonly IConfiguration configuration;

        public CategoriaController(WebApiBomberosContext context)
        {
            _context = context;
        }

        // GET: api/Categoria
        [HttpGet("paginate/{pagina},{cantidad},{descripcion}")]
        public async Task<ActionResult<Result<Categoria>>> GetCalle(int pagina, int cantidad, string descripcion)
        {
            Paginate paginate = new Paginate();
            paginate.cantidadMostrar = cantidad;
            paginate.pagina = pagina;

            using (var DBcontext = _context)
            {
                try
                {
                    var queryable = DBcontext.Categoria.AsNoTracking().AsQueryable();

                    // Agregar condición para búsqueda por nombre si el parámetro nombre no es nulo ni vacío
                    if (!string.IsNullOrEmpty(descripcion))
                    {
                        queryable = queryable.Where(e => e.descripcion.Contains(descripcion));
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
                    res.code = "500";
                    res.error = "Error al obtener el dato " + ex.Message;
                }


                data = JsonConvert.SerializeObject(res);

                return Ok(data);
            }
        }


        // GET: api/Categoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            using (var DBcontext = _context)
            {
                try
                {
                    var obj = DBcontext.Categoria.AsNoTracking().SingleOrDefault(r => r.id == id);
                    if (obj != null)
                    {
                        res.dato = obj;
                        res.code = "200";
                        res.message = "Dato obtenido correctamente";
                    }
                    else if (obj is null)
                    {
                        res.dato = obj;
                        res.code = "204";
                        res.message = "No existen datos en la base de datos";
                    }
                }
                catch (Exception ex)
                {
                    res.code = "500";
                    res.error = "Error al obtener el dato " + ex.Message;
                }

                data = JsonConvert.SerializeObject(res);

                return Ok(data);
            }
        }


        // PUT: api/Categoria/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            using (var DBcontext = _context)
            {
                try
                {
                    var obj = DBcontext.Categoria.FirstOrDefault(r => r.id == id);
                    if (obj != null)
                    {
                        obj.descripcion = categoria.nombre;

                        DBcontext.Entry(obj).State = EntityState.Modified;
                        await DBcontext.SaveChangesAsync();

                        res.dato = obj;
                        res.code = "200";
                        res.message = "Dato modificado correctamente";
                    }
                    else if (obj == null)
                    {
                        res.code = "204";
                        res.message = "No existen datos en la base de datos";
                    }
                }
                catch (Exception ex)
                {
                    res.code = "500";
                    res.error = "Error al obtener el dato " + ex.Message;
                }

                data = JsonConvert.SerializeObject(res);

                return Ok(data);
            }
        }

        // POST: api/Categoria
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estado>> PostEstado(Estado estado)
        {
            using (var DBcontext = _context)
            {
                try
                {
                    var verificar = DBcontext.Estado.SingleOrDefault(r => r.nombre == estado.nombre);

                    if (verificar == null)
                    {
                        Estado obj = new Estado();
                        obj.nombre = estado.nombre;
                        obj.activo = true;

                        DBcontext.Estado.Add(obj);
                        await DBcontext.SaveChangesAsync();

                        res.code = "200";
                        res.message = "Dato insertado correctamente";
                    }
                    else
                    {
                        res.code = "204";
                        res.message = "El dato ingresado ya existe en la base de datos";
                    }
                }
                catch (Exception ex)
                {
                    res.code = "500";
                    res.message = "No se pudo insertar los datos en la base de datos";
                    res.error = "Error al insertar el dato " + ex.Message;
                }

                data = JsonConvert.SerializeObject(res);

                return Ok(data);
            }
        }


        // DELETE: api/Categoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.id == id);
        }
    }
}
