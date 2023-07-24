using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenIdConnectIntegrationExample.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OIDCConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "varchar(36)", nullable: false),
                    authority = table.Column<string>(type: "varchar(256)", nullable: true),
                    audience = table.Column<string>(type: "varchar(256)", nullable: true),
                    callback_uri = table.Column<string>(type: "varchar(256)", nullable: true),
                    client_id = table.Column<string>(type: "varchar(36)", nullable: true),
                    redirect_uri = table.Column<string>(type: "varchar(256)", nullable: true),
                    response_type = table.Column<string>(type: "varchar(256)", nullable: true),
                    scope = table.Column<string>(type: "varchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OIDCConfigurations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OIDCConfigurations");
        }
    }
}
