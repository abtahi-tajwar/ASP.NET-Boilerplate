using ASPBoilerplate.Utils;

namespace ASPBoilerplate.Modules.File;

public class FileService
{
    private readonly AppDbContext dbContext;
    public FileService(AppDbContext _dbContext)
    {
        dbContext = _dbContext;
    }
    /**
    * Database Operations
    */
    public FileEntity CreateFile(CreateFileDto body) {
        // Create a new file
        var newFile = FileBinder.CreateDtoToEntity(body);
        dbContext.Files.Add(newFile);
        dbContext.SaveChanges();

        return newFile;
    }
    public FileEntity? UpdateFile(string Id, UpdateFileDto body) {
        var file = dbContext.Files.Find(Id);
        if (file == null) return null;
        // Works like a charm!!! Props to chatgpt
        Helpers.UpdateEntityWithDto<FileEntity, UpdateFileDto>(file, body);
        // dbContext.Entry(file).CurrentValues.SetValues(body);
        dbContext.SaveChanges();
        return file;
    }
    public FileEntity? DeleteFile(string Id) {
        var file = dbContext.Files.Find(Id);
        if (file == null) return null;
        dbContext.Files.Remove(file);
        dbContext.SaveChanges();
        return file;
    }
    /**
    * End Database Operations
    */
    /**
     * Helper methods
     */
    public static FILE_STORAGE_TYPES GetFileStorageEnum(string storage)
    {
        FILE_STORAGE_TYPES _storage;
        
        if (storage.ToLower() == "Local".ToLower())
        {
            _storage = FILE_STORAGE_TYPES.LOCAL;
        } 
        else if (storage.ToLower() == "Remote".ToLower())
        {
            _storage = FILE_STORAGE_TYPES.FIREBASE;
        }
        else
        {
            _storage = FILE_STORAGE_TYPES.LOCAL;
        }

        return _storage;
    }
}