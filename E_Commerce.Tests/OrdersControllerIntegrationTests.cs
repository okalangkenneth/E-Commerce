using E_Commerce.Models.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace E_Commerce.Tests
{
    public class OrdersControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public OrdersControllerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetOrder_ReturnsCorrectResponse_WhenOrderExists()
        {
            // Arrange
            var client = _factory.CreateClient();
            var orderId = 1;

            // Act
            var response = await client.GetAsync($"/api/orders/{orderId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var returnedOrder = JsonConvert.DeserializeObject<OrderDto>(await response.Content.ReadAsStringAsync());
            Assert.Equal(orderId, returnedOrder.Id);
        }

        [Fact]
        public async Task GetOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var orderId = -1; // Nonexistent ID

            // Act
            var response = await client.GetAsync($"/api/orders/{orderId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

}
