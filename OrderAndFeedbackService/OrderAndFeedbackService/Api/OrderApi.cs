using Microsoft.AspNetCore.Mvc;
using OrderAndFeedbackService.DTOs;
using OrderAndFeedbackService.Facades;
using OrderAndFeedbackService.Models;

namespace OrderAndFeedbackService.Api;
[ApiController]
[Route("api/[controller]")]

public class OrderApi : ControllerBase
{
    private readonly OrderFacade _orderFacade;
    private readonly IMessagePublisher _messagePublisher;

    
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
            // Perform your business logic
            OrderDTO updatedOrder = new OrderDTO(_orderFacade.UpdateOrderStatus(orderDto));
            
            

            // Construct the email message
            var emailMessage = new EmailMessage
            {
                ToEmail = updatedOrder
                Subject = "Your Order Status Has Been Updated",
                Content = $"Dear {updatedOrder.Customer.Name},\n\nYour order status has been updated to {updatedOrder.Status}.\n\nThank you for shopping with us!"
            };

            // Publish the email message to the RabbitMQ queue
            _messagePublisher.PublishEmailMessage(emailMessage);

            return Ok(updatedOrder);
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