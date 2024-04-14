export interface JournalBalance {
    balanceDate:string;
    overallBalance:number;
    overallNumberOfTrades:number;
    optionsEndingBalance: BalanceData
    stocksEndingBalance: BalanceData;
    comments:string | null
   }

export interface JournalBalanceWithMergePositions extends JournalBalance
{
    mergedTradePositions:OperationWithSymbol[]
}
export interface BalanceData {
    balance:number;
    buyAmount:number;
    sellAmount:number;
    commissions:number;
    numberOfTrades:number;
    trades:Trade[];
   }

export interface OperationWithSymbol extends TradeOperation{

}

export interface Trade
{
    symbol:string;
    assetType:string;
    openingPositions: TradeOperation[];
    closePositions: TradeOperation[];
}

export interface TradeOperation
{
    quantity:number;
    executionPrice:number; 
    closedDateTime?:string
    openedDateTime?:string;
    commission:number;
    openOrClose:string;
}

export interface Account
{
    accountId:string;
    accountType:string;
}

export interface MonthlyBalance
{
    monthName:string;
    monthBalance:number;
}