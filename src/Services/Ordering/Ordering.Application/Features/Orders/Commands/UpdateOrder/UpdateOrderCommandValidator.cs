using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        // Pre Processor Behavior -< before coming to Handle method
        public UpdateOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                    .NotEmpty().WithMessage("{UserName} is required.")
                    .NotNull()
                    .MaximumLength(50).WithMessage("{UserName} mus not exceed 50 characters");

            RuleFor(p => p.EmailAddress)
                    .NotEmpty().WithMessage("{EmailAdress} is required.");

            RuleFor(p => p.TotalPrice)
                    .NotEmpty().WithMessage("{TotalPrice} is required.")
                    .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero.");


        }
    }
}
