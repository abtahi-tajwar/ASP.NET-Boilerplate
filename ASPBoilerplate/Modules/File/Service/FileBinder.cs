namespace ASPBoilerplate.Modules.File;

public class FileBinder
{
    public static FileEntity FilePostToDto (IFilePost file)
    {
        FILE_STORAGE_TYPES _storage;
        
        if (file.Storage == "Local")
        {
            _storage = FILE_STORAGE_TYPES.LOCAL;
        } 
        else if (file.Storage == "Remote")
        {
            _storage = FILE_STORAGE_TYPES.FIREBASE;
        }
        else
        {
            _storage = FILE_STORAGE_TYPES.LOCAL;
        }

        var newFile = new FileEntity {
            Name = file.Name,
            Location = $"/files/{file.Name}",
            Storage = _storage,
            IsUsed = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        return newFile;
    }
}