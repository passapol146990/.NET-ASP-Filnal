using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.data
{
    public class UsersContext:DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
    }
}
