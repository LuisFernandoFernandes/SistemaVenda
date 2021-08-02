using Aplicacao.Servico.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.Helpers;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class LoginController : Controller
    {
        protected IHttpContextAccessor HttpContextAcessor;
        readonly IServicoAplicacaoUsuario ServicoAplicacaoUsuario;

        public LoginController(IServicoAplicacaoUsuario servicoAplicacaoUsuario, IHttpContextAccessor httpContext)
        {
            ServicoAplicacaoUsuario = servicoAplicacaoUsuario;
            HttpContextAcessor = httpContext;
        }

        public IActionResult Index(int? id)
        {
            if (id != null)
            {
                if(id == 0)
                {
                    HttpContext.Session.Clear();
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            ViewData["ErroLogin"] = string.Empty;

            if (ModelState.IsValid)
            {

                var Senha = Criptografia.GetMd5Hash(model.Senha);
                bool login = ServicoAplicacaoUsuario.ValidarLogin(model.Email, Senha);
                var usuario = ServicoAplicacaoUsuario.RetornarDadosUsuario(model.Email, Senha);
                if(login)
                {
                    HttpContext.Session.SetString(Sessao.NOME_USUARIO, usuario.Nome);
                    HttpContext.Session.SetString(Sessao.EMAIL_USUARIO, usuario.Email);
                    HttpContext.Session.SetInt32(Sessao.CODIGO_USUARIO, (int)usuario.Codigo);
                    HttpContext.Session.SetInt32(Sessao.LOGADO, 1 );
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ErroLogin"] = "O Email ou Senha informado não existe no sistema!";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}
