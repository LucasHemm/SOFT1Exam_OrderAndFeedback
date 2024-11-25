using System;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using OrderAndFeedbackService.Models;
using OrderAndFeedbackService.Models;
using IModel = RabbitMQ.Client.IModel;

namespace OrderAndFeedbackService.Services
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly string _queueName = "email_queue";
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessagePublisher(IConfiguration configuration)
        {
            var rabbitMqHost = configuration["RabbitMQ:HostName"] ?? "rabbitmq";
            var rabbitMqUser = configuration["RabbitMQ:UserName"] ?? "guest";
            var rabbitMqPassword = configuration["RabbitMQ:Password"] ?? "guest";
            var rabbitMqPort = int.Parse(configuration["RabbitMQ:Port"] ?? "5672");

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMqHost,
                Port = rabbitMqPort,
                UserName = rabbitMqUser,
                Password = rabbitMqPassword
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _queueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void PublishEmailMessage(EmailMessage message)
        {
            var messageJson = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(messageJson);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(exchange: "",
                                  routingKey: _queueName,
                                  basicProperties: properties,
                                  body: body);

            Console.WriteLine($"Sent email message to {message.ToEmail}");
        }
    }
}
