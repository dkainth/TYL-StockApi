using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApi.Repository.Migrations
{
    public partial class ManualDataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT Brokers ON;
                INSERT INTO Brokers (Id, Name) VALUES(1, 'Broker Bob');
                INSERT INTO Brokers (Id, Name) VALUES(2, 'Broker4u');
                SET IDENTITY_INSERT Brokers OFF;
            ");

            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT Stocks ON;
                INSERT INTO Stocks (Id, Symbol, Name) VALUES(1, 'AAPL', 'Apple');
                INSERT INTO Stocks (Id, Symbol, Name) VALUES(2, 'BA.', 'British Airways');
                SET IDENTITY_INSERT Stocks OFF;
            ");

            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT Transactions ON;
                INSERT INTO Transactions (Id, StockId, PriceGbp, Quantity, BrokerId) VALUES(1, 1, 100.23, 45, 1);
                INSERT INTO Transactions (Id, StockId, PriceGbp, Quantity, BrokerId) VALUES(2, 2, 1.09, 100, 1);
                INSERT INTO Transactions (Id, StockId, PriceGbp, Quantity, BrokerId) VALUES(3, 1, 105.00, 10, 2);
                SET IDENTITY_INSERT Transactions OFF;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM Brokers WHERE Id In (1,2);");

            migrationBuilder.Sql(@"DELETE FROM Stocks WHERE Id In (1,2);");

            migrationBuilder.Sql(@"DELETE FROM StockTransactions WHERE Id In (1,2,3);");
        }
    }
}
