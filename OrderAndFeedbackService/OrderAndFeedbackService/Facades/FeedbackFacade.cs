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
        
        public Feedback CreateFeedback(FeedbackDTO feedbackDto)
        {
            Feedback feedback = new Feedback(feedbackDto);
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return feedback;
        }
        
        //Get all feedbacks by restaurant by orders

    
}