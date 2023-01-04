using Microsoft.EntityFrameworkCore;
using WebApplication6.DynamicRouting.Models;

namespace WebApplication6.DynamicRouting.DataAccess.Repos;

class FolderRepository : IFolderRepository
{
    private readonly FolderContext folderContext;

    public FolderRepository(FolderContext folderContext)
    {
        this.folderContext = folderContext;
    }

    public async Task<VirtualFolder?> GetRootFolderByName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException($"Invalid {nameof(name)} value");

        return await folderContext.Folders
            .Include(x => x.Children)
            .FirstOrDefaultAsync(x => x.FolderName == name && x.DepthLevel == 0);
    }

    public async Task<VirtualFolder?> GetFolderByName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException($"Invalid {nameof(name)} value");

        return await folderContext.Folders
            .Include(x => x.Children)
            .FirstOrDefaultAsync(x => x.FolderName == name);
    }

    public async Task<VirtualFolder?> GetFolderByFullPath(string fullPath)
    {
        if (string.IsNullOrEmpty(fullPath))
            throw new ArgumentException($"Invalid {nameof(fullPath)} value");

        return await folderContext.Folders
            .Include(x => x.Children)
            .Include(x => x.ParentFolder)
            .FirstOrDefaultAsync(x => x.FullPath.Equals(fullPath));
    }
}