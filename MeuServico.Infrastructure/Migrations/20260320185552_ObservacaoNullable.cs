using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuServico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ObservacaoNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "DespesasGerais");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "DespesasGerais",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
