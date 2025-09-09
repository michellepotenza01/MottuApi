using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuApi.net.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        // Definindo as constantes para evitar repetição de literais
        private const string NVarchar450 = "NVARCHAR2(450)";
        private const string NVarchar2000 = "NVARCHAR2(2000)";
        private const string NVarchar100 = "NVARCHAR2(100)";
        private const string NVarchar7 = "NVARCHAR2(7)";
        private const string Patios = "Patios";
        private const string Funcionarios = "Funcionarios";
        private const string Motos = "Motos";
        private const string Clientes = "Clientes";
        private const string NomePatio = "NomePatio";
        private const string UsuarioCliente = "UsuarioCliente";
        private const string UsuarioFuncionario = "UsuarioFuncionario";
        private const string Placa = "Placa";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: Clientes,
                columns: table => new
                {
                    UsuarioCliente = table.Column<string>(type: NVarchar450, maxLength: 450, nullable: false),
                    Nome = table.Column<string>(type: NVarchar2000, maxLength: 2000, nullable: false),
                    Senha = table.Column<string>(type: NVarchar2000, maxLength: 2000, nullable: false),
                    MotoPlaca = table.Column<string>(type: NVarchar2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.UsuarioCliente);
                });

            migrationBuilder.CreateTable(
                name: Patios,
                columns: table => new
                {
                    NomePatio = table.Column<string>(type: NVarchar450, maxLength: 450, nullable: false),
                    Localizacao = table.Column<string>(type: NVarchar2000, maxLength: 2000, nullable: false),
                    VagasTotais = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    VagasOcupadas = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patios", x => x.NomePatio);
                });

            migrationBuilder.CreateTable(
                name: Funcionarios,
                columns: table => new
                {
                    UsuarioFuncionario = table.Column<string>(type: NVarchar450, maxLength: 450, nullable: false),
                    Nome = table.Column<string>(type: NVarchar2000, maxLength: 2000, nullable: false),
                    Senha = table.Column<string>(type: NVarchar2000, maxLength: 2000, nullable: false),
                    NomePatio = table.Column<string>(type: NVarchar450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.UsuarioFuncionario);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Patios_NomePatio",
                        column: x => x.NomePatio,
                        principalTable: Patios,
                        principalColumn: NomePatio,
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: Motos,
                columns: table => new
                {
                    Placa = table.Column<string>(type: NVarchar7, maxLength: 7, nullable: false),
                    Modelo = table.Column<string>(type: NVarchar100, maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: NVarchar100, maxLength: 100, nullable: false),
                    Setor = table.Column<string>(type: NVarchar100, maxLength: 100, nullable: false),
                    NomePatio = table.Column<string>(type: NVarchar2000, maxLength: 2000, nullable: false),
                    UsuarioFuncionario = table.Column<string>(type: NVarchar2000, maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motos", x => x.Placa);
                    table.ForeignKey(
                        name: "FK_Motos_Funcionarios_UsuarioFuncionario",
                        column: x => x.UsuarioFuncionario,
                        principalTable: Funcionarios,
                        principalColumn: UsuarioFuncionario,
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motos_Patios_NomePatio",
                        column: x => x.NomePatio,
                        principalTable: Patios,
                        principalColumn: NomePatio,
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_NomePatio",
                table: Funcionarios,
                column: NomePatio);

            migrationBuilder.CreateIndex(
                name: "IX_Motos_NomePatio",
                table: Motos,
                column: NomePatio);

            migrationBuilder.CreateIndex(
                name: "IX_Motos_UsuarioFuncionario",
                table: Motos,
                column: UsuarioFuncionario);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: Clientes);

            migrationBuilder.DropTable(
                name: Motos);

            migrationBuilder.DropTable(
                name: Funcionarios);

            migrationBuilder.DropTable(
                name: Patios);
        }
    }
}
