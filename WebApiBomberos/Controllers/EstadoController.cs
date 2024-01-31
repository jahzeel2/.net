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
            using (var DBcontext = _context)
            {
                try
                {
                    var obj = DBcontext.Estado.AsNoTracking().SingleOrDefault(r => r.id == id);
                    if (obj != null)
                    {
                        res.dato = obj;
                        res.code = "200";
                        res.message = "Dato obtenido correctamente";
                    }
                    else if (obj == null)
                    {
                        res.dato = obj;
                        res.code = "204";
                        res.message = "No existen datos en la base de datos";
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

        // PUT: api/Estado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, Estado estado)
        {
            using (var DBcontext = _context)
            {
                try
                {
                    var obj = DBcontext.Estado.FirstOrDefault(r => r.id == id);
                    if (obj != null)
                    {
                        obj.nombre = estado.nombre;

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
                    res.error = "Error al obtener el dato " + ex.Message;
                }

                data = JsonConvert.SerializeObject(res);

                return Ok(data);
            }
        }

        // POST: api/Estado
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

        // DELETE: api/Estado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            using (var DBcontext = _context)
            {
                try
                {
                    var obj = DBcontext.Estado.SingleOrDefault(r => r.id == id);

                    if (obj != null)
                    {
                        //baja logica
                        //entidad entity = DBcontext.entidad.SingleOrDefault(r => r.id == id);
                        obj.activo = false;
                        //DBcontext.Entry(entidad).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //await DBcontext.SaveChangesAsync();

                        //baja eliminar BD
                        // rol r = DBcontext.rol.Single(us => us.id == id);
                        DBcontext.Remove(obj);
                        await DBcontext.SaveChangesAsync();

                        res.code = "200";
                        res.message = "Dato eliminado correctamente";
                    }
                    else
                    {
                        res.code = "204";
                        res.message = "No se pudo eliminar los datos de la base de datos";
                    }
                }
                catch (Exception ex)
                {
                    res.code = "500";
                    res.message = "No se pudo eliminar el dato de la base de datos";
                    res.error = "Error al insertar el dato " + ex.Message;
                }

                data = JsonConvert.SerializeObject(res);

                return Ok(data);
            }
        }

        private bool EstadoExists(int id)
        {
            return _context.Estado.Any(e => e.id == id);
        }
    }
}
