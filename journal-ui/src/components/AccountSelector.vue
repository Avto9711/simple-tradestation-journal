<script setup lang="ts">
import useUserStore from '@/stores/user';
import { storeToRefs } from 'pinia';
import { onMounted } from 'vue';
const userStore = useUserStore();
onMounted(async ()=>{
    if(userStore.bearerToken !== null && !userStore.accounts.length){
        await userStore.loadAccounts(); 
        console.log(userStore.accounts);
    }
})

</script>
<template>
    <select v-model="userStore.selectAccount" class="custom-select">
          <option :selected="userStore.selectAccount === 'Select Account'">Select Account</option>
          <option :value="account.accountId" :key="account.accountId" v-for="account in userStore.accounts" :selected="account.accountId === userStore.selectAccount" value="1">{{ account.accountId }}</option>
    </select>
</template>