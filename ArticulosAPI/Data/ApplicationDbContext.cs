using ArticulosAPI.Modelos;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace ArticulosAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 

        }
            
   
         public DbSet<Articulo> Articulos { get; set; }

        public ApplicationDbContext(DbSet<Articulo> articulos)
        {
            Articulos = articulos;
        }
            
    }


}

  


