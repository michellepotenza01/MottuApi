using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuApi.net.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patios",
                columns: table => new
                {
                    NomePatio = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    VagasTotais = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    VagasOcupadas = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patios", x => x.NomePatio);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    UsuarioFuncionario = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PatioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PatioNomePatio = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.UsuarioFuncionario);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Patios_PatioNomePatio",
                        column: x => x.PatioNomePatio,
                        principalTable: "Patios",
                        principalColumn: "NomePatio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Motos",
                columns: table => new
                {
                    Placa = table.Column<string>(type: "NVARCHAR2(7)", maxLength: 7, nullable: false),
                    Modelo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Marca = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Setor = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PatioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PatioNomePatio = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    UsuarioFuncionario = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    FuncionarioUsuarioFuncionario = table.Column<string>(type: "NVARCHAR2(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motos", x => x.Placa);
                    table.ForeignKey(
                        name: "FK_Motos_Funcionarios_FuncionarioUsuarioFuncionario",
                        column: x => x.FuncionarioUsuarioFuncionario,
                        principalTable: "Funcionarios",
                        principalColumn: "UsuarioFuncionario");
                    table.ForeignKey(
                        name: "FK_Motos_Patios_PatioNomePatio",
                        column: x => x.PatioNomePatio,
                        principalTable: "Patios",
                        principalColumn: "NomePatio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    UsuarioCliente = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    MotoPlaca = table.Column<string>(type: "NVARCHAR2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.UsuarioCliente);
                    table.ForeignKey(
                        name: "FK_Clientes_Motos_MotoPlaca",
                        column: x => x.MotoPlaca,
                        principalTable: "Motos",
                        principalColumn: "Placa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_MotoPlaca",
                table: "Clientes",
                column: "MotoPlaca");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_PatioNomePatio",
                table: "Funcionarios",
                column: "PatioNomePatio");

            migrationBuilder.CreateIndex(
                name: "IX_Motos_FuncionarioUsuarioFuncionario",
                table: "Motos",
                column: "FuncionarioUsuarioFuncionario");

            migrationBuilder.CreateIndex(
                name: "IX_Motos_PatioNomePatio",
                table: "Motos",
                column: "PatioNomePatio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Motos");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Patios");
        }
    }
}
