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
