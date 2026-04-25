using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
namespace Baytak.API.Hubs
{
    [Authorize]
    public class ChatHub:Hub
    {
        public async Task JoinConversation(Guid conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,conversationId.ToString());
        }

        
    }
}
