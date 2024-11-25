using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrderAndFeedbackService.DTOs;
using OrderAndFeedbackService.Facades;
using OrderAndFeedbackService.Models;
using OrderAndFeedbackService.Services;

namespace OrderAndFeedbackService.Api;
[ApiController]
[Route("api/[controller]")]

public class OrderApi : ControllerBase
{
    private readonly OrderFacade _orderFacade;
    private readonly IMessagePublisher _messagePublisher;
    private  readonly HttpClient _httpClient = new HttpClient();

    
    public OrderApi(OrderFacade orderFacade, IMessagePublisher messagePublisher)
    {
        _orderFacade = orderFacade;
        _messagePublisher = messagePublisher;
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
    public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateStatusDTO orderDto)
    {
        try
        {
            // Perform your business logic
            OrderDTO updatedOrder = new OrderDTO(_orderFacade.UpdateOrderStatus(orderDto));
            
            var response = await _httpClient.GetAsync("http://localhost:5042/api/customerapi/" + updatedOrder.CustomerId);
            // //from the response, get the user email from the json without using any object, just get the email value
            var json = await response.Content.ReadAsStringAsync();
            String email = JObject.Parse(json)["email"].ToString();
            String content = "";
            if (orderDto.Status.Equals("Delivered"))
            {
                content = "Dear Customer, Your order status has been updated to " + updatedOrder.Status + ". Please rate your agent through the MTOGO app - MTOGO";
            }
            else
            {
                content = "Dear Customer, Your order status has been updated to " + updatedOrder.Status + " - MTOGO";
                
            }
            
            
            // Construct the email message  
            EmailMessage emailMessage = new EmailMessage(email, "Your Order Status Has Been Updated", content);
            
            
            // Publish the email message to the RabbitMQ queue
            try
            {

                _messagePublisher.PublishEmailMessage(emailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }

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