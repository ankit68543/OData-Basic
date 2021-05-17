using Microsoft.EntityFrameworkCore;
using OData_Basic.Models;


namespace OData_Basic.Context
{
    public class MusicContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }

        public MusicContext() : base() { }
        public MusicContext(DbContextOptions opts) : base(opts) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Album>().HasMany(a => a.Songs).WithOne(s => s.Album);
            builder.Entity<Album>();
        }
    }
}
