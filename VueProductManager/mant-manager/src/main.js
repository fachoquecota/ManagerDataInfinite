import { createApp } from 'vue';
import App from './App.vue';
import './registerServiceWorker';
import router from './router';
import 'tailwindcss/tailwind.css';  // Importa Tailwind CSS

// Importaciones para FontAwesome
import { library } from '@fortawesome/fontawesome-svg-core';
import { faBox, faUsers, faCog, faChevronDown } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

// Añade los iconos a la librería de FontAwesome
library.add(faBox, faUsers, faCog, faChevronDown);

// Crea la aplicación Vue
const app = createApp(App);

// Registra el componente FontAwesomeIcon globalmente
app.component('font-awesome-icon', FontAwesomeIcon);

// Usa el enrutador
app.use(router);

// Monta la aplicación
app.mount('#app');
