import { defineStore } from "pinia";
import { ref } from "vue"
import apiService from '@/apiService';
import type { JournalBalance } from "@/models";

 const useJournalStore = defineStore('journal',{
  state:()=> ({
    journalBalances:[] as JournalBalance[],
    }),
    actions:{
      async loadAccountJournal(account:string){
       this.journalBalances = await apiService.getAccountHistoricalJournalBalance(account);
      },
      clearJournalBalance(){
        this.journalBalances = [];
      }
    }
 });

export default useJournalStore;