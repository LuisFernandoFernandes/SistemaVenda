using Aplicacao.Servico.Interfaces;
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
    public class CategoriaController : Controller
    {
        readonly IServicoAplicacaoCategoria ServicoAplicacaoCategoria;

        public CategoriaController(IServicoAplicacaoCategoria servicoAplicacaoCategoria)
        {
            ServicoAplicacaoCategoria = servicoAplicacaoCategoria;
        }

        public IActionResult Index()
        {
            return View(ServicoAplicacaoCategoria.Listagem());
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            CategoriaViewModel viewModel = new CategoriaViewModel();

            if (id != null)
            {
                viewModel = ServicoAplicacaoCategoria.CarregarRegistro((int)id);
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CategoriaViewModel entidade)
        {
            if (ModelState.IsValid)
            {
                ServicoAplicacaoCategoria.Cadastrar(entidade);
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
            ServicoAplicacaoCategoria.Excluir(id);
            return RedirectToAction("Index");
        }

    }
}