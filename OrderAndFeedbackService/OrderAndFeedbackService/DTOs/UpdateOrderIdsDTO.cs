namespace OrderAndFeedbackService.DTOs;

public class UpdateOrderIdsDTO
{
    public int orderID { get; set; }
    public int agentID { get; set; }
    public int paymentID { get; set; }
    
    public UpdateOrderIdsDTO(int orderID, int agentID, int paymentID)
    {
        this.orderID = orderID;
        this.agentID = agentID;
        this.paymentID = paymentID;
    }
}