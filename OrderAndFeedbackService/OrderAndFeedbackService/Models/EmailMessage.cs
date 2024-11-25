﻿namespace OrderAndFeedbackService.Models;

public class EmailMessage
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }

    public EmailMessage(string toEmail, string subject, string content)
    {
        ToEmail = toEmail;
        Subject = subject;
        Content = content;
    }
}