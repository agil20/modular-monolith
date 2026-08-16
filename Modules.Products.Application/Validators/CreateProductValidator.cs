using FluentValidation;
using Modules.Products.Contract.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Products.Validators
{
    public class CreateProductValidator: AbstractValidator<RequestProductCreate>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
        }
    }
}
