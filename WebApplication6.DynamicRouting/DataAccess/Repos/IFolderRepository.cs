using WebApplication6.DynamicRouting.Models;

namespace WebApplication6.DynamicRouting.DataAccess.Repos;

public interface IFolderRepository
{
    Task<VirtualFolder?> GetRootFolderByName(string name);

    Task<VirtualFolder?> GetFolderByName(string name);

    Task<VirtualFolder?> GetFolderByFullPath(string fullPath);
}