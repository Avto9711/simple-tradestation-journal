<script setup lang="ts">
import { useRoute } from 'vue-router'
import useUserStore from '@/stores/user'
import config from '@/config'

const store = useUserStore();

const clientId = config.TsClientId;
let queryString = window.location.search;
let urlParams = new URLSearchParams(queryString);
const redirectUrl = "http://localhost:8080/";
debugger
if(urlParams.has('code') ){
  const code = urlParams.get('code');
  await store.setAccessToken(code as string, redirectUrl);

  }

const login = ()=>{
  window.location.href = `https://signin.tradestation.com/authorize?response_type=code&client_id=${clientId}&redirect_uri=${redirectUrl}&audience=https://api.tradestation.com&state=STATE&scope=openid offline_access profile MarketData`;
}
</script>

<template>
  <button @click="login()" class="btn btn-outline-light me-2" type="button" href="">Log in TradeStation</button>
</template>

<style scoped>

</style>
