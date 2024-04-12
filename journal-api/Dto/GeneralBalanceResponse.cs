using journal.api.Domain;

namespace journal.api.Dto;

public class GeneralBalanceResponse
{
    public IEnumerable<MonthlyBalance> MonthlyBalance { get; set; }

    public IEnumerable<JournalBalance> DailyBalance { get; set; }
}