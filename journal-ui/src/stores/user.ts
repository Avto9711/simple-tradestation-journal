import { defineStore } from 'pinia'
import { ref } from 'vue'


export const useUserStore = defineStore('user',{
  state:()=>({
    userEmail:ref("")
  }),
  actions:{
    async getAccessToken(code:string ):Promise<string>{
        //call api auth endpoint
        return "access-token"
    }
  }

});
export default useUserStore;
