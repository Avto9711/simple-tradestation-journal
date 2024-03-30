<script setup lang="ts">
import { useRoute } from 'vue-router'
import useUserStore from '@/stores/user'
const route = useRoute()
if(route.query['code']){

  alert(route.query['code'])  
}
const store  = useUserStore();

</script>

<template>
  <a class="btn btn-outline-light me-2" type="button" href="https://signin.tradestation.com/authorize?response_type=code&client_id={{clientid}}&redirect_uri=http://localhost:8080&audience=https://api.tradestation.com&state=STATE&scope=openid offline_access profile MarketData">Log in TradeStation</a>
</template>

<style scoped>

</style>
