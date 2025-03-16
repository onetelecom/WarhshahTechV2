using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class ChatHub : Hub<IChatHub>
    {

        public int OnlineUsers { get; set; }
        public async Task BroadcastAsync(ChatMessage message)
        {
            await Clients.All.MessageReceivedFromHub(message);
        }
        public override async Task OnConnectedAsync()
        {
            OnlineUsers++;
            await Clients.All.NewUserConnected("a new user connectd");
        }
        public async Task Notification(string warshahId)
        {
            var connectionId = Context.ConnectionId;

            await Clients.All.ReceiveNotification(warshahId);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            OnlineUsers--;

            return base.OnDisconnectedAsync(exception);
        }
    }

    public interface IChatHub
    {
        Task MessageReceivedFromHub(ChatMessage message);

        Task NewUserConnected(string message);
        Task SendMessage();
        Task ReceiveNotification(string warshahId);
    }
}

