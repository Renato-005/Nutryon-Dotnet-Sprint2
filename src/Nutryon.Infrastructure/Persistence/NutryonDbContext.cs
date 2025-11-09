using Microsoft.EntityFrameworkCore;
using Nutryon.Domain.Entities;

namespace Nutryon.Infrastructure.Persistence;

public class NutryonDbContext : DbContext
{
    public NutryonDbContext(DbContextOptions<NutryonDbContext> options) : base(options) {}

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Refeicao> Refeicoes => Set<Refeicao>();
    public DbSet<RefeicaoItem> RefeicaoItens => Set<RefeicaoItem>();
    public DbSet<TipoRefeicao> TiposRefeicao => Set<TipoRefeicao>();
    public DbSet<UnidadeMedida> Unidades => Set<UnidadeMedida>();
    public DbSet<Ingrediente> Ingredientes => Set<Ingrediente>();

    protected override void OnModelCreating(ModelBuilder m)
    {
        m.Entity<Usuario>(e =>
        {
            e.ToTable("USUARIO");
            e.HasKey(x => x.IdUsuario);
            e.Property(x => x.IdUsuario).HasColumnName("ID_USUARIO");
            e.Property(x => x.NomeUsuario).HasColumnName("NOM_USUARIO").HasMaxLength(100).IsRequired();
            e.Property(x => x.Email).HasColumnName("EMAIL").HasMaxLength(120).IsRequired();
            e.HasIndex(x => x.Email).IsUnique();
        });

        m.Entity<TipoRefeicao>(e =>
        {
            e.ToTable("TIPO_REFEICAO");
            e.HasKey(x => x.IdTipoRefeicao);
            e.Property(x => x.IdTipoRefeicao).HasColumnName("ID_TIPO_REFEICAO");
            e.Property(x => x.NomeTipo).HasColumnName("NOM_TIPO").HasMaxLength(50).IsRequired();
            e.HasData(
                new TipoRefeicao { IdTipoRefeicao = 1, NomeTipo = "Café da manhã" },
                new TipoRefeicao { IdTipoRefeicao = 2, NomeTipo = "Almoço" },
                new TipoRefeicao { IdTipoRefeicao = 3, NomeTipo = "Jantar" }
            );
        });

        m.Entity<UnidadeMedida>(e =>
        {
            e.ToTable("UNIDADE_MEDIDA");
            e.HasKey(x => x.IdUnidade);
            e.Property(x => x.IdUnidade).HasColumnName("ID_UNIDADE");
            e.Property(x => x.Sigla).HasColumnName("SIGLA").HasMaxLength(10).IsRequired();
            e.Property(x => x.Descr).HasColumnName("DESCR").HasMaxLength(60);
            e.HasData(
                new UnidadeMedida { IdUnidade = 1, Sigla = "g", Descr = "grama" },
                new UnidadeMedida { IdUnidade = 2, Sigla = "ml", Descr = "mililitro" }
            );
        });

        m.Entity<Ingrediente>(e =>
        {
            e.ToTable("INGREDIENTE");
            e.HasKey(x => x.IdIngrediente);
            e.Property(x => x.IdIngrediente).HasColumnName("ID_INGREDIENTE");
            e.Property(x => x.NomeIngrediente).HasColumnName("NOM_INGREDIENTE").HasMaxLength(120).IsRequired();
            e.HasData(
                new Ingrediente { IdIngrediente = 1, NomeIngrediente = "Arroz", DtCriacao = DateOnly.FromDateTime(DateTime.UtcNow) },
                new Ingrediente { IdIngrediente = 2, NomeIngrediente = "Feijão", DtCriacao = DateOnly.FromDateTime(DateTime.UtcNow) },
                new Ingrediente { IdIngrediente = 3, NomeIngrediente = "Frango", DtCriacao = DateOnly.FromDateTime(DateTime.UtcNow) }
            );
        });

        m.Entity<Refeicao>(e =>
        {
            e.ToTable("REFEICAO");
            e.HasKey(x => x.IdRefeicao);
            e.Property(x => x.IdRefeicao).HasColumnName("ID_REFEICAO");
            e.Property(x => x.IdUsuario).HasColumnName("ID_USUARIO");
            e.Property(x => x.IdTipoRefeicao).HasColumnName("ID_TIPO_REFEICAO");
            e.Property(x => x.DtRef).HasColumnName("DT_REF");
            e.Property(x => x.Situacao).HasColumnName("SITUACAO").HasMaxLength(12);
            e.Property(x => x.Observacao).HasColumnName("OBSERVACAO").HasMaxLength(200);
            e.HasOne(x => x.Usuario).WithMany(u => u.Refeicoes).HasForeignKey(x => x.IdUsuario);
            e.HasOne(x => x.Tipo).WithMany().HasForeignKey(x => x.IdTipoRefeicao);
        });

        m.Entity<RefeicaoItem>(e =>
        {
            e.ToTable("REFEICAO_ITEM");
            e.HasKey(x => new { x.IdRefeicao, x.IdIngrediente });
            e.Property(x => x.IdRefeicao).HasColumnName("ID_REFEICAO");
            e.Property(x => x.IdIngrediente).HasColumnName("ID_INGREDIENTE");
            e.Property(x => x.Qtde).HasColumnName("QTDE").HasPrecision(10,3);
            e.Property(x => x.IdUnidade).HasColumnName("ID_UNIDADE");
            e.HasOne(x => x.Refeicao).WithMany(r => r.Itens).HasForeignKey(x => x.IdRefeicao);
            e.HasOne(x => x.Ingrediente).WithMany(i => i.Itens).HasForeignKey(x => x.IdIngrediente);
            e.HasOne(x => x.Unidade).WithMany().HasForeignKey(x => x.IdUnidade);
        });
    }
}