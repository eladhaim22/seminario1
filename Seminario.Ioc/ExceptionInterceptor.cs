using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security;
using Microsoft.Practices.Unity.InterceptionExtension;
using Seminario.Validationes;

namespace Seminario.Ioc
{
	internal class ExceptionInterceptor : IInterceptionBehavior
	{
		public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
		{
			try
			{
				var result = getNext()(input, getNext);
				return result;
			}
			catch (ValidationException ex)
			{
				throw new ServiceValidationException(ex.ValidationResult);
			}
			catch (ServiceException ex)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new ServiceException("Unexpected service error", ex);
			}
		}

		public IEnumerable<Type> GetRequiredInterfaces()
		{
			return Type.EmptyTypes;
		}

		public bool WillExecute
		{
			get { return true; }
		}
	}
}
