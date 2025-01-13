using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPBoilerplate.Modules.Chat;

public class ChatHubConnections
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public required string UserId { get; set; }
    public required string ConnectionId { get; set; } = null;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}

public class MessageHistory
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public required string SenderId { get; set;}
    public required string ReceiverId { get; set; }
    public DateTime SentAt { get; set; } = DateTime.Now; 
    public required string Message {get; set;}
}
public class ChatInbox
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public required string UserId { get; set; }
    public required string MessagedUserId { get; set; }
}
