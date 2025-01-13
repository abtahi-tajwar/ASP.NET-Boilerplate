using System.Security.Claims;
using ASPBoilerplate;
using ASPBoilerplate.Modules.Chat;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _dbContext;
        private readonly ChatService _service;

        public ChatHub(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _service = new ChatService(dbContext);
        }
        public async Task SendMessage(string user, string message, string toUser)
        {
            // Console.WriteLine($"{user}, {toUser}, {message}");
            // if (toUser != null && toUser != "")
            // {
            //     await Clients.Client(toUser).SendAsync("ReceiveMessage", message);
            // }
            // else
            // {

            //     await Clients.All.SendAsync("ReceiveMessage", user, message);
            // }
            try
            {
                await _service.SendMessageToUserAsync(Clients, user, message, toUser);
            } catch (Exception e) {
                Console.WriteLine($"Failed to send message. {e.Message}");
                Context.Abort(); 
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
            var user = Context.User;
            var ConnectionId = Context.ConnectionId;

            try
            {

                if (user?.Identity?.IsAuthenticated == true)
                {
                    // Access claims from the Bearer token
                    var UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var Email = user.FindFirst(ClaimTypes.Name)?.Value;
                    var Role = user.FindFirst(ClaimTypes.Role)?.Value;
                    // Optionally, log the connection for debugging
                    Console.WriteLine($"User connected: {ConnectionId}");
                    if (UserId == null)
                    {
                        throw new Exception("User ID not found, aborting connection...");
                    }
                    await _service.RegisterConnnectionAsync(UserId, ConnectionId);
                    Console.WriteLine($"User connected: ID = {UserId}, Email = {Email}, Role = {Role}");
                }
                else
                {
                    Console.WriteLine("Unauthenticated connection attempt.");
                    Context.Abort(); // Disconnect the client if not authenticated
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Context.Abort();
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? e)
        {
            Console.WriteLine($"Connection disconnected {e?.Message}");
            var user = Context.User;
            var UserId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _service.UnregisterConnectionAsync(UserId);
            await base.OnDisconnectedAsync(e);
        }
    }



    // This method gets called when a client connects and assigns the user ID

}

