using OrderAndFeedbackService.DTOs;

namespace OrderAndFeedbackService.Models;

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public int CustomerId { get; set; }
    public int AgentId { get; set; }
    public int RestaurantId{ get; set; }
    public List<OrderLine> OrderLines { get; set; }
    public int PaymentId { get; set; }
    public int TotalPrice { get; set; }
    public String Status { get; set; }
    public String Receipt { get; set; }

    public Order()
    {
    }

    public Order(int id, string orderNumber, int customerId, int agentId, int restaurantId, List<OrderLine> orderLines, int paymentId, int totalPrice, string status, string receipt)
    {
        Id = id;
        OrderNumber = orderNumber;
        CustomerId = customerId;
        AgentId = agentId;
        RestaurantId = restaurantId;
        OrderLines = orderLines;
        PaymentId = paymentId;
        TotalPrice = totalPrice;
        Status = status;
        Receipt = receipt;
    }
    
    public Order(OrderDTO orderDto)
    {
        Id = orderDto.Id;
        OrderNumber = orderDto.OrderNumber ?? orderDto.Id.ToString();
        CustomerId = orderDto.CustomerId;
        AgentId = orderDto.AgentId;
        RestaurantId = orderDto.RestaurantId;
        OrderLines = orderDto.OrderLinesDTOs
            .Select(orderLineDto => new OrderLine(orderLineDto, this))
            .ToList();
        PaymentId = orderDto.PaymentId;
        TotalPrice = orderDto.TotalPrice;
        Status = orderDto.Status;
        Receipt = orderDto.Receipt;
    }

}