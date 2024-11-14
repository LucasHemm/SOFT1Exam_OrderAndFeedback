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
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest") // Use the correct SQL Server image
            .WithPassword("YourStrong!Passw0rd") // Set a strong password
            .WithCleanUp(true)
            .Build();

        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the existing ApplicationDbContext registration
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                             typeof(DbContextOptions<ApplicationDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    // Add ApplicationDbContext using the test container's connection string
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer(_msSqlContainer.GetConnectionString());
                    });

                    // Ensure the database is created and migrations are applied
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
        _client = _factory.CreateClient();
    }
    
    public async Task DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync().AsTask();
    }
    
    [Fact]
    public async Task ShouldCreateOrder()
    {
        var orderLineDto1 = new OrderLineDTO(0, 1, 2, 3);
        var orderLineDto2 = new OrderLineDTO(0, 1, 2, 3);
        var orderLines = new List<OrderLineDTO> {orderLineDto1, orderLineDto2};

        var orderDto = new OrderDTO(0, 1, 2, 3, orderLines, 1, 1000, "Payed", "receipt");

        var response = await _client.PostAsJsonAsync("/api/OrderApi", orderDto);
        response.EnsureSuccessStatusCode();

        var order = await response.Content.ReadFromJsonAsync<Order>();
        Assert.NotNull(order);
        Assert.Equal(orderDto.TotalPrice, order.TotalPrice);
        Assert.Equal(orderDto.OrderNumber, order.OrderNumber);
        
    }
        
}