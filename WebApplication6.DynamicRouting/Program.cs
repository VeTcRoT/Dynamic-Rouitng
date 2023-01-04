using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using WebApplication6.DynamicRouting.DataAccess;
using WebApplication6.DynamicRouting.DataAccess.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IFolderRepository, FolderRepository>();
builder.Services.AddSqlServer<FolderContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddTransient<DynamicRouteValueTransformer, CustomRouteValuesTransformer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FolderContext>();
    await context.Database.MigrateAsync();
    var rootFolders = context.Folders.ToArray();

    foreach (var rootFolder in rootFolders)
        app.MapDynamicControllerRoute<DynamicRouteValueTransformer>($"/{rootFolder.FullPath}");
}

app.Run();