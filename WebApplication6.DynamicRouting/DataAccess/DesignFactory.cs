using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApplication6.DynamicRouting.DataAccess;

internal class DesignFactory : IDesignTimeDbContextFactory<FolderContext>
{
    public FolderContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<FolderContext>();
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        builder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

        return new FolderContext(builder.Options);
    }
}