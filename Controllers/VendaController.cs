using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaVenda.DAL;
using SistemaVenda.Entidades;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaVenda.Controllers
{
    public class VendaController : Controller
    {
        protected ApplicationDbContext mContext;

        public VendaController(ApplicationDbContext context)
        {
            mContext = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Venda> lista = mContext.Venda.ToList();
            mContext.Dispose();
            return View(lista);
        }

        private IEnumerable<SelectListItem> ListaProdutos()
        {   //Menu com os produtos
            List<SelectListItem> lista = new List<SelectListItem>();
            //Primeiro item deve ser vazio para que não apareça nada antes do usuário selecionar
            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty

            });
            foreach (var item in mContext.Produto.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao.ToString()
                });
            }

            return lista;
        }

        private IEnumerable<SelectListItem> ListaClientes()
        {   //Menu com os Clientes
            List<SelectListItem> lista = new List<SelectListItem>();
            //Primeiro item deve ser vazio para que não apareça nada antes do usuário selecionar
            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty

            });
            foreach (var item in mContext.Cliente.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Nome.ToString()
                });
            }

            return lista;
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            VendaViewModel viewModel = new VendaViewModel();
            viewModel.ListaClientes = ListaClientes();
            viewModel.ListaProdutos = ListaProdutos();

            if (id != null)
            {
                var entidade = mContext.Venda.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Data = entidade.Data;
                viewModel.CodigoCliente = entidade.CodigoCliente;
                viewModel.Total = entidade.Total;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(VendaViewModel entidade)
        {

            if (ModelState.IsValid)
            {
                Venda objVenda = new Venda()
                {
                    Codigo = entidade.Codigo,
                    Data = (DateTime)entidade.Data,
                    CodigoCliente = (int)entidade.CodigoCliente,
                    Total = entidade.Total, //Vai no modelo Venda e pega o Json que foi gerado com os atributos da entidade VendaProduto
                    Produtos = JsonConvert.DeserializeObject<ICollection<VendaProdutos>>(entidade.JsonProdutos)
                };

                if (entidade.Codigo == null)
                {
                    mContext.Venda.Add(objVenda);
                }
                else
                {
                    mContext.Entry(objVenda).State = EntityState.Modified;
                }

                mContext.SaveChanges();
            }
            else
            {   //É necessário retornar a lista de clientes para que o conteúdo não se perca caso o cadastro seja inválido.
                entidade.ListaClientes = ListaClientes();
                entidade.ListaProdutos = ListaProdutos();
                return View(entidade);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var ent = new Venda() { Codigo = id };
            mContext.Attach(ent);
            mContext.Remove(ent);
            mContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("LerValorProduto/{CodigoProduto}")]
        public decimal LerValorProduto(int CodigoProduto)
        {
            return mContext.Produto.Where(x => x.Codigo == CodigoProduto).Select(x => x.Valor).FirstOrDefault();
        }

    }
}
