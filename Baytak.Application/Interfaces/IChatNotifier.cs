using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Interfaces
{
    public interface IChatNotifier
    {
        Task SendMessageAsync(Guid conversationId, object message);
    }
}
