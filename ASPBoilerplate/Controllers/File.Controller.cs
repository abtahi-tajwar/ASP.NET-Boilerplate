using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ASPBoilerplate.Modules.File;

namespace ASPBoilerplate.Controllers;

[ApiController]
[Route("/admin/files")]
[Authorize("Admin")]
public class FileControllerAdmin : ControllerBase
{
    private readonly FileService _service;
    private readonly AppDbContext _context;
    public FileControllerAdmin(AppDbContext context, FileService service)
    {
        _service = service;
        _context = context;
    }

    [HttpGet("")]
    public IResult GetFilesAdmin()
    {
        var files = _context.Files.ToList();
        return CustomResponse.Ok(files);
    }

    [HttpGet("{id}", Name = "GetFileSingle")]
    public IResult GetFileAdmin(string id)
    {
        var file = _context.Files.Find(id);
        return CustomResponse.Ok(file);
    }

    [HttpPost("upload")]
    public IResult UploadFilesAdmin([FromForm] IFormFileCollection files)
    {
        if (files == null || !files.Any())
        {
            return CustomResponse.BadRequest("No files received");
        }

        if (!FileService.IsAllUploadedFileExtensionsValid(files))
        {
            return CustomResponse.BadRequest($"All Files must be {string.Join(", ", FileConstants.ALLOWED_EXTENSION)}");
        }

        List<FileEntity> uploaded = _service.UploadFiles(files);

        return CustomResponse.Ok(uploaded);
    }
}