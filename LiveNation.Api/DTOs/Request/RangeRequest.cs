using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LiveNation.Api.DTOs.Request
{
    [ModelBinder(BinderType = typeof(RangeRequestBinder))]
    public class RangeRequest : IValidatableObject
    {
        public int Start { get; set; }
        public int End { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Start < End)
            {
                yield return new ValidationResult("The start of the range cannot be greater than the end of the range.");
            }
        }
    }

    public class RangeRequestBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var startString = bindingContext.ValueProvider.GetValue("start").FirstValue;
            var endString = bindingContext.ValueProvider.GetValue("end").FirstValue;

            var model = new RangeRequest()
            {
                Start = int.Parse(startString),
                End = int.Parse(endString),
            };

            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }
    }
}
