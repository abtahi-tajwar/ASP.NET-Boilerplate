namespace ASPBoilerplate.Modules.File;

using ASPBoilerplate.Utils;

public static class FileController
{
    private readonly static List<IFile> files =
    [
        new IFile(
            Id: "1",
            Name: "File1_Wakthu.txt",
            Location: "/files/File1.txt",
            Storage: FILE_STORAGE_TYPES.LOCAL,
            IsUsed: true,
            CreatedAt: DateTime.Now.AddDays(-10),
            UpdatedAt: DateTime.Now
        ),
        new IFile(
            Id: "2",
            Name: "Image1.png",
            Location: "/images/Image1.png",
            Storage: FILE_STORAGE_TYPES.LOCAL,
            IsUsed: false,
            CreatedAt: DateTime.Now.AddDays(-20),
            UpdatedAt: DateTime.Now.AddDays(-5)
        ),
        new IFile(
            Id: "3",
            Name: "Document1.pdf",
            Location: "/docs/Document1.pdf",
            Storage: FILE_STORAGE_TYPES.FIREBASE,
            IsUsed: true,
            CreatedAt: DateTime.Now.AddMonths(-1),
            UpdatedAt: DateTime.Now.AddDays(-15)
        ),
        new IFile(
            Id: "4",
            Name: "Presentation1.pptx",
            Location: "/presentations/Presentation1.pptx",
            Storage: FILE_STORAGE_TYPES.LOCAL,
            IsUsed: true,
            CreatedAt: DateTime.Now.AddMonths(-2),
            UpdatedAt: DateTime.Now.AddMonths(-1)
        ),
        new IFile(
            Id: "5",
            Name: "VIdeo1.mp4",
            Location: "/vIdeos/VIdeo1.mp4",
            Storage: FILE_STORAGE_TYPES.LOCAL,
            IsUsed: false,
            CreatedAt: DateTime.Now.AddYears(-1),
            UpdatedAt: DateTime.Now.AddMonths(-6)
        )
    ];

    public static WebApplication FileRoutes(this WebApplication app)
    {
        app.MapGet("/files", (AppDbContext dbContext) =>
        {
            var files = dbContext.Files.ToList();
            return Results.Ok(files);
        });

        app.MapGet("/files/{Id}", (string id, AppDbContext dbContext) =>
            {
                var file = dbContext.Files.Find(id);
                return file;
            })
            .WithName("GetFileSingle");

        app.MapPost("/files", (IFilePost body, AppDbContext dbContext) =>
        {
            var newFile = FileBinder.FilePostToDto(body);

            dbContext.Files.Add(newFile);
            dbContext.SaveChanges();

            // I need to learn about C# Enums more :(((( 

            return Results.CreatedAtRoute("GetFileSingle", new { Id = newFile.Id }, newFile);
        });
        return app;
    }
}