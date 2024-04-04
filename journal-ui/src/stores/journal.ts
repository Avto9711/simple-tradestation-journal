import { defineStore } from "pinia";
import { ref } from "vue"
import apiService from '@/apiService';

 const useJournalStore = defineStore('journal',{
  state:()=> ({
    journalBalances:[],
    }),
    actions:{

    }
 })

export default useJournalStore;