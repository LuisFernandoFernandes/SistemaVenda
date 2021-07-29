using Microsoft.AspNetCore.Mvc;
using SistemaVenda.DAL;
using SistemaVenda.Helpers;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class LoginController : Controller
    {
        protected ApplicationDbContext mContext;

        public LoginController(ApplicationDbContext context)
        {
            mContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            ViewData["ErroLogin"] = string.Empty;

            if (ModelState.IsValid)
            {

                var Senha = Criptografia.GetMd5Hash(model.Senha);
                var usuario = mContext.Usuario.Where(x => x.Email == model.Email && x.Senha == Senha).FirstOrDefault();

                if (usuario == null)
                {
                    ViewData["ErroLogin"] = "O Email ou Senha informado não existe no sistema!";
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}
