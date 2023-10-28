using API_Note.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Note.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Note> Note { get; set; }

        public DbSet<Owner> Owner { get; set; }
    }
}
