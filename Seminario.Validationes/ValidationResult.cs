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
			this.ErrorMessages = new List<string>();
		}

		public bool IsValid
		{
			get { return this.ErrorMessages.Count == 0; }
		}

		public IList<string> ErrorMessages
		{
			get;
			private set;
		}
	}
}
