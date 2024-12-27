namespace ASPBoilerplate.Modules.File;

public class FileBinder
{
    public static FileEntity CreateDtoToEntity (CreateFileDto file)
    {
        var newFile = new FileEntity {
            Name = file.Name,
            Location = $"/files/{file.Name}",
            Storage = FileService.GetFileStorageEnum(file.Storage),
            IsUsed = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        return newFile;
    }
}