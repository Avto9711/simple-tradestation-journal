import { defineStore } from 'pinia'
import { ref } from 'vue'
import apiService from '@/apiService';
import type { Account } from '@/models';

export const useUserStore = defineStore('user',{
  state:()=>({
    userEmail:"",
    bearerToken:null,
    accounts:[] as Account[],
  }),
  getters:{
    isUserLoggedIn: (state)=> state.bearerToken != null
  },
  persist:true,
  actions:{
    async setAccessToken(code:string, redirectUrl:string){
        const response = await apiService.getUserAccessToken(code, redirectUrl);
        this.bearerToken = response.access_token;
    },
    async loadAccounts(){
      const accounts = await apiService.getUserAccounts();
      this.accounts = accounts as Account[];
    },
    resetStore(){
      this.$reset();
    }
  }

});
export default useUserStore;
