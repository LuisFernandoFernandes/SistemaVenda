using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoVenda
    {
        IEnumerable<GraficoViewModel> ListaGrafico();

        IEnumerable<VendaViewModel> Listagem();

        VendaViewModel CarregarRegistro(int codigoVenda);

        void Cadastrar(VendaViewModel venda);

        void Excluir(int id);
    }
}
