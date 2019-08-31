using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Impacta.autenticacao.MVC.Models
{
	public class Usuario : IUser
	{
		public string Id { get; set; }
		[Required]
		
		public string UserName { get; set; }

		[Required]
		[MaxLength(8, ErrorMessage ="Maximo de 8 caracteres")]
		public string Passoword { get; set; }
	}
}