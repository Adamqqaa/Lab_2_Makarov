using Microsoft.EntityFrameworkCore;

namespace Lab_2.Data;


public static class TradeContextFactory
{
    // Строка подключения к SQL Server Express (см. Data\create_trade_db.sql)
    public const string ConnectionString =
        @"Data Source=.\SQLEXPRESS; Initial Catalog=Trade; Integrated Security=true; TrustServerCertificate=true";

    public static TradeContext Create()
    {
        var options = new DbContextOptionsBuilder<TradeContext>()
            .UseSqlServer(ConnectionString)
            .Options;

        return new TradeContext(options);
    }
}