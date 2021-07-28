using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.DAL;
using SistemaVenda.Entidades;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class ProdutoController : Controller
    {
        protected ApplicationDbContext mContext;

        public ProdutoController(ApplicationDbContext context)
        {
            mContext = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Produto> lista = mContext.Produto.Include(x => x.Categoria).ToList();
            mContext.Dispose();
            return View(lista);
        }

        private IEnumerable<SelectListItem> ListaCategoria()
        {   //Menu com as categorias
            List<SelectListItem> lista = new List<SelectListItem>();
            //Primeiro item deve ser vazio para que não apareça nada antes do usuário selecionar
            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty

            });
            foreach (var item in mContext.Categoria.ToList())
            {   
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao.ToString()
                });
            }

            return lista;
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            ProdutoViewModel viewModel = new ProdutoViewModel();
            viewModel.ListaCategorias = ListaCategoria();

            if (id != null)
            {
                var entidade = mContext.Produto.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Descricao = entidade.Descricao;
                viewModel.Quantidade = entidade.Quantidade;
                viewModel.Valor = entidade.Valor;
                viewModel.CodigoCategoria = entidade.CodigoCategoria;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoViewModel entidade)
        {

            if (ModelState.IsValid)
            {
                Produto objProduto = new Produto()
                {   
                    Codigo = entidade.Codigo,
                    Descricao = entidade.Descricao,
                    Quantidade = entidade.Quantidade,
                    Valor = (decimal)entidade.Valor,
                    CodigoCategoria = (int)entidade.CodigoCategoria
                };

                if (entidade.Codigo == null)
                {
                    mContext.Produto.Add(objProduto);
                }
                else
                {
                    mContext.Entry(objProduto).State = EntityState.Modified;
                }

                mContext.SaveChanges();
            }
            else
            {   //É necessário retornar a lista de categorias para que o conteúdo não se perca caso o cadastro seja inválido.
                entidade.ListaCategorias = ListaCategoria();
                return View(entidade);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var ent = new Produto() { Codigo = id };
            mContext.Attach(ent);
            mContext.Remove(ent);
            mContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
