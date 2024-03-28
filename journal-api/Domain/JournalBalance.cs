namespace journal.api.Domain;

public class JournalBalance(DateOnly balanceDay, double optionsEndingBalance = 0, double stocksEndingBalance = 0)
{
    public DateOnly BalanceDate { get; init; } = balanceDay;

    public double OptionsEndingBalance { get; init; } = optionsEndingBalance;

    public double StocksEndingBalance { get; init; } = stocksEndingBalance;
}