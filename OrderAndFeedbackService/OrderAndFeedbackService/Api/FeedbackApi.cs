using Microsoft.AspNetCore.Mvc;
using OrderAndFeedbackService.DTOs;
using OrderAndFeedbackService.Facades;

namespace OrderAndFeedbackService.Api;
[ApiController]
[Route("api/[controller]")]
public class FeedbackApi : ControllerBase
{
    private readonly FeedbackFacade _feedbackFacade;
    
    public FeedbackApi(FeedbackFacade feedbackFacade)
    {
        _feedbackFacade = feedbackFacade;
    }
    
    // POST: api/Feedback
    [HttpPost]
    public IActionResult CreateFeedback([FromBody] FeedbackDTO feedbackDto)
    {
        try
        {
            FeedbackDTO createdFeedback = new FeedbackDTO(_feedbackFacade.CreateFeedback(feedbackDto));
            return Ok(createdFeedback);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    
}