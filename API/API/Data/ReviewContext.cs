using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ReviewContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }
        public ReviewContext(DbContextOptions options) : base(options)
        {
        }
    }
}
