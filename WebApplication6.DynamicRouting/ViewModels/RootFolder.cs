namespace WebApplication6.DynamicRouting.ViewModels
{
    public class RootFolder
    {
        public int FolderId { get; set; }
        public string FullPath { get; set; }
        public string FolderName { get; set; }
        public int? IdParentFolder { get; set; }
        public int? DepthLevel { get; set; }
    }
}
