using OrderAndFeedbackService.Models;

namespace OrderAndFeedbackService.DTOs;

public class OrderLineDTO
{
    public int Id { get; set; }
    public int MenuItemId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }


    public OrderLineDTO()
    {
    }

    public OrderLineDTO(int id, int menuItemId,int orderId, int quantity)
    {
        Id = id;
        MenuItemId = menuItemId;
        OrderId = orderId;
        Quantity = quantity;
    }
    
    public OrderLineDTO(OrderLine orderLine)
    {
        Id = orderLine.Id;
        MenuItemId = orderLine.MenuItemId;
        OrderId = orderLine.Order.Id;
        Quantity = orderLine.Quantity;
    }
}