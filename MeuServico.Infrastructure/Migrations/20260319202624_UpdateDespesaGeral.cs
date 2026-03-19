using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuServico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDespesaGeral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataDespesa",
                table: "DespesasGerais",
                newName: "Data");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "DespesasGerais",
                newName: "DataDespesa");
        }
    }
}
