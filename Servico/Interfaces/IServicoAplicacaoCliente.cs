using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoCliente
    {
        IEnumerable<SelectListItem> ListaClientesDropDownList();
        public IEnumerable<ClienteViewModel> Listagem();

        ClienteViewModel CarregarRegistro(int codigoCliente);
        void Cadastrar(ClienteViewModel cliente);
        void Excluir(int id);
    }
}
