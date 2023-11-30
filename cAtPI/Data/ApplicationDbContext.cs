using Microsoft.EntityFrameworkCore;

namespace cAtPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CatBase> Cats { get; set; }
    }
}
