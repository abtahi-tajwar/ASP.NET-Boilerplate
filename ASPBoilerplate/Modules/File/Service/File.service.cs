using ASPBoilerplate.Utils;

namespace ASPBoilerplate.Modules.File;

[ScopedService]
public class FileService
{
    private readonly AppDbContext dbContext;
    private readonly FILE_STORAGE_TYPES StorageType = FileConstants.FILE_STORAGE_TYPE;
    private readonly Dictionary<FILE_STORAGE_TYPES, Func<IEnumerable<IFormFile>, List<CreateFileDto>>> FILE_UPLOAD_ACTION_MAP =
        new Dictionary<FILE_STORAGE_TYPES, Func<IEnumerable<IFormFile>, List<CreateFileDto>>>
        {
            { FILE_STORAGE_TYPES.LOCAL, (IEnumerable<IFormFile> files) => SaveToLocal(files) }
            // { FILE_STORAGE_TYPES.FIREBASE, SaveToFirebase }
        };
    public FileService(AppDbContext _dbContext)
    {
        dbContext = _dbContext;
    }
    /**
    * Database Operations
    */
    public FileEntity CreateFile(CreateFileDto body)
    {
        // Create a new file
        var newFile = FileBinder.CreateDtoToEntity(body);
        dbContext.Files.Add(newFile);
        dbContext.SaveChanges();

        return newFile;
    }
    public FileEntity? UpdateFile(string Id, UpdateFileDto body)
    {
        var file = dbContext.Files.Find(Id);
        if (file == null) return null;
        // Works like a charm!!! Props to chatgpt
        Helpers.UpdateEntityWithDto<FileEntity, UpdateFileDto>(file, body);
        // dbContext.Entry(file).CurrentValues.SetValues(body);
        dbContext.SaveChanges();
        return file;
    }
    public FileEntity? DeleteFile(string Id)
    {
        var file = dbContext.Files.Find(Id);
        if (file == null) return null;
        dbContext.Files.Remove(file);
        dbContext.SaveChanges();
        return file;
    }
    public List<FileEntity> UploadFiles(IEnumerable<IFormFile> files)
    {
        List<CreateFileDto> FileEntries = FILE_UPLOAD_ACTION_MAP[StorageType](files);
        List<FileEntity> FileEntities = [];

        foreach (var file in FileEntries)
        {
            FileEntity fileEntity = FileBinder.CreateDtoToEntity(file);
            dbContext.Files.Add(fileEntity);
            dbContext.SaveChanges();
            FileEntities.Add(fileEntity);
        }

        return FileEntities;
    }

    /**
    * Action Methods
    */
    public static List<CreateFileDto> SaveToLocal(IEnumerable<IFormFile> files)
    {
        List<CreateFileDto> FileEntries = [];
        foreach (var formFile in files)
        {
            if (formFile.Length > 0)
            {
                string fileName = $"{System.IO.Path.GetRandomFileName()}{System.IO.Path.GetExtension(formFile.FileName)}";
                var filePath = System.IO.Path.Combine(FileConstants.FILE_STORAGE_PATH, fileName);

                CreateFileDto fileDto = new CreateFileDto(
                    Name: fileName, 
                    Location: filePath, 
                    Storage: FileConstants.FILE_STORAGE_TYPE,
                    OriginalName: formFile.FileName
                );
                FileEntries.Add(fileDto);

                using (var stream = System.IO.File.Create(filePath))
                {
                    formFile.CopyTo(stream);
                }
            }
        }
        return FileEntries;
    }
    public static List<CreateFileDto> SaveToFirebase(List<IFormFile> files)
    {
        List<CreateFileDto> FileEntries = [];
        Console.WriteLine("Function for saving to firebase is not implemented yet!!");
        return FileEntries;
    }
    /**
    * End Action Methods
    */
    /**
    * End Database Operations
    */
    /**
     * Helper methods
     */
    public static FILE_STORAGE_TYPES GetFileStorageEnum(string? storage)
    {
        FILE_STORAGE_TYPES _storage;
        if (storage == null)  _storage = FILE_STORAGE_TYPES.LOCAL;
        if (storage.ToLower() == "Local".ToLower())
        {
            _storage = FILE_STORAGE_TYPES.LOCAL;
        }
        else if (storage.ToLower() == "Remote".ToLower())
        {
            _storage = FILE_STORAGE_TYPES.LOCAL;
        }
        else
        {
            _storage = FILE_STORAGE_TYPES.LOCAL;
        }

        return _storage;
    }
    public static Boolean IsAllUploadedFileExtensionsValid(IEnumerable<IFormFile> files) {
        Boolean isValid = true;
        foreach( var file in files ) {
            if (!FileConstants.ALLOWED_EXTENSION.Contains(System.IO.Path.GetExtension(file.FileName))) {
                isValid = false;
            }
        }
        return isValid;
    }
}