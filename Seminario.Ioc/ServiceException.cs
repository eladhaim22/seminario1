using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Seminario.Ioc
{
	[Serializable]
	public class ServiceException : Exception
	{
		public ServiceException()
			: base()
		{
		}

		public ServiceException(string message)
			: base(message, null)
		{
		}

		public ServiceException(string message, Exception innerException) :
			base(message, innerException)
		{
		}

		protected ServiceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}

			base.GetObjectData(info, context);
		}
	}
}
