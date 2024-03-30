import { defineStore } from 'pinia'
import { ref } from 'vue'


export const useUserStore = defineStore('user',()=>{
  const userEmail = ref("")
	return {userEmail};	

});
export default useUserStore;
