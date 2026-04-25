using Baytak.API.Hubs;
using Baytak.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Baytak.API.SignalR
{
    public class SignalRChatNotifier : IChatNotifier
    {
        private readonly IHubContext<ChatHub> _hub;

        public SignalRChatNotifier(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        public async Task SendMessageAsync(Guid conversationId, object message)
        {
            await _hub.Clients.Group(conversationId.ToString())
                .SendAsync("ReceiveMessage", message);
        }
    }
}
