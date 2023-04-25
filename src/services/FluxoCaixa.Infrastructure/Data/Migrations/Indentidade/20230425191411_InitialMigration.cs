using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxoCaixa.Infrastructure.Data.Migrations.Indentidade
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NORMALIZEDNAME = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CONCURRENCYSTAMP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASPNETROLES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    USERNAME = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NORMALIZEDUSERNAME = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NORMALIZEDEMAIL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EMAILCONFIRMED = table.Column<bool>(type: "bit", nullable: false),
                    PASSWORDHASH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SECURITYSTAMP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CONCURRENCYSTAMP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHONENUMBER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHONENUMBERCONFIRMED = table.Column<bool>(type: "bit", nullable: false),
                    TWOFACTORENABLED = table.Column<bool>(type: "bit", nullable: false),
                    LOCKOUTEND = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LOCKOUTENABLED = table.Column<bool>(type: "bit", nullable: false),
                    ACCESSFAILEDCOUNT = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASPNETUSERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLE_CLAIMS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ROLEID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CLAIMTYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CLAIMVALUE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASPNETROLECLAIMS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ASPNETROLECLAIMS_ASPNETROLES_ROLEID",
                        column: x => x.ROLEID,
                        principalTable: "ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_CLAIMS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USERID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CLAIMTYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CLAIMVALUE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASPNETUSERCLAIMS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ASPNETUSERCLAIMS_ASPNETUSERS_USERID",
                        column: x => x.USERID,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_LOGINS",
                columns: table => new
                {
                    LOGINPROVIDER = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PROVIDERKEY = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PROVIDERDISPLAYNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USERID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASPNETUSERLOGINS", x => new { x.LOGINPROVIDER, x.PROVIDERKEY });
                    table.ForeignKey(
                        name: "FK_ASPNETUSERLOGINS_ASPNETUSERS_USERID",
                        column: x => x.USERID,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_ROLES",
                columns: table => new
                {
                    USERID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ROLEID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASPNETUSERROLES", x => new { x.USERID, x.ROLEID });
                    table.ForeignKey(
                        name: "FK_ASPNETUSERROLES_ASPNETROLES_ROLEID",
                        column: x => x.ROLEID,
                        principalTable: "ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ASPNETUSERROLES_ASPNETUSERS_USERID",
                        column: x => x.USERID,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_TOKENS",
                columns: table => new
                {
                    USERID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LOGINPROVIDER = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VALUE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASPNETUSERTOKENS", x => new { x.USERID, x.LOGINPROVIDER, x.NAME });
                    table.ForeignKey(
                        name: "FK_ASPNETUSERTOKENS_ASPNETUSERS_USERID",
                        column: x => x.USERID,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_CLAIMS_ROLEID",
                table: "ROLE_CLAIMS",
                column: "ROLEID");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "ROLES",
                column: "NORMALIZEDNAME",
                unique: true,
                filter: "[NORMALIZEDNAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_USER_CLAIMS_USERID",
                table: "USER_CLAIMS",
                column: "USERID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_LOGINS_USERID",
                table: "USER_LOGINS",
                column: "USERID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLES_ROLEID",
                table: "USER_ROLES",
                column: "ROLEID");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "USERS",
                column: "NORMALIZEDEMAIL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "USERS",
                column: "NORMALIZEDUSERNAME",
                unique: true,
                filter: "[NORMALIZEDUSERNAME] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ROLE_CLAIMS");

            migrationBuilder.DropTable(
                name: "USER_CLAIMS");

            migrationBuilder.DropTable(
                name: "USER_LOGINS");

            migrationBuilder.DropTable(
                name: "USER_ROLES");

            migrationBuilder.DropTable(
                name: "USER_TOKENS");

            migrationBuilder.DropTable(
                name: "ROLES");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
