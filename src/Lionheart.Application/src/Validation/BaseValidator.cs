using Lionheart.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lionheart.Application.Services
{
    public abstract class BaseValidator<TModel> : IValidationService<TModel> where TModel : class
    {
        protected readonly Dictionary<string, Func<TModel, IEnumerable<string>>> FieldValidators = new();

        protected BaseValidator()
        {
            AddFieldValidators();
        }

        public bool IsValid(TModel model) => ApplyShortCircuitValidationRules(model);

        public virtual Dictionary<string, IEnumerable<string>> Validate(TModel model)
        {
            return ApplyValidationRules(model);
        }

        protected abstract void AddFieldValidators();

        protected Dictionary<string, IEnumerable<string>> ApplyValidationRules(TModel model)
        {
            var entries = FieldValidators.Select(x =>
                new KeyValuePair<string, IEnumerable<string>>(x.Key, x.Value(model)));

            return new Dictionary<string, IEnumerable<string>>(entries.Where(x => x.Value.Any()));
        }

        protected virtual bool ApplyShortCircuitValidationRules(TModel model)
        {
            IEnumerable<Func<TModel, IEnumerable<string>>> validatorFunctions = FieldValidators.Select(x => x.Value);

            foreach (var validatorFunction in validatorFunctions)
            {
                if (validatorFunction(model).Any())
                {
                    return false;
                }
            }
            return true;
        }

        protected virtual void MergeValidationResults(
            Dictionary<string, IEnumerable<string>> validationResults,
            Dictionary<string, IEnumerable<string>> childValidationResults,
            string childPropertyName)
        {
            foreach (var result in childValidationResults)
            {
                validationResults.Add($"{childPropertyName}.{result.Key}", result.Value);
            }
        }
    }
}
