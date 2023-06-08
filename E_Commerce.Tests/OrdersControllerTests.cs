using AutoMapper;
using E_Commerce.Controllers;
using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace E_Commerce.Tests
{
    public class OrdersControllerTests : IDisposable
    {
        private readonly OrdersController _controller;
        private readonly ECommerceContext _context;
        private readonly OrderReadContext _readContext;
        private readonly Mock<ILogger<OrdersController>> _mockLogger;
        private readonly Mock<IMapper> _mapper;

        public OrdersControllerTests()
        {
            var options = new DbContextOptionsBuilder<ECommerceContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique in-memory database for each test
            .Options;

            var readOptions = new DbContextOptionsBuilder<OrderReadContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique in-memory database for each test
            .Options;

            _context = new ECommerceContext(options);
            _readContext = new OrderReadContext(readOptions);

            _mockLogger = new Mock<ILogger<OrdersController>>();
            _mapper = new Mock<IMapper>();

            _controller = new OrdersController(_context, _readContext, _mockLogger.Object, _mapper.Object);

            _readContext.Orders.Add(new Order { Id = 1 });
            _readContext.SaveChanges();

            var orderDto = new OrderDto { Id = 1 };
            _mapper.Setup(m => m.Map<OrderDto>(It.IsAny<Order>())).Returns(orderDto);

            Assert.Equal(1, _readContext.Orders.Count()); // Check that the order has been added to the context
        }

        public void Dispose()
        {
            // Dispose the _context and _readContext objects
            _context.Dispose();
            _readContext.Dispose();
        }




        [Fact]
        public async Task GetOrder_ReturnsCorrectOrder_WhenOrderExists()
        {
            // Arrange
            var expectedOrderId = 1;

            // Act
            var result = await _controller.GetOrder(expectedOrderId);

            // Debugging lines:
            Assert.NotNull(result); // Check that result is not null
            Assert.NotNull(result.Result); // Check that result.Result is not null

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<OrderDto>(okResult.Value);
            Assert.Equal(expectedOrderId, returnValue.Id);
        }

        [Fact]
        public async Task GetOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = 4; // This order ID does not exist in the mock data

            // Act
            var result = await _controller.GetOrder(orderId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}


