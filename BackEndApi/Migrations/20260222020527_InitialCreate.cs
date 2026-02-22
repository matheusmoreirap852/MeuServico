using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    CPF = table.Column<string>(type: "TEXT", nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", nullable: false),
                    CNH = table.Column<string>(type: "TEXT", nullable: false),
                    ValidadeCNH = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeFantasia = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    RazaoSocial = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Cnpj = table.Column<string>(type: "TEXT", maxLength: 18, nullable: true),
                    Endereco = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbRegistroServico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Anotacoes = table.Column<string>(type: "TEXT", nullable: false),
                    LinhaTempo = table.Column<string>(type: "TEXT", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbRegistroServico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Placa = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Marca = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Modelo = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Ano = table.Column<int>(type: "INTEGER", nullable: false),
                    Cor = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    EmpresaId = table.Column<int>(type: "INTEGER", nullable: false),
                    KmAtual = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorVenal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorVenda = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorSeguro = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorIPVAAnual = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DataCompra = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carros_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DespesasGerais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataDespesa = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TipoDespesa = table.Column<int>(type: "INTEGER", nullable: false),
                    CarroId = table.Column<int>(type: "INTEGER", nullable: true),
                    Observacao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesasGerais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesasGerais_Carros_CarroId",
                        column: x => x.CarroId,
                        principalTable: "Carros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoValorVenal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CarroId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataReferencia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValorVenal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoValorVenal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoValorVenal_Carros_CarroId",
                        column: x => x.CarroId,
                        principalTable: "Carros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CarroId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataPrevistaDevolucao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataDevolucaoReal = table.Column<DateTime>(type: "TEXT", nullable: true),
                    KmInicial = table.Column<int>(type: "INTEGER", nullable: false),
                    KmFinal = table.Column<int>(type: "INTEGER", nullable: true),
                    ValorDiaria = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantidadeDiarias = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorTotalPrevisto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotalFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MultaAtraso = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locacoes_Carros_CarroId",
                        column: x => x.CarroId,
                        principalTable: "Carros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locacoes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manutencoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CarroId = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    DescricaoServico = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataManutencao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    KmVeiculo = table.Column<int>(type: "INTEGER", nullable: false),
                    ProximaRevisaoData = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ProximaRevisaoKm = table.Column<int>(type: "INTEGER", nullable: true),
                    Observacao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manutencoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manutencoes_Carros_CarroId",
                        column: x => x.CarroId,
                        principalTable: "Carros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carros_EmpresaId",
                table: "Carros",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Carros_Placa",
                table: "Carros",
                column: "Placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DespesasGerais_CarroId",
                table: "DespesasGerais",
                column: "CarroId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoValorVenal_CarroId",
                table: "HistoricoValorVenal",
                column: "CarroId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_CarroId",
                table: "Locacoes",
                column: "CarroId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_ClienteId",
                table: "Locacoes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Manutencoes_CarroId",
                table: "Manutencoes",
                column: "CarroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DespesasGerais");

            migrationBuilder.DropTable(
                name: "HistoricoValorVenal");

            migrationBuilder.DropTable(
                name: "Locacoes");

            migrationBuilder.DropTable(
                name: "Manutencoes");

            migrationBuilder.DropTable(
                name: "tbRegistroServico");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Carros");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
