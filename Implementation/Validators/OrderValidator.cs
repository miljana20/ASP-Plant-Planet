using Application.UseCases.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class OrderValidator : AbstractValidator<OrderDto>
    {
        public OrderValidator()
        {
            RuleFor(x => x.Order).NotEmpty()
                                 .WithMessage("Order status is required")
                                 .Equal(true)
                                 .WithMessage("Invalid value");
        }
    }
}
