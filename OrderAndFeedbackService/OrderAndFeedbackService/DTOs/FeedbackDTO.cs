using OrderAndFeedbackService.Models;

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

    public FeedbackDTO()
    {
    }

    public FeedbackDTO(int id, OrderDTO orderDto, string title, string description, int agentrating, int restaurantRating, int overAllRating)
    {
        Id = id;
        OrderDTO = orderDto;
        Title = title;
        Description = description;
        Agentrating = agentrating;
        RestaurantRating = restaurantRating;
        OverAllRating = overAllRating;
    }
    
    public FeedbackDTO(Feedback feedback)
    {
        Id = feedback.Id;
        OrderDTO = new OrderDTO(feedback.Order);
        Title = feedback.Title;
        Description = feedback.Description;
        Agentrating = feedback.Agentrating;
        RestaurantRating = feedback.RestaurantRating;
        OverAllRating = feedback.OverAllRating;
    }
}