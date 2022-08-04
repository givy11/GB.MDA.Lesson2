using System;
using System.Text;

namespace GB.MDA.Lesson2.Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var producer = new RabbitProducer();
            var testMessage = "test";
            producer.SendToQueue(Encoding.UTF8.GetBytes(testMessage),"RabbitTest");
        }
    }
}