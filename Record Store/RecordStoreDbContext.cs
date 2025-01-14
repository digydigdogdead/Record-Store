using Microsoft.EntityFrameworkCore;

namespace Record_Store
{
    public class RecordStoreDbContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }

        public RecordStoreDbContext(DbContextOptions<RecordStoreDbContext> options) : base(options) { }

    }
}
