using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure
{
    public class PassInDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public DbSet<Attendee> Attendees { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\InstanceLocal;Initial Catalog=PASS_IN;User ID=AdminLocal;Password=Admin123;Encrypt=False");
        }
    }
}
