using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.Validationes
{
	public class ValidationResult
	{
		public ValidationResult()
		{
			this.Errors = new List<ValidationError>();
		}

		public bool IsValid
		{
			get { return this.Errors.Count == 0; }
		}

		public ValidationResult(string errorMessage)
			: this(new ValidationError { PropertyKey = string.Empty, Message = errorMessage })
		{
		}

		public ValidationResult(string propertyKey, string errorMessage)
			: this(new ValidationError { PropertyKey = propertyKey, Message = errorMessage })
		{
		}

		public ValidationResult(ValidationError error)
			: this()
		{
			if (error == null)
			{
				throw new ArgumentNullException("error");
			}
			this.Errors.Add(error);
		}

		public IList<ValidationError> Errors
		{
			get;
			private set;
		}
	}
}
