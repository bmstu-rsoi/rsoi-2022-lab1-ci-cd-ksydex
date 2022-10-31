using API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext : DbContext
{
    public DbSet<Person> Persons => Set<Person>();

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}