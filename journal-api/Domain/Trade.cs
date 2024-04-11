
namespace journal.api.Domain;

public class Trade
{
    public string Symbol { get; set; } 
    public string AssetType { get; set; } 

    public IEnumerable<TradeOperation> OpeningPositions { get; set; }

    public IEnumerable<TradeOperation> ClosePositions { get; set; }


    public double OpeningPositionTotalAmount  { 
        get 
        {
            return OpeningPositions.Sum(y=>y.ExecutionPrice * y.Quantity);
        }
     }

    public double ClosingPositionTotalAmount  { 
        get 
        {
            return ClosePositions.Sum(y=>y.ExecutionPrice * y.Quantity);
        }
    
     }
    public double TradeEndingBalance 
    { 
        get 
        {
            return  ClosingPositionTotalAmount - OpeningPositionTotalAmount; 
        } 
    }

    public double TradeEndingCommission { 
        get 
        {
            return  OpeningPositions.Sum(y=>y.Commission) + ClosePositions.Sum(y=>y.Commission);
        }
     }
}