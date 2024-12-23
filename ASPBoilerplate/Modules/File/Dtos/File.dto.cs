namespace ASPBoilerplate.Modules.File;
// using ASPBoilerplate.Modules.File

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
    FILE_STORAGE_TYPES Storage
);
