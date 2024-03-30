// Import our custom CSS
import * as bootstrap from 'bootstrap'
import './scss/styles.scss'

// Import all of Bootstrap's JS

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

const app = createApp(App)

app.use(createPinia())
app.use(router)

app.mount('#app')
