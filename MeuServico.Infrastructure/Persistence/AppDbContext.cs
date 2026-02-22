using Microsoft.EntityFrameworkCore;
using MeuServico.Core.Entities;


namespace MeuServico.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<Manutencao> Manutencoes { get; set; }
        public DbSet<DespesaGeral> DespesasGerais { get; set; }
        public DbSet<HistoricoValorVenal> HistoricoValorVenal { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        

        // =============================
        // CONFIGURAÇÕES
        // =============================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //-----------------------------------
            // CARRO
            //-----------------------------------

            modelBuilder.Entity<Carro>()
                .HasIndex(c => c.Placa)
                .IsUnique();

            modelBuilder.Entity<Carro>()
                .HasOne(c => c.Empresa)
                .WithMany(e => e.Carros)
                .HasForeignKey(c => c.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            //-----------------------------------
            // MANUTENÇÃO
            //-----------------------------------

            modelBuilder.Entity<Manutencao>()
                .HasOne(m => m.Carro)
                .WithMany(c => c.Manutencoes)
                .HasForeignKey(m => m.CarroId);

            //-----------------------------------
            // DESPESAS
            //-----------------------------------
            modelBuilder.Entity<DespesaGeral>()
                .HasOne(d => d.Carro)
                .WithMany(c => c.Despesas)
                .HasForeignKey(d => d.CarroId)
                .OnDelete(DeleteBehavior.SetNull);

            //-----------------------------------
            // HISTÓRICO VALOR VENAL
            //-----------------------------------
            modelBuilder.Entity<HistoricoValorVenal>()
                .HasOne(h => h.Carro)
                .WithMany()
                .HasForeignKey(h => h.CarroId);

            //-----------------------------------
            // LOCAÇÃO
            //-----------------------------------
            modelBuilder.Entity<Locacao>()
                .HasOne(l => l.Carro)
                .WithMany(c => c.Locacoes)
                .HasForeignKey(l => l.CarroId);

            modelBuilder.Entity<Locacao>()
                .HasOne(l => l.Cliente)
                .WithMany(c => c.Locacoes)
                .HasForeignKey(l => l.ClienteId);
        }
    }
}