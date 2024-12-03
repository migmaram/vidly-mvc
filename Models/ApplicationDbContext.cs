using System.Data.Entity;
using Vidly.Migrations;

namespace Vidly.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Genre> Genres{ get; set; }
        public DbSet<Rental> Rentals { get; set; }
    }
}
