using Microsoft.EntityFrameworkCore;
using ReceptekWebAPI.Entities;

namespace ReceptekWebAPI.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
