using Blog.Core.SignalR;
using Core.Attribute;
using Microsoft.AspNetCore.SignalR;

namespace Application.SignalR;
[DynamicApi(ServiceLifeCycle = "Transient")]
public class SignalR:ISignalR
{
    private readonly IHubContext<ChatHub> _hubContext;

    public SignalR(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }
        
    public async Task SendMessage(string user, string message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}