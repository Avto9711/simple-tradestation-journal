namespace journal.api.Domain;

public class MonthlyBalance(string monthName, double monthBBalance )
{
    public string MonthName {get; init; } = monthName;

    public double MonthBalance { get; init; } =  monthBBalance;
}