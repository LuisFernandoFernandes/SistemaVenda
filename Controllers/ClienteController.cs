using Microsoft.AspNetCore.Mvc;
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
    public class ClienteController : Controller
    {
        protected ApplicationDbContext mContext;

        public ClienteController(ApplicationDbContext context)
        {
            mContext = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Cliente> lista = mContext.Cliente.ToList();
            mContext.Dispose();
            return View(lista);
        }
        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            ClienteViewModel viewModel = new ClienteViewModel();

            if (id != null)
            {
                var entidade = mContext.Cliente.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Nome = entidade.Nome;
                viewModel.CNPJ_CPF = entidade.CNPJ_CPF;
                viewModel.Email = entidade.Email;
                viewModel.Celular = entidade.Celular;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteViewModel entidade)
        {

            if (ModelState.IsValid)
            {
                Cliente objCliente = new Cliente()
                {   
                    Codigo = entidade.Codigo,
                    Nome = entidade.Nome,
                    CNPJ_CPF = entidade.CNPJ_CPF,
                    Email = entidade.Email,
                    Celular = entidade.Celular
                };

                if (entidade.Codigo == null)
                {
                    mContext.Cliente.Add(objCliente);
                }
                else
                {
                    mContext.Entry(objCliente).State = EntityState.Modified;
                }

                mContext.SaveChanges();
            }
            else
            {
                return View(entidade);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var ent = new Cliente() { Codigo = id };
            mContext.Attach(ent);
            mContext.Remove(ent);
            mContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
