using Microsoft.EntityFrameworkCore;
using OrderAndFeedbackService.DTOs;
using OrderAndFeedbackService.Models;

namespace OrderAndFeedbackService.Facades;
public class FeedbackFacade
{
    private readonly ApplicationDbContext _context;
    
        public FeedbackFacade(ApplicationDbContext context)
        {
            _context = context;
        }

        
        //Create feedback by order
        public Feedback CreateFeedback(FeedbackDTO feedbackDto)
        {
            OrderFacade orderFacade = new OrderFacade(_context);
            Order order = orderFacade.GetOrder(feedbackDto.OrderDTO.Id);
            Feedback feedback = new Feedback(feedbackDto,order);
            feedback.Order = order;
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return feedback;
        }
        
        public List<Feedback> GetFeedbacksByRestaurant(int restaurantId)
        {
            return _context.Feedbacks
                .Include(feedback => feedback.Order)
                .ThenInclude(order => order.RestaurantId)
                .Where(feedback => feedback.Order.RestaurantId == restaurantId)
                .ToList();
        }
        
        public List<Feedback> GetFeedbacksByAgent(int agentId)
        {
            return _context.Feedbacks
                .Include(feedback => feedback.Order)
                .ThenInclude(order => order.AgentId)
                .Where(feedback => feedback.Order.AgentId == agentId)
                .ToList();
        }
        
        
        
}