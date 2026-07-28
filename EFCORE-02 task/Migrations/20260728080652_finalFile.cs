using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCORE_02_task.Migrations
{
    /// <inheritdoc />
    public partial class finalFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_orders_OrderId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_products_ProductId",
                table: "OrderProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct");

            migrationBuilder.RenameTable(
                name: "OrderProduct",
                newName: "ordersProducts");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProduct_OrderId",
                table: "ordersProducts",
                newName: "IX_ordersProducts_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ordersProducts",
                table: "ordersProducts",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ordersProducts_orders_OrderId",
                table: "ordersProducts",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ordersProducts_products_ProductId",
                table: "ordersProducts",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordersProducts_orders_OrderId",
                table: "ordersProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ordersProducts_products_ProductId",
                table: "ordersProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ordersProducts",
                table: "ordersProducts");

            migrationBuilder.RenameTable(
                name: "ordersProducts",
                newName: "OrderProduct");

            migrationBuilder.RenameIndex(
                name: "IX_ordersProducts_OrderId",
                table: "OrderProduct",
                newName: "IX_OrderProduct_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_orders_OrderId",
                table: "OrderProduct",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_products_ProductId",
                table: "OrderProduct",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
