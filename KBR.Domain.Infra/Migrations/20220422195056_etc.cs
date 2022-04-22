using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KBR.Domain.Infra.Migrations
{
    public partial class etc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_orderStatus_statusId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_orders_orderId",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_orderId",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_orders_statusId",
                table: "orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "statusId",
                table: "orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "statusId",
                table: "orders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_payments_orderId",
                table: "payments",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_statusId",
                table: "orders",
                column: "statusId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_orderStatus_statusId",
                table: "orders",
                column: "statusId",
                principalTable: "orderStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_orders_orderId",
                table: "payments",
                column: "orderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
