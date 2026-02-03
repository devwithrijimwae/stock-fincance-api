using Microsoft.EntityFrameworkCore;
using stock_fincance_api.Models;

namespace stock_fincance_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }
    }
}
