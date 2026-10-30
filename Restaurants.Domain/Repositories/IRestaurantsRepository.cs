using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
    }
}
