﻿namespace OrderAndFeedbackService.DTOs;

public class OrderLineDTO
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
}