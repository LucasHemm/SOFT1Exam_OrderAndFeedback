using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderAndFeedbackService;
using OrderAndFeedbackService.DTOs;
using OrderAndFeedbackService.Facades;
using OrderAndFeedbackService.Models;
using Testcontainers.MsSql;

namespace OrderAndFeedbackServiceTest;

public class OrderApiTests : IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer;
    private readonly WebApplicationFactory<Program> _factory;
    private HttpClient _client;

    public OrderApiTests()
    {
        _msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("YourStrong!Passw0rd")
            .WithCleanUp(true)
            .Build();

        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer(_msSqlContainer.GetConnectionString());
                    });

                    var sp = services.BuildServiceProvider();
                    using (var scope = sp.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        db.Database.Migrate();
                    }
                });
            });
    }
    
    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
        await Task.Delay(500); // Optional delay for container initialization
        _client = _factory.CreateClient();
    }
    
    public async Task DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync().AsTask();
    }
    
    [Fact]
    public async Task ShouldCreateOrder()
    {
        var orderLines = new List<OrderLineDTO>
        {
            new OrderLineDTO(0, 1, 2, 3),
            new OrderLineDTO(0, 1, 2, 3)
        };

        var orderDto = new OrderDTO(0, 1, 2, 3, orderLines, 1, 1000, "Payed", "receipt");

        var response = await _client.PostAsJsonAsync("/api/OrderApi", orderDto);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        Assert.Equal("Order created successfully", result);
    }

    [Fact]
    public async Task ShouldGetOrderById()
    {
        var orderLines = new List<OrderLineDTO>
        {
            new OrderLineDTO(0, 1, 2, 3),
            new OrderLineDTO(0, 1, 2, 3)
        };

        var orderDto = new OrderDTO(0, 1, 2, 3, orderLines, 1, 1000, "Payed", "receipt");

        var response = await _client.PostAsJsonAsync("/api/OrderApi", orderDto);
        response.EnsureSuccessStatusCode();
        
        var createdOrder = await _client.GetFromJsonAsync<Order>($"/api/OrderApi/1");
        
        Assert.NotNull(createdOrder);
        Assert.Equal(1, createdOrder.Id);
        
    }
    
    [Fact]
    public async Task ShouldUpdateOrderStatus()
    {
        var orderLines = new List<OrderLineDTO>
        {
            new OrderLineDTO(0, 1, 2, 3),
            new OrderLineDTO(0, 1, 2, 3)
        };
    
        var orderDto = new OrderDTO(0, 1, 2, 3, orderLines, 1, 1000, "Payed", "receipt");
    
        var response = await _client.PostAsJsonAsync("/api/OrderApi", orderDto);
       
        var createdOrder = await _client.GetFromJsonAsync<Order>($"/api/OrderApi/1");
        Assert.NotNull(createdOrder);
        Assert.Equal("Payed", createdOrder.Status);
    
        var updateStatusDto = new UpdateStatusDTO(createdOrder.Id, "Delivered");
        
    
        var updateResponse = await _client.PutAsJsonAsync("/api/OrderApi", updateStatusDto);
        updateResponse.EnsureSuccessStatusCode();
        
        var updatedOrder = await _client.GetFromJsonAsync<Order>($"/api/OrderApi/1");
        Assert.NotNull(updatedOrder);
        Assert.Equal("Delivered", updatedOrder.Status);
    }
    
    [Fact]
    public async Task ShouldGetAllOrders()
    {
        var orderLines = new List<OrderLineDTO>
        {
            new OrderLineDTO(0, 1, 2, 3),
            new OrderLineDTO(0, 1, 2, 3)
        };

        var orderDto = new OrderDTO(0, 1, 2, 3, orderLines, 1, 1000, "Payed", "receipt");

        var response = await _client.PostAsJsonAsync("/api/OrderApi", orderDto);
        response.EnsureSuccessStatusCode();
        
        var allOrders = await _client.GetFromJsonAsync<List<Order>>("/api/OrderApi");
        
        Assert.NotNull(allOrders);
        Assert.NotEmpty(allOrders);
    }
}
