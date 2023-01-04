namespace WebApplication6.DynamicRouting.Models;

public class FolderViewModel
{
    public string ParentPath { get; set; }

    public string FullPath { get; set; }

    public string FolderName { get; set; }

    public List<ChildViewModel> Children { get; set; } = new();

    public int? DepthLevel { get; set; }
}

public class ChildViewModel
{
    public string Name { get; set; }

    public string Url { get; set; }
}