using journal.api.Domain;

namespace journal.api.service;

public interface IJournalService
{
    Task<JournalBalance> CalculateTodayBalance(string account);

    Task<IEnumerable<JournalBalance>> CalculateHistoricalBalance(string account, DateOnly? since, bool includeTodays = true);
}

public class JournalService : IJournalService
{
    private readonly TradeStationService _tradeStationService;

    public JournalService(TradeStationService tradeStationService)
    {
        _tradeStationService = tradeStationService;
    }

    public async Task<JournalBalance> CalculateTodayBalance(string account)
    {
        var orders = await _tradeStationService.GetTodayOrders(account);
        var orderDate = DateOnly.FromDateTime(DateTime.Now);
        return new JournalBalance(orderDate, CalculateOrdersAssetTypeBalance(orders, "STOCKOPTION",orderDate), 
                                             CalculateOrdersAssetTypeBalance(orders,"STOCK", orderDate));
    }

    public async Task<IEnumerable<JournalBalance>> CalculateHistoricalBalance(string account, DateOnly? since = null, bool includeTodays = true)
    {
        var ordersSince = since ?? GetThreeMonthsAgoDate();
        var orders = await _tradeStationService.GetHistoricOrders(account, ordersSince);

        var groupedByDate = orders.Where(y=> y.ClosedDateTime is not null).GroupBy(y=>DateOnly.FromDateTime(y.ClosedDateTime.Value));
        var historicalJournal = groupedByDate.Select(y=> new JournalBalance(y.Key, CalculateOrdersAssetTypeBalance(y, "STOCKOPTION", y.Key), 
                                                                                 CalculateOrdersAssetTypeBalance(y,"STOCK", y.Key)));
        if(includeTodays == false)
            return historicalJournal;
        
        var todaysResult = await CalculateTodayBalance(account);
        return historicalJournal.Prepend(todaysResult);
    }

    private BalanceData CalculateOrdersAssetTypeBalance(IEnumerable<Order> orders, string assetType, DateOnly ordersDate)
    {
        var isOptions = assetType == "STOCKOPTION";
        var dateOrders = orders.Where(o => o.Legs.Any(l => l.AssetType == assetType) && DateOnly.FromDateTime(o.ClosedDateTime.Value) == ordersDate);
        var filledOrders = dateOrders.Where(y=> y.Status == "FLL");

        var boughtOrdersTotal = filledOrders.Where(x => x.Legs.Any(y=>y.BuyOrSell == "Buy")).Sum(x => x.FilledPrice) * (isOptions ? 100:1);
        var soldOrdersTotal = filledOrders.Where(x => x.Legs.Any(y=>y.BuyOrSell == "Sell")).Sum(x => x.FilledPrice) * (isOptions ? 100:1);
        var commissions = dateOrders.Sum(y=>y.CommissionFee);
        
        return new BalanceData(
                        sellAmount: Math.Round(soldOrdersTotal,2), 
                        buyAmount: Math.Round(boughtOrdersTotal,2),
                        commissions:Math.Round(commissions,2),
                        numberOfTrades: (filledOrders.Count() / 2));
    }

    private DateOnly GetThreeMonthsAgoDate()=> DateOnly.FromDateTime(DateTime.Now.AddMonths(-3).AddDays(2));
}

