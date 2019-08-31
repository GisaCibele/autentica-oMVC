using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

namespace Impacta.Identity.Terminal
{
	class Program
	{
		static void Main(string[] args)
		{
			// criara um ususario e senha que sera armazenado no bando de dados gerenciado pelo Identity
			var nomeUsuario = "gisa.cibele@hotmail.com";
			var senha = "Password 1234";

				var usuarioArmazenado = new UserStore<IdentityUser>();

			var usuarioGerenciador = new UserManager<IdentityUser>(usuarioArmazenado);
			IdentityUser objIdentityUser = new IdentityUser(nomeUsuario);
			var resultado = usuarioGerenciador.Create(objIdentityUser, senha);
			//var novo_resultado = usuarioGerenciador.Create(new IdentityUser(nomeUsuario, senha);
			Console.WriteLine("Status create {0}", resultado.Succeeded);
			Console.ReadLine();

			var identidadeUsuario = usuarioGerenciador.FindByName(nomeUsuario);

			usuarioGerenciador.AddClaim(identidadeUsuario.Id, new Claim("Nome_Usuario", "Gisele"));
			var validaSenha = usuarioGerenciador.CheckPassword(identidadeUsuario, senha);
			Console.WriteLine("Senha verificada: {0}", validaSenha);

			Console.ReadLine();
		}
	}
}
