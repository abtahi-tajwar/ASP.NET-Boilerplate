using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string toUser)
        {
            Console.WriteLine($"{user}, {toUser}, {message}");
            if (toUser != null && toUser != "")
            {
                await Clients.Client(toUser).SendAsync("ReceiveMessage", message);
            }
            else
            {

                await Clients.All.SendAsync("ReceiveMessage", user, message);
            }
        }

        public async Task SendMessageToCaller(string user, string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessageToGroup(string user, string message)
        {
            await Clients.Group("SignalR Users").SendAsync("ReceiveMessage", user, message);
        }
        public override async Task OnConnectedAsync()
        {
            var userId = Context.ConnectionId;

            // Optionally, log the connection for debugging
            Console.WriteLine($"User connected: {userId}");
            await Clients.All.SendAsync("ReceiveMessage", $"User Connected {userId}");
            await base.OnConnectedAsync();
        }
    }



    // This method gets called when a client connects and assigns the user ID

}

