using RabbitMQ.Client;
using System;

namespace ProductScanner.Gateway.Interfaces
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
