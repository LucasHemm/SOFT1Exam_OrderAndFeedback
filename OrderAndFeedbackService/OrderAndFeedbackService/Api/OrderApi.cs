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
            OrderDTO createdOrder = new OrderDTO(_orderFacade.CreateOrder(orderDto));
            return Ok(createdOrder);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // PUT: api/Order
    [HttpPut]
    public IActionResult UpdateOrderStatus([FromBody] UpdateStatusDTO orderDto)
    {
        try
        {
            OrderDTO updateOrderStatus = new OrderDTO(_orderFacade.UpdateOrderStatus(orderDto));
            return Ok(updateOrderStatus);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // GET: api/Order/{id}
    [HttpGet("{id}")]
    public IActionResult GetOrder(int id)
    {
        try
        {
            return Ok(new OrderDTO(_orderFacade.GetOrder(id)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // GET: api/Order
    [HttpGet]
    public IActionResult GetAllOrders()
    {
        try
        {
            return Ok(_orderFacade.GetAllOrders().Select(order => new OrderDTO(order)).ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    
}