<script setup lang="ts">
import { useRouter } from 'vue-router'
import useUserStore from '@/stores/user'
import config from '@/config'
import { onMounted } from 'vue';

const store = useUserStore();
const router = useRouter();

const clientId = config.TsClientId;
let queryString = window.location.search;
let urlParams = new URLSearchParams(queryString);
const redirectUrl = "http://localhost:8080/";

onMounted(async ()=>{
  if(urlParams.has('code') ){
   const code = urlParams.get('code');
   await store.setAccessToken(code as string, redirectUrl);
   router.push({name:'journal'})
  }
})


const login = ()=>{
  window.location.href = `https://signin.tradestation.com/authorize?response_type=code&client_id=${clientId}&redirect_uri=${redirectUrl}&audience=https://api.tradestation.com&state=STATE&scope=openid offline_access profile MarketData ReadAccount`;
}

const logOff = () =>{
  store.removeAccessToken();

}
</script>

<template>
  <button v-if="store.bearerToken === null" @click="login()" class="btn btn-outline-light me-2" type="button" href="">Log in TradeStation</button>
  <button v-else @click="logOff()" class="btn btn-outline-light me-2" type="button" href="">Log off</button>
</template>

<style scoped>

</style>
