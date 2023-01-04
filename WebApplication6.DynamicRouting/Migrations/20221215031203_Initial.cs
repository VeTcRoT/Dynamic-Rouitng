using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication6.DynamicRouting.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    FolderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullPath = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdParentFolder = table.Column<int>(type: "int", nullable: true),
                    DepthLevel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.FolderId);
                    table.ForeignKey(
                        name: "FK_Folders_Folders_IdParentFolder",
                        column: x => x.IdParentFolder,
                        principalTable: "Folders",
                        principalColumn: "FolderId");
                });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "FolderId", "DepthLevel", "FolderName", "FullPath", "IdParentFolder" },
                values: new object[,]
                {
                    { 1, 0, "Creating Digital Images", "Creating Digital Images", null },
                    { 2, 1, "Resources", "Creating Digital Images/Resources", 1 },
                    { 3, 1, "Evidence", "Creating Digital Images/Evidence", 1 },
                    { 6, 1, "Graphic Products", "Creating Digital Images/Graphic Products", 1 },
                    { 4, 2, "Primary Sources", "Creating Digital Images/Resources/Primary Sources", 2 },
                    { 5, 2, "Secondary Sources", "Creating Digital Images/Resources/Secondary Sources", 2 },
                    { 7, 2, "Process", "Creating Digital Images/Graphic Products/Process", 6 },
                    { 8, 2, "Final Product", "Creating Digital Images/Graphic Products/Final Product", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Folders_FullPath",
                table: "Folders",
                column: "FullPath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Folders_IdParentFolder",
                table: "Folders",
                column: "IdParentFolder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Folders");
        }
    }
}
