using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Seminario.Model;

namespace Seminario.Validationes
{
	public interface IEntityValidator
	{
		ValidationResult Validate(IEntity entity);
	}

	public interface IEntityValidator<TEntity> : IEntityValidator where TEntity : class, IEntity
	{
		ValidationResult Validate(TEntity entity);
	}

	public abstract class EntityValidator<TEntity> : IEntityValidator<TEntity>
		where TEntity : class, IEntity
	{
		private readonly AbstractValidator<TEntity> fluentValidator;

		protected EntityValidator()
		{
			this.fluentValidator = new FluentValidatorImpl();
		}

		protected AbstractValidator<TEntity> FluentValidator
		{
			get { return this.fluentValidator; }
		}

		public ValidationResult Validate(IEntity entity)
		{
			return this.Validate(entity as TEntity);
		}

		public virtual Seminario.Validationes.ValidationResult Validate(TEntity entity)
		{
			var result = new Seminario.Validationes.ValidationResult();

			foreach (ValidationFailure item in this.FluentValidator.Validate(entity).Errors)
			{
				result.Errors.Add(new ValidationError { PropertyKey = item.PropertyName, Message = item.ErrorMessage });
			}
			return result;
		}

		public void AddRule(IValidationRule rule)
		{
			this.FluentValidator.AddRule(rule);
		}

		public void Custom(Func<TEntity, ValidationContext<TEntity>, ValidationFailure> customValidator)
		{
			this.FluentValidator.Custom(customValidator);
		}

		public void Custom(Func<TEntity, ValidationFailure> customValidator)
		{
			this.FluentValidator.Custom(customValidator);
		}

		public IRuleBuilderInitial<TEntity, TProperty> RuleFor<TProperty>(Expression<Func<TEntity, TProperty>> expression)
		{
			return this.FluentValidator.RuleFor(expression);
		}

		public void RuleSet(string ruleSetName, Action action)
		{
			this.FluentValidator.RuleSet(ruleSetName, action);
		}

		public void Unless(Func<TEntity, bool> predicate, Action action)
		{
			this.FluentValidator.Unless(predicate, action);
		}

		public void When(Func<TEntity, bool> predicate, Action action)
		{
			this.FluentValidator.When(predicate, action);
		}

		private class FluentValidatorImpl : AbstractValidator<TEntity>
		{
		}
	}
}
