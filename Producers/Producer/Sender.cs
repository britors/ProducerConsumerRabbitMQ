using RabbitMQ.Client;
using System;
using System.Text;

/// <summary>
/// Create a very basic RabbitMQ's Producer
/// </summary>
namespace Producer
{
    public class Sender
    {
        public static void Main(string[] args)
        {
            // Create a factory to my rabbitmq
            var factory = new ConnectionFactory() { HostName = "localhost" };
            // Create a connection
            using var connection = factory.CreateConnection();
            //Create a chanel
            using (var chanel = connection.CreateModel())
            {
                // Declare a Queue
                chanel.QueueDeclare("BasicTest", false, false, false, null);
                // Declare the message
                string message = "Getting startup with .Net Core RabbitMQ";
                // Body is UTF8 Byte array from the string
                var body = Encoding.UTF8.GetBytes(message);
                // Send (publish) the body in Queue BasicTest
                chanel.BasicPublish("", "BasicTest", null, body);
                // Informe message Sent
                Console.WriteLine("Sent message {0}", message);
            }

            Console.WriteLine("Press [enter] to exit then Sender.");
            Console.ReadLine();
        }
    }
}
