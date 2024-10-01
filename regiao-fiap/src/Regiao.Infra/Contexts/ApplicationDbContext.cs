using Microsoft.EntityFrameworkCore;
using Regiao.Infra.Contexts.Mappings;
using RegiaoEntities = Regiao.Domain.Entities;

namespace Regiao.Infra.Contexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<RegiaoEntities.Regiao> Regioes { get; set; }
    public DbSet<RegiaoEntities.Regiao> Cidades { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new RegiaoMapping().Configure(modelBuilder.Entity<RegiaoEntities.Regiao>());
        new CidadeMapping().Configure(modelBuilder.Entity<RegiaoEntities.Cidade>());
    }
}