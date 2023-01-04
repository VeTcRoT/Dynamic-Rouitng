using Microsoft.AspNetCore.Mvc;
using WebApplication6.DynamicRouting.DataAccess.Repos;
using WebApplication6.DynamicRouting.Models;

namespace WebApplication6.DynamicRouting.Controllers;

public class DynamicRoutesController : Controller
{
    private readonly ILogger<DynamicRoutesController> _logger;
    private readonly IFolderRepository _folderRepository;

    public DynamicRoutesController(ILogger<DynamicRoutesController> logger, IFolderRepository folderRepository)
    {
        _logger = logger;
        _folderRepository = folderRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string fullPath)
    {
        var trimmed = fullPath.Trim('/');

        if (string.IsNullOrEmpty(trimmed))
            return BadRequest("Empty full path");

        var folder = await _folderRepository.GetFolderByFullPath(trimmed);

        if (folder is null)
            return NotFound();

        var lastIndexOf = trimmed.LastIndexOf('/');
        var parentPath = lastIndexOf > 0 ? trimmed.Substring(0, lastIndexOf + 1) : string.Empty;

        FolderViewModel vm = new()
        {
            ParentPath = $"/{parentPath}",
            FolderName = folder.FolderName,
            DepthLevel = folder.DepthLevel,
            FullPath = folder.FullPath,
            Children = folder.Children!.Select(x =>  new ChildViewModel { Name = x.FolderName, Url = $"/{x.FullPath}" }).ToList()
        };

        return View(vm);
    }
}