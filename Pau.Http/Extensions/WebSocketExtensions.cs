using System.Net.WebSockets;

namespace Pau.Http.Extensions;

public static class WebSocketExtensions
{
    public static async Task<WebSocket> AcceptAsync(this WebSocketManager manager, bool unsafeCompression = false)
    {
        if (unsafeCompression)
        {
            return await manager.AcceptWebSocketAsync(
                new WebSocketAcceptContext { DangerousEnableCompression = true }
            );
        }

        return await manager.AcceptWebSocketAsync();
    }
}
