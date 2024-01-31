using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiBomberos.Models;

namespace WebApiBomberos.Data
{
    public class WebApiBomberosContext : DbContext
    {
        public WebApiBomberosContext (DbContextOptions<WebApiBomberosContext> options)
            : base(options)
        {
        }

        public DbSet<WebApiBomberos.Models.Estado> Estado { get; set; } = default!;

        public DbSet<WebApiBomberos.Models.TipoServicio> TipoServicio { get; set; }
    }
}
