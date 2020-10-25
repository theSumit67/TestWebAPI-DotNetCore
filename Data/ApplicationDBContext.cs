using Microsoft.EntityFrameworkCore;
using TestWebAPI.Models;

namespace TestWebAPI.Data
{
    class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<NationalPark> NationalParks { get; set; }
    }
}