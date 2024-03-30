namespace journal.api.Domain;

public class JournalBalance(DateOnly balanceDay, BalanceData optionsEndingBalance, BalanceData stocksEndingBalance)
{
    public DateOnly BalanceDate { get; init; } = balanceDay;

    public BalanceData OptionsEndingBalance { get; init; } = optionsEndingBalance;

    public BalanceData StocksEndingBalance { get; init; } = stocksEndingBalance;
}