import { createApp } from 'vue';
import './assets/styles/default.css';
import App from './App.vue';
import { store } from "./store";


createApp(App)
  .use(store)
  .mount('#app')
