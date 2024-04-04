import { defineStore } from 'pinia'
import { ref } from 'vue'
import apiService from '@/apiService';

export const useUserStore = defineStore('user',{
  state:()=>({
    userEmail:"",
    bearerToken:null,
    accounts:[] as any,
    selectAccount:"Select Account"
  }),
  persist:true,
  actions:{
    async setAccessToken(code:string, redirectUrl:string){
        const response = await apiService.getUserAccessToken(code, redirectUrl);
        this.bearerToken = response.access_token;
    },
    async loadAccounts(){
      const accounts = await apiService.getUserAccounts();
      this.accounts = accounts as any;
    },
    removeAccessToken(){
      this.bearerToken = null;
  },
  setSelectedAccount(account:string){
    this.selectAccount = account;
  },
  }

});
export default useUserStore;
