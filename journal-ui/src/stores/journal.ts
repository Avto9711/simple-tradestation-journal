import { defineStore } from "pinia";
import { ref } from "vue"

 const useJournalStore = defineStore('journal', ()=>{
  const userJournals = ref([]);

	return {userJournals};
})

export default useJournalStore;