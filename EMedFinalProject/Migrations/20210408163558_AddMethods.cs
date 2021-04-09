using Microsoft.EntityFrameworkCore.Migrations;

namespace EMedFinalProject.Migrations
{
    public partial class AddMethods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Methods",
                columns: table => new
                {
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Methods", x => x.PaymentMethodID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodID",
                table: "Orders",
                column: "PaymentMethodID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Methods_PaymentMethodID",
                table: "Orders",
                column: "PaymentMethodID",
                principalTable: "Methods",
                principalColumn: "PaymentMethodID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Methods_PaymentMethodID",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Methods");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentMethodID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentMethodID",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
