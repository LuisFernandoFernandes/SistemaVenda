namespace SistemaVenda.Entidades
{
    public class VendaProdutos
    {
        public int CodigoVenda { get; set; }
        public virtual Venda Venda { get; set; }
        
        public int CodigoProduto { get; set; }
        public virtual Produto Produto { get; set; }

        public double Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
