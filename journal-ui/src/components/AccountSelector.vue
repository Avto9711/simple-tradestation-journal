<script setup lang="ts">
import useUserStore from '@/stores/user';
const userStore = useUserStore();

const selectAccount = defineModel()

</script>
<template>
    <select v-model="selectAccount" class="custom-select">
          <option :value="null" :selected="selectAccount === null">Select Account</option>
          <option :value="account.accountId" :key="account.accountId" v-for="account in userStore.accounts" :selected="account.accountId === userStore.selectAccount" value="1">{{ account.accountId }}</option>
    </select>
</template>