using System.Net.Security;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GB.MDA.Lesson2.Consumer
{
    public class Consumer
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IModel _channel;
        private readonly string _queueName;

        public Consumer(string queueName)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "shark-01.rmq.cloudamqp.com",
                VirtualHost = "ynlrftfm",
                UserName = "ynlrftfm", //Указать имя пользователя
                Password = "8kwCFzMD4mzMaQwTShEmtpiWM-kM994d", //Указать пароль
                Port = 5671,
                RequestedHeartbeat = TimeSpan.FromSeconds(10),
                Ssl = new SslOption
                {
                    Enabled = true,
                    AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateNameMismatch |
                                             SslPolicyErrors.RemoteCertificateChainErrors,
                    Version = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls11
                }
            };

            _channel = _connectionFactory.CreateConnection().CreateModel();
            _queueName = queueName;
        }

        public void Receive(EventHandler<BasicDeliverEventArgs> receiveCallback)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += receiveCallback;

            _channel.BasicConsume(_queueName, true, consumer);
        }
    }
}
