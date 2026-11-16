using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Restaurants.Application.Common;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantsDbContext restaurantsDbContext) : IRestaurantsRepository
    {
        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await restaurantsDbContext.Restaurants.ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await restaurantsDbContext.Restaurants
                                                       .Include(r => r.Dishes)
                                                       .FirstOrDefaultAsync(r => r.Id == id);
            return restaurant;
        }

        public async Task<int> Create(Restaurant entity)
        {
            restaurantsDbContext.Restaurants.Add(entity);
            await restaurantsDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(Restaurant restaurant)
        {
            restaurantsDbContext.Remove(restaurant);
            await restaurantsDbContext.SaveChangesAsync();
        }
        public async Task SaveChanges() => await restaurantsDbContext.SaveChangesAsync();

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();
            var queryBase = restaurantsDbContext.Restaurants
                                                 .Where(r => searchPhraseLower == null ||
                                                            r.Name.ToLower().Contains(searchPhraseLower) ||
                                                            r.Description.ToLower().Contains(searchPhraseLower)
                                                        );
            var totalCount = await queryBase.CountAsync();
            if (sortBy != null)
            {
                var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name),r=>r.Name },
                    {nameof(Restaurant.Description),r=>r.Description },
                    {nameof(Restaurant.Category),r=>r.Category }
                };
                var selectedColumn = columnSelector[sortBy];
                queryBase = sortDirection == SortDirection.Ascending ? queryBase.OrderBy(selectedColumn) : queryBase.OrderByDescending(selectedColumn);
            }
            var restaurants = await queryBase.Skip(pageSize * (pageNumber - 1))
                                           .Take(pageSize)
                                           .ToListAsync();
            return (restaurants, totalCount);

        }

    }
}
