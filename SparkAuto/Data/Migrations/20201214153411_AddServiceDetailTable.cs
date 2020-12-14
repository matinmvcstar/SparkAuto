using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SparkAuto.Data.Migrations
{
    public partial class AddServiceDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Miles = table.Column<double>(type: "float", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceHeaders_Cars_CardId",
                        column: x => x.CardId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "serviceShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serviceShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_serviceShoppingCarts_Cars_CardId",
                        column: x => x.CardId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_serviceShoppingCarts_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceHeaderId = table.Column<int>(type: "int", nullable: false),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    ServicePrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiseDetails_ServiceHeaders_ServiceHeaderId",
                        column: x => x.ServiceHeaderId,
                        principalTable: "ServiceHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiseDetails_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceHeaders_CardId",
                table: "ServiceHeaders",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_serviceShoppingCarts_CardId",
                table: "serviceShoppingCarts",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_serviceShoppingCarts_ServiceTypeId",
                table: "serviceShoppingCarts",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiseDetails_ServiceHeaderId",
                table: "ServiseDetails",
                column: "ServiceHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiseDetails_ServiceTypeId",
                table: "ServiseDetails",
                column: "ServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "serviceShoppingCarts");

            migrationBuilder.DropTable(
                name: "ServiseDetails");

            migrationBuilder.DropTable(
                name: "ServiceHeaders");
        }
    }
}
