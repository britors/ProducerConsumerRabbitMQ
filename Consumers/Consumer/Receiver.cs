using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;


namespace Consumer
{
    /// <summary>
    /// Basic Consumer from RabbitMQ
    /// </summary>
    public class Receiver
    {
        public static void Main(string[] args)
        {
            // create a factory
            var factory = new ConnectionFactory() { HostName = "localhost" };
            // create a connection from factory
            using (var connection = factory.CreateConnection())
                // create a channel
            using (var chanel = connection.CreateModel())
            {
                // declare the Queue
                chanel.QueueDeclare("BasicTest", false, false, false, null);
                // declare then Event
                var consumer = new EventingBasicConsumer(chanel);
                // Received from Event
                consumer.Received += (model, ea) =>
                {
                    // get message body
                    var body = ea.Body.ToArray();
                    // convert array to string
                    var message = Encoding.UTF8.GetString(body);
                    // write the message in console
                    Console.WriteLine("Recived message {0}", message);
                };
                // Cosume
                chanel.BasicConsume("BasicTest", true, consumer);
            }
            // exit
            Console.WriteLine("Press [enter] to exit consumer");
            Console.ReadLine();
        }
    }
}
