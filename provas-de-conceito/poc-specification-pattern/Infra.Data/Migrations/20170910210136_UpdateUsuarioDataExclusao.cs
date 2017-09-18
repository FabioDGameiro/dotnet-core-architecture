#region Using

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace Infra.Data.Migrations
{
    public partial class UpdateUsuarioDataExclusao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                "DataExclusao",
                "Usuarios",
                "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "DataExclusao",
                "Usuarios");
        }
    }
}