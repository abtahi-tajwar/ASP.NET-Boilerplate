namespace ASPBoilerplate.Modules.File;

public class FileBinder
{
    public static FileEntity CreateDtoToEntity (CreateFileDto file)
    {
        var newFile = new FileEntity {
            Name = file.Name,
            Location = file.Location,
            Storage = file.Storage ?? FILE_STORAGE_TYPES.LOCAL,
            OriginalName = file.OriginalName ?? "",
            IsUsed = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        return newFile;
    }
}