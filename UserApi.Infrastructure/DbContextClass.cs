using Microsoft.EntityFrameworkCore;
using UserApi.Core.Models;

namespace UserApi.Models;

public partial class DbContextClass : DbContext
{
    public DbContextClass()
    {
    }

    public DbContextClass(DbContextOptions<DbContextClass> options)
        : base(options)
    {
    }

    public virtual DbSet<User> User { get; set; }    
}
