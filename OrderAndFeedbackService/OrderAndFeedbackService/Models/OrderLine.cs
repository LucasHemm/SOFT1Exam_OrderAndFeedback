using OrderAndFeedbackService.DTOs;

namespace OrderAndFeedbackService.Models;

public class OrderLine
{
    public int Id { get; set; }
    public int MenuItemId { get; set; }
    public Order Order { get; set; }
    public int Quantity { get; set; }

    public OrderLine()
    {
    }

    public OrderLine(int id, int menuItemId,Order order, int quantity)
    {
        Id = id;
        MenuItemId = menuItemId;
        Order = order;
        Quantity = quantity;
    }

    public OrderLine(OrderLineDTO orderLineDto, Order order)
    {
        Id = orderLineDto.Id;
        MenuItemId = orderLineDto.MenuItemId;
        Quantity = orderLineDto.Quantity;
        this.Order = order;
    }
}