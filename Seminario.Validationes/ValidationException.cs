using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Seminario.Validationes
{
	/// <summary>
	/// Exception throw on when validation errors are found saving an entity
	/// </summary>
	[Serializable]
	public class ValidationException : Exception
	{
		/// <summary>
		/// The separator used for serializing and deserializing the validation result.
		/// </summary>
		private const string Separator = ">|<eafb4efaa5194edba87d0703a674329b>|<";

		/// <summary>
		/// The validation result containing the errors that caused the exception.
		/// </summary>
		private readonly ValidationResult validationResult;

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationException"/> class.
		/// </summary>
		public ValidationException()
			: this(new ValidationResult())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public ValidationException(string message)
			: this(message, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public ValidationException(string message, Exception innerException) :
			this(new ValidationResult(message), message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationException"/> class.
		/// </summary>
		/// <param name="validationResult">The validation result that is the cause of the current exception.</param>
		public ValidationException(ValidationResult validationResult)
			: this(validationResult, validationResult == null ? string.Empty : string.Join("\n", validationResult.Errors.Select(e => e.Message)))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationException"/> class.
		/// </summary>
		/// <param name="validationResult">The validation result that is the cause of the current exception.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public ValidationException(ValidationResult validationResult, string message)
			: this(validationResult, message, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationException"/> class.
		/// </summary>
		/// <param name="validationResult">The validation result that is the cause of the current exception.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public ValidationException(ValidationResult validationResult, string message, Exception innerException)
			: base(message, innerException)
		{
			if (validationResult == null)
			{
				throw new ArgumentNullException("validationResult");
			}

			this.validationResult = validationResult;
		}

		/// <summary>Initializes a new instance of the <see cref="ValidationException"/> class with serialized data.</summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
		/// <exception cref="System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
		/// <exception cref="SerializationException">The class name is null or <see cref="HResult"/> is zero (0).</exception>
		protected ValidationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}

			this.validationResult = new ValidationResult();

			var properties = info.GetString("ValidationResultErrorProperties").Split(new[] { Separator }, int.MaxValue, StringSplitOptions.None);
			var messages = info.GetString("ValidationResultErrorMessages").Split(new[] { Separator }, int.MaxValue, StringSplitOptions.None);

			for (int index = 0; index < properties.Length; ++index)
			{
				this.validationResult.Errors.Add(new ValidationError { PropertyKey = properties[index], Message = messages[index] });
			}
		}

		/// <summary>
		/// Gets the validation result containing the errors that caused the exception.
		/// </summary>
		public ValidationResult ValidationResult
		{
			get { return this.validationResult; }
		}

		/// <summary>When overridden in a derived class, sets the <see cref="SerializationInfo"/> with information about the exception.</summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic).</exception>
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}

			base.GetObjectData(info, context);

			info.AddValue("ValidationResultErrorProperties", string.Join(Separator, this.validationResult.Errors.Select(e => e.PropertyKey)));
			info.AddValue("ValidationResultErrorMessages", string.Join(Separator, this.validationResult.Errors.Select(e => e.Message)));
		}
	}
}
