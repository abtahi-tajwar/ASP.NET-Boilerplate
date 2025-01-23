using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ASPBoilerplate.Modules.Chat;

[ScopedService]
public class ChatService
{
    private readonly AppDbContext _context;

    public ChatService(AppDbContext context)
    {
        _context = context;
    }
    public async Task RegisterConnnectionAsync(string UserId, string ConnectionId)
    {
        var ExistingConnection = await _context.ChatHubConnections.FirstOrDefaultAsync(c => c.UserId == UserId && c.ConnectionId == ConnectionId);
        
        // First delete all the existing connections to avoid concurrency
        var itemsToRemove = _context.ChatHubConnections.Where(item => item.UserId == UserId).ToList();
        _context.ChatHubConnections.RemoveRange(itemsToRemove);

        // Create a seperate new connection entry
        _context.ChatHubConnections.Add(new()
        {
            UserId = UserId,
            ConnectionId = ConnectionId
        });
        _context.SaveChanges();
    }
    public async Task UnregisterConnectionAsync(string? UserId)
    {
        if (UserId == null)
        {
            Console.WriteLine("No user ID provded, unable to unregister connection");
            return;
        }
        var ExistingConnection = await _context.ChatHubConnections.FirstOrDefaultAsync(c => c.UserId == UserId);
        if (ExistingConnection == null)
        {
            throw new Exception("No connection registered under this user on the first place");
        }
        _context.ChatHubConnections.Remove(ExistingConnection);
        _context.SaveChanges();
    }

    public async Task SendMessageToUserAsync(IHubCallerClients Client, string Message, string SenderId, string ReceiverId)
    {
        var RestrictedUserEntity = await _context.RestrictedUsers.FirstOrDefaultAsync(user => user.Id == ReceiverId);
        var UnrestrictedUserEntity = await _context.UnrestrictedUsers.FirstOrDefaultAsync(user => user.Id == ReceiverId);

        Console.WriteLine($"Message={Message}\nReceiverId=${ReceiverId}");
        if (RestrictedUserEntity == null && UnrestrictedUserEntity == null)
        {
            throw new Exception("Invalid receiver id, no user exists with that ID");
        }
        var ReceiverConnectionId = await _context.ChatHubConnections.FirstOrDefaultAsync(con => con.UserId == ReceiverId);
        Console.WriteLine($"Receiver retreived connection={ReceiverConnectionId?.ConnectionId}");
        if (ReceiverConnectionId != null)
        {
            await Client.Client(ReceiverConnectionId.ConnectionId).SendAsync("ReceiveMessage", Message);
        }
        _context.MessageHistories.Add(new MessageHistory()
        {
            SenderId = SenderId,
            ReceiverId = ReceiverId,
            Message = Message
        });
        var AlreadyInInbox = await _context.ChatInboxes.FirstOrDefaultAsync(inbox => (inbox.UserId == SenderId && inbox.MessagedUserId == ReceiverId));
        if (AlreadyInInbox == null)
        {
            _context.Add(new ChatInbox()
            {
                UserId = SenderId,
                MessagedUserId = ReceiverId,
            });
        }
        await _context.SaveChangesAsync();
    }

    
    
}
