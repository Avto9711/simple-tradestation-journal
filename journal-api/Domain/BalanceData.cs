namespace journal.api.Domain;

public class BalanceData(double buyAmount = 0, double sellAmount = 0, double commissions = 0, int numberOfTrades = 0)
{
    public double BuyAmount { get; init; } = buyAmount;
    public double SellAmount { get; init; } = sellAmount;
    public double Commissions { get; init; } = commissions;
    public int NumberOfTrades  { get; init; } = numberOfTrades;
    public IEnumerable<Trade> Trades { get; set; }

    public double Balance { get{
        return SellAmount - BuyAmount - Commissions;
    } }

}   