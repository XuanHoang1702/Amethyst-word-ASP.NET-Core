using System.Net.WebSockets;
using System.Collections.Concurrent;

namespace Application.Share
{
    public class WebSocketConnectionManager
    {
        private readonly ConcurrentDictionary<string, WebSocket> _sockets = new();

        public void AddSocket(string orderCode, WebSocket socket)
            => _sockets[orderCode] = socket;

        public void RemoveSocket(string orderCode)
            => _sockets.TryRemove(orderCode, out _);

        public WebSocket? GetSocket(string orderCode)
            => _sockets.TryGetValue(orderCode, out var socket) ? socket : null;
    }
}
