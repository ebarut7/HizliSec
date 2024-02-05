using FluentValidation;
using HizliSec.Entities.Concrete;
namespace HizliSec.Business.ValidationRules.FluentValidation
{
    public class OrderDetailValidator : AbstractValidator<OrderDetail>
    {
        public OrderDetailValidator()
        {
            RuleFor(x=>x.Price).GreaterThanOrEqualTo(1);
            RuleFor(x=>x.Quantity).GreaterThanOrEqualTo(1);
        }
    }
}
