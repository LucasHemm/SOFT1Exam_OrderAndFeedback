using Microsoft.EntityFrameworkCore;
using OrderAndFeedbackService;
using OrderAndFeedbackService.Facades;

namespace OrderAndFeedbackServiceTest;

public class UnitTests
{
    
    private readonly OrderFacade _orderFacade;
    public UnitTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var context = new ApplicationDbContext(options);
        _orderFacade = new OrderFacade(context);
    }

    
    [Fact]
    public void ShouldCreateOrderNumber()
    {
        string res = _orderFacade.CreateOrderNumber(1, 2, 3);
        Assert.NotNull(res);
        Assert.Contains("a1r2u3", res);
    }
}