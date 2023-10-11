using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenIdConnectIntegrationExample.Database.Migrations
{
    /// <inheritdoc />
    public partial class CertificatesAndClientSecretAndWebKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "certificatePassword",
                table: "OIDCConfigurations",
                type: "varchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "certificateSerial",
                table: "OIDCConfigurations",
                type: "varchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_secret",
                table: "OIDCConfigurations",
                type: "varchar(36)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "jsonWebKey",
                table: "OIDCConfigurations",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "certificatePassword",
                table: "OIDCConfigurations");

            migrationBuilder.DropColumn(
                name: "certificateSerial",
                table: "OIDCConfigurations");

            migrationBuilder.DropColumn(
                name: "client_secret",
                table: "OIDCConfigurations");

            migrationBuilder.DropColumn(
                name: "jsonWebKey",
                table: "OIDCConfigurations");
        }
    }
}
