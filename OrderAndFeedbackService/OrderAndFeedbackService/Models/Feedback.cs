using OrderAndFeedbackService.DTOs;

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

    public Feedback()
    {
    }

    public Feedback(int id, Order order, string title, string description, int agentrating, int restaurantRating, int overAllRating)
    {
        Id = id;
        Order = order;
        Title = title;
        Description = description;
        Agentrating = agentrating;
        RestaurantRating = restaurantRating;
        OverAllRating = overAllRating;
    }
    
    public Feedback(FeedbackDTO feedbackDto)
    {
        Id = feedbackDto.Id;
        Order = new Order(feedbackDto.OrderDTO);
        Title = feedbackDto.Title;
        Description = feedbackDto.Description;
        Agentrating = feedbackDto.Agentrating;
        RestaurantRating = feedbackDto.RestaurantRating;
        OverAllRating = feedbackDto.OverAllRating;
    }
}