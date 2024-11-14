namespace OrderAndFeedbackService.DTOs;

public class UpdateStatusDTO
{
    public int OrderId { get; set; }
    public string Status { get; set; }
    
    public UpdateStatusDTO()
    {
    }
    
    public UpdateStatusDTO(int orderId, string status)
    {
        OrderId = orderId;
        Status = status;
    }
}