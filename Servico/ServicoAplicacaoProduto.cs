using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacao.Servico.Interfaces;
using Dominio.Interfaces;
using SistemaVenda.Dominio.Entidades;
using SistemaVenda.Models;

namespace Aplicacao.Servico
{
    public class ServicoAplicacaoProduto : IServicoAplicacaoProduto
    {
        private readonly IServicoProduto ServicoProduto;
        public ServicoAplicacaoProduto(IServicoProduto servicoProduto)
        {
            ServicoProduto = servicoProduto;
        }

        public void Cadastrar(ProdutoViewModel produto)
        {
            Produto item = new Produto()
            {
                Codigo = produto.Codigo,
                Descricao = produto.Descricao
            };

            ServicoProduto.Cadastrar(item);
        }

        public ProdutoViewModel CarregarRegistro(int codigoProduto)
        {
            var registro = ServicoProduto.CarregarRegistro(codigoProduto);

            ProdutoViewModel produto = new ProdutoViewModel()
            {
                Codigo = registro.Codigo,
                Descricao = registro.Descricao,
                Quantidade = registro.Quantidade,
                Valor = registro.Valor,
                CodigoCategoria = registro.CodigoCategoria
            };
            return produto;
        }

        public void Excluir(int id)
        {
            ServicoProduto.Excluir(id);
        }

        public IEnumerable<ProdutoViewModel> Listagem()
        {
            var lista = ServicoProduto.Listagem();
            List<ProdutoViewModel> listaProduto = new List<ProdutoViewModel>();

            foreach (var item in lista)
            {
                ProdutoViewModel produto = new ProdutoViewModel()
                {
                    Codigo = item.Codigo,
                    Descricao = item.Descricao,
                    Quantidade = item.Quantidade,
                    Valor = item.Valor,
                    CodigoCategoria = item.CodigoCategoria
                };
                listaProduto.Add(produto);
            }
            return listaProduto;
        }
    }
}
