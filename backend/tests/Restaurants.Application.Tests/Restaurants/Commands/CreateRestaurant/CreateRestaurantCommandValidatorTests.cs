using FluentValidation.TestHelper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidateCommand_ShouldNotHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Test",
                Category = "Italian",
                ContactEmail = "test@asp.net",
                PostalCode = "12-654"
            };
            var validator = new CreateRestaurantCommandValidator();
            //act
            var result = validator.TestValidate(command);
            //assert
            result.ShouldNotHaveAnyValidationErrors();

        }
        [Fact()]
        public void Validator_ForInvalidateCommand_ShouldHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "T",
                Category = "Portugeese",
                ContactEmail = "test.-asp.net",
                PostalCode = "12654"
            };
            var validator = new CreateRestaurantCommandValidator();
            //act
            var result = validator.TestValidate(command);
            //assert
            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Category);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);

        }

        [Theory()]
        [InlineData("Moroccan")]
        [InlineData("Spanish")]
        [InlineData("Italian")]
        [InlineData("American")]
        [InlineData("Japanese")]
        [InlineData("Mexican")]
        public void Validator_ForValidateCategory_ShouldNotHaveValidationErrorsForCategoryPropriety(string category)
        {
            //arrange
            //"Moroccan", "Italian", "Spanish", "Mexican", "Japanese", "American"
            var command = new CreateRestaurantCommand()
            {
                Category = category
            };
            var validator = new CreateRestaurantCommandValidator();
            //act
            var result = validator.TestValidate(command);
            //assert
            result.ShouldNotHaveValidationErrorFor(c => c.Category);

        }
    }
}
