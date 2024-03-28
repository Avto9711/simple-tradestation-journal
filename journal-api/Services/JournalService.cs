using journal.api.Domain;

namespace journal.api.service;

public interface IJournalService
{
    Task<JournalBalance> CalculateTodayBalance(string account);

    Task<IEnumerable<JournalBalance>> CalculateHistoricalBalance(string account, DateOnly since);
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
        var optionOrdersBalance = CalculateOptionsOrdersBalance(orders);

        return new JournalBalance(DateOnly.FromDateTime(DateTime.Now), optionOrdersBalance, 0);
    }

    public async Task<IEnumerable<JournalBalance>> CalculateHistoricalBalance(string account, DateOnly since)
    {
        var orders = await _tradeStationService.GetHistoricOrders(account, since);

        var orderByDate = orders.Where(y=> y.ClosedDateTime is not null).GroupBy(y=>DateOnly.FromDateTime(y.ClosedDateTime.Value));
        var historicalJournal = orderByDate.Select(y=> new JournalBalance(y.Key, CalculateOptionsOrdersBalance(y)));
        return historicalJournal;
    }

    private double CalculateOptionsOrdersBalance(IEnumerable<Order> orders)
    {
        var optionOrders = orders.Where(o => o.Legs.Any(l => l.OptionType is not null));
        var filledOrders = optionOrders.Where(y=> y.Status == "FLL");

        var boughtOrdersTotal = filledOrders.Where(x => x.Legs.Any(y=>y.BuyOrSell == "Buy")).Sum(x => x.FilledPrice);
        var soldOrdersTotal = filledOrders.Where(x => x.Legs.Any(y=>y.BuyOrSell == "Sell")).Sum(x => x.FilledPrice);
        var commissions = optionOrders.Sum(y=>y.CommissionFee);
        return ((soldOrdersTotal - boughtOrdersTotal) * 100) - commissions ;
    }
}

