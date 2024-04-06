export interface JournalBalance {
    balanceDate:string;
    overallBalance:number;
    overallNumberOfTrades:number;
    optionsEndingBalance: BalanceData
    stocksEndingBalance: BalanceData;
    comments:string | null
   }
  
export interface BalanceData {
    balance:number;
    buyAmount:number;
    sellAmount:number;
    commissions:number;
    numberOfTrades:number;
   }

export interface Account
{
accountId:string
accountType:string
}