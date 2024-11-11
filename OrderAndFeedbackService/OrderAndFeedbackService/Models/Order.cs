namespace OrderAndFeedbackService.Models;

public class Order
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public int CustomerId { get; set; }
    public int AgentId { get; set; }
    public int RestaurantId{ get; set; }
    public List<OrderLine> OrderLines { get; set; }
    public int PaymentId { get; set; }
    public int TotalPrice { get; set; }
    public String Status { get; set; }
    public String Receipt { get; set; }
}