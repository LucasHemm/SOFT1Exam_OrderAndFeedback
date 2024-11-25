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
        
        
        
        //Get all feedbacks by restaurant by orders

    
}