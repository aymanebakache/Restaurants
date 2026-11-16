using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryValidator:AbstractValidator<GetAllRestaurantsQuery>
    {
        private int[] allowPageSizes = [5, 10, 15, 30];
        private string[] allowSortByColumnsNames = [nameof(RestaurantDto.Name), nameof(RestaurantDto.Description), nameof(RestaurantDto.Category)];
        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Must(value => allowPageSizes.Contains(value))
                                    .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");
            RuleFor(r => r.SortBy).Must(value => allowSortByColumnsNames.Contains(value))
                                    .When(q=>q.SortBy!=null)
                                    .WithMessage($"SortBy is optional or should choose between: [{string.Join(",", allowSortByColumnsNames)}]");
        }
    }
}
