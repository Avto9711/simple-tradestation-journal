namespace journal.api.Domain;

public class JournalBalance(DateOnly balanceDay, BalanceData optionsEndingBalance, BalanceData stocksEndingBalance, string comments = null)
{
    public DateOnly BalanceDate { get; init; } = balanceDay;

    public BalanceData OptionsEndingBalance { get; init; } = optionsEndingBalance;

    public BalanceData StocksEndingBalance { get; init; } = stocksEndingBalance;

    public double OverallBalance => this.OptionsEndingBalance.Balance + this.StocksEndingBalance.Balance;

    public double OverallNumberOfTrades => this.OptionsEndingBalance.NumberOfTrades + this.StocksEndingBalance.NumberOfTrades;
 
    public string Comments { get; init; } = comments;
}