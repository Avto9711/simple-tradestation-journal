namespace journal.api.Domain;

public class JournalBalanceMonthBalance (IEnumerable<JournalBalance> balances, IEnumerable<MonthlyBalance> monthlyBalances)
{
    public IEnumerable<JournalBalance> Balances { get; init; } = balances;

    public IEnumerable<MonthlyBalance> MonthlyBalances { get; init; } = monthlyBalances;
}