using System.ComponentModel.DataAnnotations.Schema;

namespace ASPBoilerplate.Modules.File;

public class FileEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public required string Name {get; set; }
    public string OriginalName { get; set; }
    public required string Location { get; set; }
    public required FILE_STORAGE_TYPES Storage {get; set;}
    public bool IsUsed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}