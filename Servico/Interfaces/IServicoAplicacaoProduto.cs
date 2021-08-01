using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoProduto
    {
        IEnumerable<SelectListItem> ListaProdutosDropDownList();
        public IEnumerable<ProdutoViewModel> Listagem();

        ProdutoViewModel CarregarRegistro(int codigoProduto);
        void Cadastrar(ProdutoViewModel produto);
        void Excluir(int id);
    }
}
