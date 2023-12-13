using medsys.Entities;
using Microsoft.EntityFrameworkCore;

namespace medsys.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Consultation> Consultation { get; set; }
    }
}
