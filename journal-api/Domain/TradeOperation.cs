namespace journal.api.Domain;

public class TradeOperation
{
    public int Quantity { get; set; }

    public double ExecutionPrice { get; set; }

    public DateTime? ClosedDateTime{get;set;}

    public DateTime? OpenedDateTime{get;set;}

    public double Commission { get; set; }
}