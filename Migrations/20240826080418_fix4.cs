using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApi.Migrations
{
    /// <inheritdoc />
    public partial class fix4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "BookBookGenre");

            migrationBuilder.DropTable(
                name: "BookGenreGenre");

            migrationBuilder.DropTable(
                name: "BookGenres");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => new { x.BookId, x.GenreId });
                });

            migrationBuilder.CreateTable(
                name: "BookBookGenre",
                columns: table => new
                {
                    booksId = table.Column<int>(type: "int", nullable: false),
                    BookGenresBookId = table.Column<int>(type: "int", nullable: false),
                    BookGenresGenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBookGenre", x => new { x.booksId, x.BookGenresBookId, x.BookGenresGenreId });
                    table.ForeignKey(
                        name: "FK_BookBookGenre_BookGenres_BookGenresBookId_BookGenresGenreId",
                        columns: x => new { x.BookGenresBookId, x.BookGenresGenreId },
                        principalTable: "BookGenres",
                        principalColumns: new[] { "BookId", "GenreId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookBookGenre_Books_booksId",
                        column: x => x.booksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGenreGenre",
                columns: table => new
                {
                    genresId = table.Column<int>(type: "int", nullable: false),
                    bookGenresBookId = table.Column<int>(type: "int", nullable: false),
                    bookGenresGenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenreGenre", x => new { x.genresId, x.bookGenresBookId, x.bookGenresGenreId });
                    table.ForeignKey(
                        name: "FK_BookGenreGenre_BookGenres_bookGenresBookId_bookGenresGenreId",
                        columns: x => new { x.bookGenresBookId, x.bookGenresGenreId },
                        principalTable: "BookGenres",
                        principalColumns: new[] { "BookId", "GenreId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenreGenre_Genres_genresId",
                        column: x => x.genresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBookGenre_BookGenresBookId_BookGenresGenreId",
                table: "BookBookGenre",
                columns: new[] { "BookGenresBookId", "BookGenresGenreId" });

            migrationBuilder.CreateIndex(
                name: "IX_BookGenreGenre_bookGenresBookId_bookGenresGenreId",
                table: "BookGenreGenre",
                columns: new[] { "bookGenresBookId", "bookGenresGenreId" });
        }
    }
}
