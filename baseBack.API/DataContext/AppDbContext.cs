using Microsoft.EntityFrameworkCore;

namespace baseBack.API.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Models.Contato> Contatos { get; set; }
    }
}
