using Impacta.autenticacao.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace Impacta.autenticacao.MVC.Controllers
{
	public class HomeController : Controller
	{


		public ActionResult Inicio()
		{
			return View();
		}

		public ActionResult AreaLivre()
		{
			ViewBag.Message = "Você esta em uma pagina de acesso livre";

			return View();
		}

		public ActionResult AreaRestrita()
		{
			ViewBag.Message = "Você está na area restrita.";

			return View();
		}
		public ActionResult LoginView()
		{
			ViewBag.Message = "Você está na pagina login, seja bem vindo";
			return View();
		}
		public ActionResult CriarLogin()
		{
			Usuario usuario = null;

			return View(usuario);
		}

		[HttpPost]
		public ActionResult CriarLogin(Usuario usuario)
		{
			//private bool SalvarUsuario(Usuario usuario)
			{
				//bool resultado = SalvarUsuario(usuario);
				if (SalvarUsuario(usuario))
				{
					return View("Inicio");
				}
				else
				{
					return View("CriarLogin");
				}
			}

		}

		private bool SalvarUsuario(Usuario usuario)
		{
			bool retorno = false;

			var usuarioStore = new UserStore<IdentityUser>();
			var usuarioGerenciador =
			new UserManager<IdentityUser>(usuarioStore);

			var usuarioInfo = new IdentityUser() { UserName = usuario.UserName };

			IdentityResult resultado =
			usuarioGerenciador.Create(usuarioInfo, usuario.Passoword);


			if (resultado.Succeeded)
			{

				var gerenciadorDeAutenticacao =
				HttpContext.GetOwinContext().Authentication;
				var identidadeUsuario = usuarioGerenciador.CreateIdentity(
				usuarioInfo,
				DefaultAuthenticationTypes.ApplicationCookie);
				gerenciadorDeAutenticacao.SignIn(
				new AuthenticationProperties() { },
				identidadeUsuario);
				Response.Redirect("Inicio");
				retorno = true;
			}
			else
			{
				ViewBag.Error = resultado.Errors;

			}
			return retorno;
		}

		[HttpPost]
		public ActionResult LoginView(Usuario usuario)
		{
			if (AutenticarUsuario(usuario))
			{
				return View("Area Restrita");
			}
			else
			{
				return View("inicio");
			}
		}

		private bool AutenticarUsuario(Usuario usuario)
		{
			bool retorno = false;
			var usuarioStore = new UserStore<IdentityUser>();
			var usuarioGerenciador =
            new UserManager<IdentityUser>(usuarioStore);
			var IdentidadeUsuario = usuarioGerenciador.Find(usuario.UserName, usuario.Passoword);

			if (usuario != null)
			{
				var gerenciadorDeAutenticacao = HttpContext
				.GetOwinContext().Authentication;

				var identidade = usuarioGerenciador.CreateIdentity(IdentidadeUsuario,
				DefaultAuthenticationTypes.ApplicationCookie);
				gerenciadorDeAutenticacao.SignIn(
				new AuthenticationProperties()
				{ IsPersistent = false },identidade);
				retorno = true;
			}
			else
			{
				ViewBag.Mesagem.Error = "Usuario ou senha invalida.";
				//mensagemErro.Text = "Usuario ou senha invalida.";
			}
			return retorno;
		}
	}
}


