using SistemaVenda.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Entity

namespace SistemaVenda.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<VendaProdutos> VendaProdutos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VendaProdutos>().HasKey(x => new { x.CodigoVenda, x.CodigoProduto });

            modelBuilder.Entity<VendaProdutos>()
                 .HasRequired(x => x.Venda)
                 .WithMany(y => y.Produtos)
                 .HasForeignKey(x => x.CodigoVenda);

            modelBuilder.Entity<VendaProdutos>()
                 .HasRequired(x => x.Produto)
                 .WithMany(y => y.Vendas)
                 .HasForeignKey(x => x.CodigoProduto);


        }
    }
}
