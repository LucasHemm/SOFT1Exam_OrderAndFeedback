namespace OrderAndFeedbackService.Models;

public interface IMessagePublisher
{
    void PublishEmailMessage(EmailMessage message);
}