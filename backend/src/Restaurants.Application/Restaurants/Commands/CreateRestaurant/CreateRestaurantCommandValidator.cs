using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidator:AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategories = new(){ "Moroccan", "Italian", "Spanish", "Mexican", "Japanese", "American" };
        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
            .Length(3, 100);

            RuleFor(dto => dto.Category)
            .Must(validCategories.Contains)
            .WithMessage("Invalid Category,Please choose from valid categories");
            RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");
            RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide a valid postal code (**-***)");

        }
    }
}
