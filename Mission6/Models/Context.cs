using Microsoft.EntityFrameworkCore;

namespace Mission6.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<Application> Applications { get; set; }
}