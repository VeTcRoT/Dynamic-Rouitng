using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication6.DynamicRouting.Models;

namespace WebApplication6.DynamicRouting.DataAccess;

public class FolderContext : DbContext
{
    public FolderContext(DbContextOptions<FolderContext> options) : base(options)
    {
    }

    public DbSet<VirtualFolder> Folders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entityBuilder = modelBuilder.Entity<VirtualFolder>();
        entityBuilder.HasKey(x => x.FolderId);
        entityBuilder.Property(x => x.FolderName).IsRequired();
        entityBuilder.Property(x => x.FullPath).IsRequired();
        entityBuilder.HasIndex(x => x.FullPath).IsUnique();
        entityBuilder.Property(x => x.DepthLevel).IsRequired(false);

        entityBuilder
            .HasMany(x => x.Children)
            .WithOne(x => x.ParentFolder)
            .HasForeignKey(x => x.IdParentFolder)
            .IsRequired(false);

        SeedFolders(entityBuilder);
    }

    private void SeedFolders(EntityTypeBuilder<VirtualFolder> entityBuilder)
    {
        VirtualFolder rootFolder = new()
        {
            FolderId = 1,
            FullPath = "Creating Digital Images",
            FolderName = "Creating Digital Images",
            IdParentFolder = null,
            ParentFolder = null,
            DepthLevel = 0
        };

        var resources = new VirtualFolder
        {
            FolderId = 2,
            IdParentFolder = 1,
            FolderName = "Resources",
            FullPath = "Creating Digital Images/Resources",
            DepthLevel = 1
        };

        var evidence = new VirtualFolder
        {
            FolderId = 3,
            IdParentFolder = 1,
            FolderName = "Evidence",
            FullPath = "Creating Digital Images/Evidence",
            DepthLevel = 1
        };

        var primarySources = new VirtualFolder
        {
            FolderId = 4,
            IdParentFolder = 2,
            FolderName = "Primary Sources",
            FullPath = "Creating Digital Images/Resources/Primary Sources",
            DepthLevel = 2
        };

        var secondarySources = new VirtualFolder
        {
            FolderId = 5,
            IdParentFolder = 2,
            FolderName = "Secondary Sources",
            FullPath = "Creating Digital Images/Resources/Secondary Sources",
            DepthLevel = 2
        };

        var graphicProducts = new VirtualFolder
        {
            FolderId = 6,
            IdParentFolder =1,
            FolderName = "Graphic Products",
            FullPath = "Creating Digital Images/Graphic Products",
            DepthLevel = 1
        };

        var process = new VirtualFolder
        {
            FolderId = 7,
            IdParentFolder = 6,
            FolderName = "Process",
            FullPath = "Creating Digital Images/Graphic Products/Process",
            DepthLevel = 2
        };

        var finalProduct = new VirtualFolder
        {
            FolderId = 8,
            IdParentFolder = 6,
            FolderName = "Final Product",
            FullPath = "Creating Digital Images/Graphic Products/Final Product",
            DepthLevel = 2
        };

        entityBuilder.HasData(new[]
        {
            rootFolder,
            resources,
            graphicProducts,
            evidence,
            process,
            finalProduct,
            primarySources,
            secondarySources
        });
    }
}