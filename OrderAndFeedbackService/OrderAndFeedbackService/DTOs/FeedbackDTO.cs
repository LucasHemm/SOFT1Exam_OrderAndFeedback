namespace OrderAndFeedbackService.DTOs;

public class FeedbackDTO
{
    public int Id { get; set; }
    public OrderDTO OrderDTO{ get; set; }
    public String Title { get; set; }
    public String Description { get; set; }
    public int Agentrating { get; set; }
    public int RestaurantRating { get; set; }
    public int OverAllRating { get; set; }
}