using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication6.DynamicRouting.Models;
using WebApplication6.DynamicRouting.ViewModels;


namespace WebApplication6.DynamicRouting.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var configPath = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var sqlConnection = new SqlConnection(configPath.GetConnectionString("DefaultConnection"));

        sqlConnection.Open();

        string query = $"SELECT * FROM Folders WHERE DepthLevel = 0";

        var command = new SqlCommand(query, sqlConnection);

        SqlDataReader reader = command.ExecuteReader();

        List<RootFolder> rootFolders = new List<RootFolder>();

        while (reader.Read())
        {
            rootFolders.Add(new RootFolder 
            { 
                FolderId = reader.GetInt32(0),
                FullPath = reader.GetString(1),
                FolderName = reader.GetString(2),
                IdParentFolder = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                DepthLevel = reader.IsDBNull(4) ? null : reader.GetInt32(4)
            });
        }

        sqlConnection.Close();

        return View(rootFolders);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}