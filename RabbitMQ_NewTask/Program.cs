using RabbitMQ.Client;
using RabbitMQ_NewTask.Service;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQ_NewTask
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(queue: "task_queue2",
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    Thread thr1 = new Thread(() => Sender.GetData(channel));
                    thr1.Start();

                    Console.ReadLine();
                }
            }
        }

    }


}
