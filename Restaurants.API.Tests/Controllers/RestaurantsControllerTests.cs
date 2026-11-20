using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Restaurants.API.Tests.Controllers
{
    public class RestaurantsControllerTests:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IRestaurantsRepository> _restaurantRepositoryMock = new();
        
        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
                                builder.ConfigureTestServices(services =>
                                {
                                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                                    services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository), _ => _restaurantRepositoryMock.Object));
                                }
                                ));
        }
        [Fact()]
        public async Task GetById_ForNonExistingId_Returns404NotFound()
        {
            //arrange
            var id = 1123;
            _restaurantRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync((Restaurant?)null);
            var client = _factory.CreateClient();

            //act
            var result = await client.GetAsync($"/api/restaurants/{id}");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
        [Fact()]
        public async Task GetById_ForExistingId_Returns200Ok()
        {
            //arrange
            var id = 1123;
            var restaurant = new Restaurant()
            {
                Id = id,
                Name = "Test",
                Description = "Test description"
            };
            _restaurantRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync(restaurant);
            var client = _factory.CreateClient();

            //act
            var result = await client.GetAsync($"/api/restaurants/{id}");
            var restaurantDto = await result.Content.ReadFromJsonAsync<RestaurantDto>();

            //assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            restaurantDto.Should().NotBeNull();
            restaurantDto.Name.Should().Be("Test");
            restaurantDto.Description.Should().Be("Test description");
        }
        [Fact()]
        public async Task GetAll_ForValidRequest_Returns200Ok()
        {
            //arrange
            var client = _factory.CreateClient();

            //act
            var result = await client.GetAsync("/api/restaurants?pageNumber=1&pageSize=10");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact()]
        public async Task GetAll_ForInvalidRequest_Returns400BadRequest()
        {
            //arrange
            var client = _factory.CreateClient();

            //act
            var result = await client.GetAsync("/api/restaurants");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
