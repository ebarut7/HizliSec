

using FluentValidation;
using HizliSec.Entities.Concrete;

namespace HizliSec.Business.ValidationRules.FluentValidation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x=>x.Name).MaximumLength(30);
        }
    }
}
