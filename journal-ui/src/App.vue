<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import HelloWorld from './components/HelloWorld.vue'
import Authorization from './components/Authorization.vue'
import AccountSelector from './components/AccountSelector.vue'
import useJournalStore from './stores/journal'

import useUserStore from './stores/user'
import { ref, watch } from 'vue';
const accountSelected = ref(null);
const journalStore = useJournalStore();
const userStore = useUserStore();


watch(accountSelected, async (value)=>{
  if(value){
    userStore.setAccountSelected(value);
    await journalStore.loadAccountJournal(value);
  }else{
    journalStore.clearJournalBalance();
  }
})
</script>

<template>
  <header class="p-3 text-bg-dark">
    <div class="container">
      <div class="d-flex flex-wrap align-items-center justify-content-center justify-content-lg-start">
        <a href="/" class="d-flex align-items-center mb-2 mb-lg-0 text-white text-decoration-none">
          <svg class="bi me-2" width="40" height="32" role="img" aria-label="Bootstrap"><use xlink:href="#bootstrap"></use></svg>
        </a>

        <ul class="nav col-12 col-lg-auto me-lg-auto mb-2 justify-content-center mb-md-0">
          <li> <RouterLink class="nav-link px-2 text-white" to="/">Home</RouterLink></li>
          <li><RouterLink v-if="userStore.isUserLoggedIn"  class="nav-link px-2 text-white" to="/journal">User</RouterLink></li>
          <li><RouterLink v-if="userStore.isUserLoggedIn" class="nav-link px-2 text-white" to="/journal">Journal</RouterLink></li>
          <li><RouterLink class="nav-link px-2 text-white" to="/about">About</RouterLink></li>
        </ul>
        <div class="text-end  ">
        <AccountSelector v-if="userStore.isUserLoggedIn"  v-model="accountSelected" />
        <Authorization />

        </div>
      </div>
    </div>
  </header>
  <main class="container">
      <RouterView />
  </main>
</template>

<style scoped>

</style>
