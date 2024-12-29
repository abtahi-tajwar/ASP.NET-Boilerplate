using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPBoilerplate.Modules.User;

public class UserEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public required string  Username { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
