using journal.api.Domain;

namespace journal.api.service;

public interface IJournalService
{
    Task<JournalBalance> CalculateTodayBalance(string account);

    Task<IEnumerable<JournalBalance>> CalculateHistoricalBalance(string account, DateOnly? since);
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

    public async Task<IEnumerable<JournalBalance>> CalculateHistoricalBalance(string account, DateOnly? since = null)
    {
        var ordersSince = since ?? GetThreeMonthsAgoDate();
        var orders = await _tradeStationService.GetHistoricOrders(account, ordersSince);

        var orderByDate = orders.Where(y=> y.ClosedDateTime is not null).GroupBy(y=>DateOnly.FromDateTime(y.ClosedDateTime.Value));
        var historicalJournal = orderByDate.Select(y=> new JournalBalance(y.Key, CalculateOrdersAssetTypeBalance(y, "STOCKOPTION", y.Key), 
                                                                                 CalculateOrdersAssetTypeBalance(y,"STOCK", y.Key)));
        return historicalJournal;
    }

    private BalanceData CalculateOrdersAssetTypeBalance(IEnumerable<Order> orders, string assetType, DateOnly ordersDate)
    {
        var isOptions = assetType == "STOCKOPTION";
        var dateOrders = orders.Where(o => o.Legs.Any(l => l.AssetType == assetType) && DateOnly.FromDateTime(o.ClosedDateTime.Value) == ordersDate);
        var filledOrders = dateOrders.Where(y=> y.Status == "FLL");

        var boughtOrdersTotal = filledOrders.Where(x => x.Legs.Any(y=>y.BuyOrSell == "Buy")).Sum(x => x.FilledPrice) * (isOptions ? 100:1);
        var soldOrdersTotal = filledOrders.Where(x => x.Legs.Any(y=>y.BuyOrSell == "Sell")).Sum(x => x.FilledPrice) * (isOptions ? 100:1);
        var commissions = dateOrders.Sum(y=>y.CommissionFee);
        var balance = ((soldOrdersTotal - boughtOrdersTotal) - commissions);
        
        return new BalanceData(
                        balance:balance, 
                        sellAmount: soldOrdersTotal, 
                        buyAmount: boughtOrdersTotal,
                        commissions:commissions);
    }

    private DateOnly GetThreeMonthsAgoDate()=> DateOnly.FromDateTime(DateTime.Now.AddMonths(-3).AddDays(2));
}

