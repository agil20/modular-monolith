using FluentValidation;
using Modules.Categories.Contract.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Categories.Validators
{
    public class CreateCategoryValidator:AbstractValidator<RequestCategoryCreate>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
        }
    }
}
