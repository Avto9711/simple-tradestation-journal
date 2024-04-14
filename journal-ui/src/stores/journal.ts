import { defineStore } from "pinia";
import apiService from '@/apiService';
import type { JournalBalance, MonthlyBalance } from "@/models";

 const useJournalStore = defineStore('journal',{
  state:()=> ({
    journalBalances:[] as JournalBalance[],
    monthlyBalance:[] as MonthlyBalance[],
    }),
    persist:true,
    actions:{
      async loadAccountJournal(account:string){
       const {monthlyBalance, dailyBalance} = await apiService.getAccountGeneralBalance(account);
       this.monthlyBalance = monthlyBalance;
       this.journalBalances = dailyBalance;
      },
      clearJournalBalance(){
        this.journalBalances = []; 
        this.monthlyBalance = [];
      }
    },
    getters:{
      journalTotal: (state)=> state.journalBalances.length,
      journalBalancesMergedTrades: (state) => state.journalBalances.map(journalBalance=>{
        const optionsTradeWithMergedPositions = journalBalance
        .optionsEndingBalance
        .trades
        .map(trade=>trade.openingPositions.concat(trade.closePositions)
        .map(position=>({...position, symbol: trade.symbol, assetType: trade.assetType})))

        const stockTradeWithMergedPositions = journalBalance
        .stocksEndingBalance
        .trades
        .map(trade=>trade.openingPositions.concat(trade.closePositions)
        .map(position=>({...position, symbol: trade.symbol, assetType: trade.assetType})))
     
        return {...journalBalance, mergedTradePositions: optionsTradeWithMergedPositions.concat(stockTradeWithMergedPositions).flat() };
      })
    }
 });

export default useJournalStore;