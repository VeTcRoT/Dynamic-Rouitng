namespace WebApplication6.DynamicRouting.Models;

public class VirtualFolder
{
    public int FolderId { get; set; }

    public string FullPath { get; set; }

    public string FolderName { get; set; }

    public ICollection<VirtualFolder>? Children { get; set; } = new List<VirtualFolder>();

    public int? IdParentFolder { get; set; }

    public VirtualFolder? ParentFolder { get; set; }

    public int? DepthLevel { get; set; }
}