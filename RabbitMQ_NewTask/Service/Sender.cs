using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ_NewTask.Service
{
    public static class Sender
    {
        public static void GetData(IModel channel)
        {
            string[] _arr = { ".", "..", "...", "....", "...." };
            string message = String.Empty;

            for (int i = 0; i < _arr.Length; i++)
            {
                message = (i + 1) + ". Message" + _arr[i];

                Console.WriteLine(message);
                Thread.Sleep(i * 1000);

                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                for (int j = 0; j < 2; j++)  // aynı mesajı aynı anda 2 kere göndereceğiz.
                {
                    channel.BasicPublish(exchange: "",
                                    routingKey: "task_queue2",
                                    basicProperties: properties,
                                    body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
               

             
            }
        }
    }
}
