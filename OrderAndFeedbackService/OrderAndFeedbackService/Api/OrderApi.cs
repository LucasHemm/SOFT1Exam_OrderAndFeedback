using Microsoft.AspNetCore.Mvc;
using OrderAndFeedbackService.DTOs;
using OrderAndFeedbackService.Facades;

namespace OrderAndFeedbackService.Api;
[ApiController]
[Route("api/[controller]")]

public class OrderApi : ControllerBase
{
    private readonly OrderFacade _orderFacade;
    
    public OrderApi(OrderFacade orderFacade)
    {
        _orderFacade = orderFacade;
    }
    
    // POST: api/Order
    [HttpPost]
    public IActionResult CreateOrder([FromBody] OrderDTO orderDto)
    {
        try
        {
            _orderFacade.CreateOrder(orderDto);
            return Ok("Order created successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    
}