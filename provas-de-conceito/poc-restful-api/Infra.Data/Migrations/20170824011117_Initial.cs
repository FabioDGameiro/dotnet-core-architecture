#region Using

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Usuarios",
                table => new
                {
                    Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                    DataNascimento = table.Column<DateTime>("datetime2", nullable: true),
                    Email = table.Column<string>("varchar(30)", nullable: true),
                    Nome = table.Column<string>("varchar(20)", nullable: false),
                    Sexo = table.Column<int>("int", nullable: true),
                    Sobrenome = table.Column<string>("varchar(30)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Usuarios", x => x.Id); });

            migrationBuilder.CreateTable(
                "UsuariosEnderecos",
                table => new
                {
                    Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                    Complemento = table.Column<string>("varchar(20)", nullable: true),
                    Estado = table.Column<string>("varchar(2)", nullable: false),
                    Logradouro = table.Column<string>("varchar(100)", nullable: false),
                    Numero = table.Column<string>("varchar(10)", nullable: false),
                    Tipo = table.Column<int>("int", nullable: false),
                    UsuarioId = table.Column<Guid>("uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosEnderecos", x => x.Id);
                    table.ForeignKey(
                        "FK_UsuariosEnderecos_Usuarios_UsuarioId",
                        x => x.UsuarioId,
                        "Usuarios",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_UsuariosEnderecos_UsuarioId",
                "UsuariosEnderecos",
                "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "UsuariosEnderecos");

            migrationBuilder.DropTable(
                "Usuarios");
        }
    }
}