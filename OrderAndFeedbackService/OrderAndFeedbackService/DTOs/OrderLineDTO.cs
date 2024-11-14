using OrderAndFeedbackService.Models;

namespace OrderAndFeedbackService.DTOs;

public class OrderLineDTO
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }


    public OrderLineDTO()
    {
    }

    public OrderLineDTO(int id, int orderId, int quantity)
    {
        Id = id;
        OrderId = orderId;
        Quantity = quantity;
    }
    
    public OrderLineDTO(OrderLine orderLine)
    {
        Id = orderLine.Id;
        OrderId = orderLine.Order.Id;
        Quantity = orderLine.Quantity;
    }
}