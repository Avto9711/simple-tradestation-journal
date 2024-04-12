using System.Globalization;
using journal.api.Domain;
using journal.api.Dto;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace journal.api.service;

public interface IJournalService
{
    Task<JournalBalance> CalculateTodayBalance(string account);

    Task<IEnumerable<JournalBalance>> CalculateHistoricalBalance(string account, DateOnly? since, bool includeTodays = true);

    Task<GeneralBalanceResponse> GetJournalGeneralBalance(string account);

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

    public async Task<JournalBalanceMonthBalance> CalculateHistoricalBalanceWithMothsBalance(string account, DateOnly? since = null, bool includeTodays = true)
    {
        var lastThreeMonthsOrders = await CalculateHistoricalBalance(account, since,includeTodays);

        var lastMonthsBalance = lastThreeMonthsOrders
        .GroupBy(x=>x.BalanceDate.Month)
        .Select(groupedOrders => new MonthlyBalance(groupedOrders.Key.ToString("MMM"), 
                                                    groupedOrders.Sum(x=>x.OverallBalance)));

        return new JournalBalanceMonthBalance(monthlyBalances:lastMonthsBalance, balances:lastThreeMonthsOrders);
    }

    private BalanceData CalculateOrdersAssetTypeBalance(IEnumerable<Order> orders, string assetType, DateOnly ordersDate)
    {
        var isOptions = assetType == "STOCKOPTION";
        var dateOrders = orders.Where(o => o.Legs.Any(l => l.AssetType == assetType) && DateOnly.FromDateTime(o.ClosedDateTime.Value) == ordersDate);
        var filledOrders = dateOrders.Where(y=> y.Status == "FLL");

        var trades = dateOrders
        .SelectMany(order => order.Legs, (o,l)=> new {order=o, leg=l})
        .GroupBy(x=> new { x.leg.Symbol })
        .Select(ol =>
            new Trade 
                { 
                    OpeningPositions = ol.Where(y=>y.leg.OpenOrClose == "Open").OrderByDescending(m=>m.order.OpenedDateTime).Select(openOrderLegs => GetTradeOperationFromLegAndOrder(openOrderLegs.leg, openOrderLegs.order)), 
                    ClosePositions = ol.Where(y=>y.leg.OpenOrClose == "Close").OrderByDescending(m=>m.order.OpenedDateTime).Select(openOrderLegs => GetTradeOperationFromLegAndOrder(openOrderLegs.leg, openOrderLegs.order)),
                    Symbol = ol.Key.Symbol,
                    AssetType = assetType
                }
        );
        var multiplier = (isOptions ? 100:1);
        var boughtOrdersTotal = trades.Sum(y=>y.OpeningPositionTotalAmount) * multiplier;
        var soldOrdersTotal = trades.Sum(y=>y.ClosingPositionTotalAmount) * multiplier;
        var commissions = trades.Sum(y=>y.TradeEndingCommission);
        var balanceData = new BalanceData(
                        sellAmount: Math.Round(soldOrdersTotal,2), 
                        buyAmount: Math.Round(boughtOrdersTotal,2),
                        commissions:Math.Round(commissions,2),
                        numberOfTrades: trades.Count());
        
        balanceData.Trades = trades;

        return balanceData;
    }

    public async Task<GeneralBalanceResponse> GetJournalGeneralBalance(string account)
    {
        var balances = await CalculateHistoricalBalance(account);
        var monthlyBalances = balances
        .GroupBy(y=>y.BalanceDate.Month)
        .Select(y=> new MonthlyBalance (
                CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(y.Key),
                y.Sum(y=>y.OverallBalance)));
        return new GeneralBalanceResponse 
        {
            MonthlyBalance = monthlyBalances,
            DailyBalance = balances
        };
    }

    private static TradeOperation GetTradeOperationFromLegAndOrder(Leg orderLeg, Order order){
        return new TradeOperation
        {
            Quantity = orderLeg.ExecQuantity,
            Commission = order.CommissionFee,
            ClosedDateTime = order.ClosedDateTime,
            ExecutionPrice = orderLeg.ExecutionPrice,
            OpenedDateTime = order.OpenedDateTime
        };
    }

    private DateOnly GetThreeMonthsAgoDate()=> DateOnly.FromDateTime(DateTime.Now.AddMonths(-3).AddDays(2));
}

