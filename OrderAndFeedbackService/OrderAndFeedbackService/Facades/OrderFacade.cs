using Microsoft.EntityFrameworkCore;
using OrderAndFeedbackService.DTOs;
using OrderAndFeedbackService.Models;

namespace OrderAndFeedbackService.Facades;

public class OrderFacade
{
    private readonly ApplicationDbContext _context;

    public OrderFacade(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public Order CreateOrder(OrderDTO orderDto)
    {
        orderDto.OrderNumber = CreateOrderNumber(orderDto.AgentId, orderDto.RestaurantId, orderDto.CustomerId);
        Order order = new Order(orderDto);
        _context.Orders.Add(order);
        _context.SaveChanges();
        return order;
    }
    
    public Order GetOrder(int id)
    {
        Order order = _context.Orders
            .Include(order => order.OrderLines)
            .FirstOrDefault(order => order.Id == id);
        if (order == null)
        {
            throw new Exception("Order not found");
        }
        return order;
    }
    
    public Order UpdateOrderStatus(UpdateStatusDTO orderDto)
    {
        Order order = GetOrder(orderDto.OrderId);
        order.Status = orderDto.Status;
        _context.SaveChanges();
        return order;
    }


    public string CreateOrderNumber(int agentId, int restaurantId, int userId)
    {
        return DateTime.Now.ToString("yyyyMMddHHmmss")+new Random().Next(1000,9999)+"a"+agentId+"r"+restaurantId+"u"+userId;
    }
    
}
