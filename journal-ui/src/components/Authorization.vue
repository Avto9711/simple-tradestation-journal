<script setup lang="ts">
import { useRoute } from 'vue-router'
import useUserStore from '@/stores/user'
import config from '@/config'
const route = useRoute()

const store = useUserStore();

const clientId = config.TsClientId;

const code = route.query['code'];
if(code){
  store.getAccessToken(code as string);
}
const login = ()=>{
  window.location.href = `https://signin.tradestation.com/authorize?response_type=code&client_id=${clientId}&redirect_uri=http://localhost:8080&audience=https://api.tradestation.com&state=STATE&scope=openid offline_access profile MarketData`;
}
</script>

<template>
  <button @click="login()" class="btn btn-outline-light me-2" type="button" href="">Log in TradeStation</button>
</template>

<style scoped>

</style>
