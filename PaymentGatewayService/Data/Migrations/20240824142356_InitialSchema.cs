using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentGatewayService.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    PaymentMethod = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TransactionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SourceCardNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DestinationCardNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DestinationAccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PaymentGatewayTrackingCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserIpAddress = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderId = table.Column<long>(type: "bigint", maxLength: 15, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TerminalId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TerminalKey = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PaymentRequestUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VerifyPaymentUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PurchaseUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PurchasePageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GatewayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "CreatedDate", "GatewayName", "MerchantId", "PaymentRequestUrl", "PurchasePageUrl", "PurchaseUrl", "TerminalId", "TerminalKey", "VerifyPaymentUrl" },
                values: new object[] { 1, new DateTime(2024, 8, 24, 17, 53, 55, 823, DateTimeKind.Local).AddTicks(4800), "Sadad", "1818", "api/v0/Request/PaymentRequest", "https://sandbox.banktest.ir/melli/sadad.shaparak.ir/VPG", "Purchase", "53C20VIY", "NzMyMGMyZjA0MDFjYjg2NWU4OGVhMWUy", "api/v0/Advice/Verify" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
