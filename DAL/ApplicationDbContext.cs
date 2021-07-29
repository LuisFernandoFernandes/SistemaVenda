using SistemaVenda.Entidades;
using Microsoft.EntityFrameworkCore;

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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<VendaProdutos>().HasKey(x => new { x.CodigoVenda, x.CodigoProduto });

            //
            builder.Entity<Venda>()
                .HasMany<VendaProdutos>(x => x.Produtos)
                .WithOne(x => x.Venda)
                .HasForeignKey(x => x.CodigoVenda);

            builder.Entity<VendaProdutos>()
                .HasOne<Produto>(x => x.Produto)
                .WithMany(x => x.Vendas)
                .HasForeignKey(x => x.CodigoProduto);

            //.WithMany(y => y.Vendas)

            //builder.Entity<VendaProdutos>()
            //     .HasOne(x => x.Produto)                 
            //     .HasForeignKey(x => x.CodigoProduto);
        }
    }
}
