using Microsoft.EntityFrameworkCore;
using OrderAndFeedbackService;
using OrderAndFeedbackService.DTOs;
using OrderAndFeedbackService.Facades;
using OrderAndFeedbackService.Models;
using Testcontainers.MsSql;

namespace OrderAndFeedbackServiceTest;

public class IntegrationTests : IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest") // Use the correct SQL Server image
        .WithPassword("YourStrong!Passw0rd") // Set a strong password
        .Build();

    private string _connectionString;

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();

        // Create the connection string for the database
        _connectionString = _msSqlContainer.GetConnectionString();

        // Initialize the database context and apply migrations
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(_connectionString)
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.Migrate(); // Apply any pending migrations
        }
    }

    public async Task DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync().AsTask();
    }
    
    [Fact]
    public void ShouldCreateOrder()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(_connectionString)
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            OrderFacade orderFacade = new OrderFacade(context);
            OrderLineDTO orderLineDto1 = new OrderLineDTO(0, 1, 2, 3);
            OrderLineDTO orderLineDto2 = new OrderLineDTO(0, 1, 2, 3);
            List<OrderLineDTO> orderLines = new List<OrderLineDTO>();
            orderLines.Add(orderLineDto1);
            orderLines.Add(orderLineDto2);

            OrderDTO orderDto = new OrderDTO(0, 1, 2, 3, orderLines, 1, 1000, "Payed", "receipt");

            Order order = orderFacade.CreateOrder(orderDto);
            
            Order createdOrder = context.Orders.FirstOrDefault(o => o.OrderNumber == order.OrderNumber);

            Assert.NotNull(order);
            Assert.NotNull(createdOrder);
            Assert.Equal(createdOrder.TotalPrice, order.TotalPrice);
            Assert.Equal(orderDto.OrderNumber, order.OrderNumber);
        }
    }
    
    [Fact]
    public void ShouldUpdateOrderStatus()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(_connectionString)
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            OrderFacade orderFacade = new OrderFacade(context);
            OrderLineDTO orderLineDto1 = new OrderLineDTO(0, 1, 2, 3);
            OrderLineDTO orderLineDto2 = new OrderLineDTO(0, 1, 2, 3);
            List<OrderLineDTO> orderLines = new List<OrderLineDTO>();
            orderLines.Add(orderLineDto1);
            orderLines.Add(orderLineDto2);

            OrderDTO orderDto = new OrderDTO(0, 1, 2, 3, orderLines, 1, 1000, "Payed", "receipt");

            Order order = orderFacade.CreateOrder(orderDto);
            UpdateStatusDTO updateStatusDto = new UpdateStatusDTO(order.Id, "Delivered");
            Order updatedOrder = orderFacade.UpdateOrderStatus(updateStatusDto);
            
            Assert.NotNull(order);
            Assert.NotNull(updatedOrder);
            Assert.Equal(updatedOrder.Status, updateStatusDto.Status);
        }
    }
    
    [Fact]
    public void ShouldGetOrder()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(_connectionString)
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            OrderFacade orderFacade = new OrderFacade(context);
            OrderLineDTO orderLineDto1 = new OrderLineDTO(0, 1, 2, 3);
            OrderLineDTO orderLineDto2 = new OrderLineDTO(0, 1, 2, 3);
            List<OrderLineDTO> orderLines = new List<OrderLineDTO>();
            orderLines.Add(orderLineDto1);
            orderLines.Add(orderLineDto2);

            OrderDTO orderDto = new OrderDTO(0, 1, 2, 3, orderLines, 1, 1000, "Payed", "receipt");

            Order order = orderFacade.CreateOrder(orderDto);
            Order createdOrder = orderFacade.GetOrder(order.Id);
            
            Assert.NotNull(order);
            Assert.NotNull(createdOrder);
            Assert.Equal(createdOrder.Id, order.Id);
        }
    }
    
    [Fact]
    public void ShouldGetAllOrders()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(_connectionString)
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            OrderFacade orderFacade = new OrderFacade(context);
            OrderLineDTO orderLineDto1 = new OrderLineDTO(0, 1, 2, 3);
            OrderLineDTO orderLineDto2 = new OrderLineDTO(0, 1, 2, 3);
            List<OrderLineDTO> orderLines = new List<OrderLineDTO>();
            orderLines.Add(orderLineDto1);
            orderLines.Add(orderLineDto2);

            OrderDTO orderDto = new OrderDTO(0, 1, 2, 3, orderLines, 1, 1000, "Payed", "receipt");

            Order order = orderFacade.CreateOrder(orderDto);
            List<Order> orders = orderFacade.GetAllOrders();
            
            Assert.NotNull(order);
            Assert.NotNull(orders);
            Assert.NotEmpty(orders);
            Assert.Contains(order, orders);
        }
    }
}