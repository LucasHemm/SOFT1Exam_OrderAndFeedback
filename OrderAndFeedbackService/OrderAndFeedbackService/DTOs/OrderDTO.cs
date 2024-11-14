using OrderAndFeedbackService.Models;

namespace OrderAndFeedbackService.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public int CustomerId { get; set; }
    public int AgentId { get; set; }
    public int RestaurantId{ get; set; }
    public List<OrderLineDTO> OrderLinesDTOs { get; set; }
    public int PaymentId { get; set; }
    public int TotalPrice { get; set; }
    public String Status { get; set; }
    public String Receipt { get; set; }


    public OrderDTO()
    {
    }

    public OrderDTO(int id, int orderNumber, int customerId, int agentId, int restaurantId, List<OrderLineDTO> orderLinesDtOs, int paymentId, int totalPrice, string status, string receipt)
    {
        Id = id;
        OrderNumber = orderNumber;
        CustomerId = customerId;
        AgentId = agentId;
        RestaurantId = restaurantId;
        OrderLinesDTOs = orderLinesDtOs;
        PaymentId = paymentId;
        TotalPrice = totalPrice;
        Status = status;
        Receipt = receipt;
    }
    
    public OrderDTO(Order order)
    {
        Id = order.Id;
        OrderNumber = order.OrderNumber;
        CustomerId = order.CustomerId;
        AgentId = order.AgentId;
        RestaurantId = order.RestaurantId;
        OrderLinesDTOs = order.OrderLines
            .Select(orderLine => new OrderLineDTO(orderLine))
            .ToList();
        PaymentId = order.PaymentId;
        TotalPrice = order.TotalPrice;
        Status = order.Status;
        Receipt = order.Receipt;
    }
}