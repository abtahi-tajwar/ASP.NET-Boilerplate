namespace ASPBoilerplate.Modules.File;

public record IFile (
    string Id,
    string Name,
    string Location,
    FILE_STORAGE_TYPES Storage,
    bool IsUsed,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record IFilePost (
    string Name,
    string Storage
);

public record CreateFileDto(
    string Name,
    string Location,
    FILE_STORAGE_TYPES Storage,
    bool IsUsed,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
