using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeaconSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialBeaconSync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PESSOAS_VULNERAVEIS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TipoVulnerabilidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NecessidadeEspecial = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PESSOAS_VULNERAVEIS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VOLUNTARIOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    Especialidade = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DisponivelParaAjuda = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VOLUNTARIOS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ENDERECOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PessoaVulneravelId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Logradouro = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Numero = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Bairro = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Distrito = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    TipoImovel = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    TemLajeParaHelicoptero = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Andar = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    EEnderecoAtual = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENDERECOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ENDERECOS_PESSOAS_VULNERAVEIS_PessoaVulneravelId",
                        column: x => x.PessoaVulneravelId,
                        principalTable: "PESSOAS_VULNERAVEIS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESPONSAVEIS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    MoraComVulneravel = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    LogradouroRes = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    NumeroRes = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    BairroRes = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    CidadeRes = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    PessoaVulneravelId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESPONSAVEIS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RESPONSAVEIS_PESSOAS_VULNERAVEIS_PessoaVulneravelId",
                        column: x => x.PessoaVulneravelId,
                        principalTable: "PESSOAS_VULNERAVEIS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESPOSTAS_ALERTAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PessoaVulneravelId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    EnderecoOndeEstaId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    StatusSeguranca = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Observacao = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    DataResposta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESPOSTAS_ALERTAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RESPOSTAS_ALERTAS_PESSOAS_VULNERAVEIS_PessoaVulneravelId",
                        column: x => x.PessoaVulneravelId,
                        principalTable: "PESSOAS_VULNERAVEIS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ENDERECOS_PessoaVulneravelId",
                table: "ENDERECOS",
                column: "PessoaVulneravelId");

            migrationBuilder.CreateIndex(
                name: "IX_RESPONSAVEIS_PessoaVulneravelId",
                table: "RESPONSAVEIS",
                column: "PessoaVulneravelId");

            migrationBuilder.CreateIndex(
                name: "IX_RESPOSTAS_ALERTAS_PessoaVulneravelId",
                table: "RESPOSTAS_ALERTAS",
                column: "PessoaVulneravelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ENDERECOS");

            migrationBuilder.DropTable(
                name: "RESPONSAVEIS");

            migrationBuilder.DropTable(
                name: "RESPOSTAS_ALERTAS");

            migrationBuilder.DropTable(
                name: "VOLUNTARIOS");

            migrationBuilder.DropTable(
                name: "PESSOAS_VULNERAVEIS");
        }
    }
}
