using System;
using Microsoft.EntityFrameworkCore;

namespace ASPBoilerplate.Modules.Chat;

public class ChatService
{
    private readonly AppDbContext _context;

    public ChatService (AppDbContext context) {
        _context = context;
    }
    public async Task RegisterConnnectionAsync (string UserId, string ConnectionId) {
        var ExistingConnection = await _context.ChatHubConnections.FirstOrDefaultAsync(c => c.UserId == UserId && c.ConnectionId == ConnectionId); 
        if (ExistingConnection != null) {
            ExistingConnection.ConnectionId = ConnectionId;
        } else {
            _context.ChatHubConnections.Add(new() {
                UserId = UserId,
                ConnectionId = ConnectionId
            });
        }
        _context.SaveChanges();
    }
    public async Task UnregisterConnectionAsync (string? UserId) {
        if (UserId == null) {
            Console.WriteLine("No user ID provded, unable to unregister connection");
            return;
        }    
        var ExistingConnection = await _context.ChatHubConnections.FirstOrDefaultAsync(c => c.UserId == UserId);
        if (ExistingConnection == null) {
            throw new Exception("No connection registered under this user on the first place");
        }
        _context.ChatHubConnections.Remove(ExistingConnection);
        _context.SaveChanges();
    }
}
