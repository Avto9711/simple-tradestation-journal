import { defineStore } from 'pinia'
import { ref } from 'vue'
import apiService from '@/apiService';

export const useUserStore = defineStore('user',{
  state:()=>({
    userEmail:"",
    bearerToken:null
  }),
  persist:true,
  actions:{
    async setAccessToken(code:string, redirectUrl:string){
        const response = await apiService.getUserAccessToken(code, redirectUrl);
        this.bearerToken = response.access_token;
    }
  }

});
export default useUserStore;
