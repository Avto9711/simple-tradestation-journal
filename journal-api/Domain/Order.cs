using System;
using System.Collections.Generic;

namespace journal.api.Domain;

public class Leg
{
    public double Ask { get; set; }
    public string BaseSymbol { get; set; }
    public double Bid { get; set; }
    public double ExecutionPrice { get; set; }
    public int ExecQuantity { get; set; }
    public string ExpireDate { get; set; }
    public double Leaves { get; set; }
    public int LegNumber { get; set; }
    public double LimitPrice { get; set; }
    public string LimitPriceDisplay { get; set; }
    public int Month { get; set; }
    public string OpenOrClose { get; set; }
    public int OrderID { get; set; }
    public string BuyOrSell { get; set; }
    public string AssetType { get; set; }
    public string OrderType { get; set; }
    public double PointValue { get; set; }
    public double PriceUsedForBuyingPower { get; set; }
    public string OptionType { get; set; }
    public double Quantity { get; set; }
    public string Side { get; set; }
    public double StopPrice { get; set; }
    public string StopPriceDisplay { get; set; }
    public double StrikePrice { get; set; }
    public string Symbol { get; set; }
    public string TimeExecuted { get; set; }
    public string Type { get; set; }
    public int Year { get; set; }
}

public class MarketActivationRule
{
    public string RuleType { get; set; }
    public string Symbol { get; set; }
    public string Predicate { get; set; }
    public string TriggerKey { get; set; }
    public string Price { get; set; }
    public string LogicOperator { get; set; }
}

public class TimeActivationRule
{
    public string TimeUtc { get; set; }
}

public class TrailingStop
{
    public double Amount { get; set; }
    public double Percent { get; set; }
}

public class Order
{
    public string AccountID { get; set; }
    public DateTime? ClosedDateTime{get;set;}
    public DateTime? OpenedDateTime{get;set;}
    public string AdvancedOptions { get; set; }
    public string Alias { get; set; }
    public string AssetType { get; set; }
    public double CommissionFee { get; set; }
    public string ContractExpireDate { get; set; }
    public double ConversionRate { get; set; }
    public string CostBasisCalculation { get; set; }
    public string Country { get; set; }
    public string Denomination { get; set; }
    public string DisplayName { get; set; }
    public int DisplayType { get; set; }
    public string Duration { get; set; }
    public double ExecuteQuantity { get; set; }
    public string FilledCanceled { get; set; }
    public double FilledPrice { get; set; }
    public string FilledPriceText { get; set; }
    public string GroupName { get; set; }
    public List<Leg> Legs { get; set; }
    public double LimitPrice { get; set; }
    public string LimitPriceText { get; set; }
    public List<MarketActivationRule> MarketActivationRules { get; set; }
    public double MinMove { get; set; }
    public int OrderID { get; set; }
    public int Originator { get; set; }
    public double Quantity { get; set; }
    public double QuantityLeft { get; set; }
    public double RealizedExpenses { get; set; }
    public string RejectReason { get; set; }
    public string Routing { get; set; }
    public double ShowOnlyQuantity { get; set; }
    public string Spread { get; set; }
    public string Status { get; set; }
    public string StatusDescription { get; set; }
    public double StopPrice { get; set; }
    public string StopPriceText { get; set; }
    public string Symbol { get; set; }
    public List<TimeActivationRule> TimeActivationRules { get; set; }
    public string TimeStamp { get; set; }
    public TrailingStop TrailingStop { get; set; }
    public string TriggeredBy { get; set; }
    public string Type { get; set; }
    public double UnbundledRouteFee { get; set; }
}
