namespace ASPBoilerplate.Modules.File;

using ASPBoilerplate.Utils;

public static class FileController
{

    public static WebApplication FileRoutes(this WebApplication _app)
    {
        var app = _app.MapGroup("/files");
        app.MapGet("/", (AppDbContext dbContext) =>
        {
            var files = dbContext.Files.ToList();
            return CustomResponse.Ok(files);
        });

        app.MapGet("/{Id}", (string id, AppDbContext dbContext) =>
            {
                var file = dbContext.Files.Find(id);
                return CustomResponse.Ok(file);
            })
            .WithName("GetFileSingle");

        app.MapPost("/", (CreateFileDto body, AppDbContext dbContext) =>
        {
            FileService service = new(dbContext);
            FileEntity newFile = service.CreateFile(body);

            return CustomResponse.CreatedAtRoute("GetFileSingle", newFile.Id, newFile);
        });

        // Update file
        app.MapPatch("/{id}", (string id, UpdateFileDto body, AppDbContext dbContext) =>
        {
            FileService service = new(dbContext);
            FileEntity? updatedFile = service.UpdateFile(id, body);

            if (updatedFile == null)
            {
                return Results.NotFound();
            }

            return CustomResponse.Ok(updatedFile);
        });

        app.MapDelete("/{id}", (string id, AppDbContext dbContext) =>
        {
            FileService service = new(dbContext);
            FileEntity? deletedFile = service.DeleteFile(id);

            if (deletedFile == null)
            {
                return Results.NotFound();
            }

            return CustomResponse.Ok(deletedFile);
        });
        return _app;
    }
}