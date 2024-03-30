namespace journal.api.Domain;

public class BalanceData(double balance = 0, double buyAmount = 0, double sellAmount = 0, double commissions = 0)
{

    public double Balance { get; init; } = balance;
    public double BuyAmount { get; init; } = buyAmount;
    public double SellAmount { get; init; } = sellAmount;
    public double Commissions { get; init; } = commissions;

}