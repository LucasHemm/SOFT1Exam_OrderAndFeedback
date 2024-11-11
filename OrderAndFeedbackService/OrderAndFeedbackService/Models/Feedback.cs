namespace OrderAndFeedbackService.Models;

public class Feedback
{
    public int Id { get; set; }
    public Order Order{ get; set; }
    public String Title { get; set; }
    public String Description { get; set; }
    public int Agentrating { get; set; }
    public int RestaurantRating { get; set; }
    public int OverAllRating { get; set; }
    
}