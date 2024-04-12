namespace journal.api.Domain;

public class MonthlyBalance(string monthName, double monthBalance )
{
    public string MonthName {get; init; } = monthName;

    public double MonthBalance { get; init; } =  monthBalance;
}