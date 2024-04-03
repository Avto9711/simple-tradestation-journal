// Import our custom CSS
import * as bootstrap from 'bootstrap'
import './scss/styles.scss'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'

// Import all of Bootstrap's JS

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

const app = createApp(App)

const pinia = createPinia();
pinia.use(piniaPluginPersistedstate)

app.use(pinia)
app.use(router)

app.mount('#app')
