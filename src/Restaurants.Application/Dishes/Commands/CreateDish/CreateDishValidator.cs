using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishValidator:AbstractValidator<CreateDishCommand>
    {
        public CreateDishValidator()
        {
            RuleFor(dish => dish.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be a non-negative number");
            RuleFor(dish => dish.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Calories must be a non-negative number");
        }
    }
}
