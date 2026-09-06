using Franquias.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Franquia> Franquias => Set<Franquia>();
}