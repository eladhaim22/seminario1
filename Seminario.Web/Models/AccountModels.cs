using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using Seminario.Model;

namespace Seminario.Web.Models
{
	public class UsersContext : DbContext
	{
		public UsersContext()
			: base("MyContext")
		{
		}

		public DbSet<Empleado> Empleado { get; set; }
	}

	public class RegisterExternalLoginModel
	{
		[Required]
		[Display(Name = "Nro Legajo")]
		public string Legajo { get; set; }

		public string ExternalLoginData { get; set; }
	}

	public class LocalPasswordModel
	{
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Current password")]
		public string OldPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "El {0} debe ser al menos de {2} caracteres.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "New password")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm new password")]
		[Compare("NewPassword", ErrorMessage = "La nueva contraseña y la de confirmación no coinciden.")]
		public string ConfirmPassword { get; set; }
	}

	public class LoginModel
	{
		[Required]
		[Display(Name = "Nro Legajo")]
		public string Legajo { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}

	public class RegisterModel
	{
		[Required]
		[Display(Name = "Nro Legajo")]
		public string Legajo { get; set; }

		[Required]
        [StringLength(100, ErrorMessage = "El {0} debe ser al menos de {2} caracteres.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "La nueva contraseña y la de confirmación no coinciden.")]
		public string ConfirmPassword { get; set; }
	}

	public class ExternalLogin
	{
		public string Provider { get; set; }
		public string ProviderDisplayName { get; set; }
		public string ProviderUserId { get; set; }
	}
}
