namespace ASPBoilerplate.Modules.File;

public record CreateFileDto(
    string Name,
    string Location,
    FILE_STORAGE_TYPES? Storage,
    string? OriginalName
);


public record UpdateFileDto  (
    string? Name,
    string? Location,
    FILE_STORAGE_TYPES? Storage,
    bool? IsUsed
);



