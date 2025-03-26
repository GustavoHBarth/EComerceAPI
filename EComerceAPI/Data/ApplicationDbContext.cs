using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Models;

namespace EComerceAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Produto> Products { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

    }
}
