namespace ASPBoilerplate.Modules.File;

using System.Runtime.CompilerServices;
using ASPBoilerplate.Utils;
using Microsoft.AspNetCore.Mvc;

public static class FileController
{

    public static WebApplication FileRoutes(this WebApplication _app)
    {
        var app = _app.MapGroup("/files");
        app.DisableAntiforgery();
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

        app.MapPost("/upload", ([FromForm] IEnumerable<IFormFile> files, AppDbContext dbContext) =>
        {
            FileService service = new(dbContext);
            foreach(var file in files)
            {
                if (file == null)
                {
                    Console.WriteLine("File is null");
                } else {
                    Console.WriteLine(file.FileName); 
                }
            }
            List<FileEntity> uploaded = service.UploadFiles(files);

            return CustomResponse.Ok(uploaded);

        });
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