namespace OrderAndFeedbackService.Models;

public class OrderLine
{
    public int Id { get; set; }
    public Order Order { get; set; }
    public int Quantity { get; set; }
}