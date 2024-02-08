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
    public class TipoServicioController : ControllerBase
    {
        private readonly WebApiBomberosContext _context;


        public Result<TipoServicio> res = new Result<TipoServicio>();
        string data;

        private readonly IConfiguration configuration;

        public TipoServicioController(WebApiBomberosContext context)
        {
            _context = context;
        }

        // GET: api/TipoServicio
        [HttpGet("paginate/{pagina},{cantidad},{descripcion}")]
        public async Task<ActionResult<Result<TipoServicio>>> GetTipoServicio(int pagina, int cantidad, string descripcion)
        {
            Paginate paginate = new Paginate();
            paginate.cantidadMostrar = cantidad;
            paginate.pagina = pagina;

            using (var DBcontext = _context)
            {
                try
                {
                    var queryable = DBcontext.TipoServicio.AsNoTracking().AsQueryable();

                    // Agregar condición para búsqueda por descripcion si el parámetro descripcion no es nulo ni vacío
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
                    res.error = "Error al obtener el dato " + ex.Message;
                }


                data = JsonConvert.SerializeObject(res);

                return Ok(data);
            }
        }

        // GET: api/TipoServicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoServicio>> GetTipoServicio(int id)
        {
            using (var DBcontext = _context)
            {
                try
                {
                    var obj = DBcontext.TipoServicio.AsNoTracking().SingleOrDefault(r => r.id == id);
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
                    res.error = "Error al obtener el dato " + ex.Message;
                }

                data = JsonConvert.SerializeObject(res);

                return Ok(data);
            }
        }

        // PUT: api/TipoServicio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoServicio(int id, TipoServicio ts)
        {
            using (var DBcontext = _context)
            {
                try
                {
                    var obj = DBcontext.TipoServicio.FirstOrDefault(r => r.id == id);
                    if (obj != null)
                    {
                        obj.descripcion = ts.descripcion;

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

        // POST: api/TipoServicio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoServicio>> PostTipoServicio(TipoServicio ts)
        {
            using (var DBcontext = _context)
            {
                try
                {
                    var verificar = DBcontext.TipoServicio.SingleOrDefault(r => r.descripcion == ts.descripcion);

                    if (verificar == null)
                    {
                        TipoServicio obj = new TipoServicio();
                        obj.descripcion = ts.descripcion;
                        obj.activo = true;

                        DBcontext.TipoServicio.Add(obj);
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

        // DELETE: api/TipoServicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoServicio(int id)
        {
            using (var DBcontext = _context)
            {
                try
                {
                    var obj = DBcontext.TipoServicio.SingleOrDefault(r => r.id == id);

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

        private bool TipoServicioExists(int id)
        {
            return _context.TipoServicio.Any(e => e.id == id);
        }
    }
}
