using BackEndApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<RegistroServico> tbRegistroServico { get; set; }


    }
}
