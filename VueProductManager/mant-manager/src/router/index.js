import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Login from '../components/Login.vue'  // Asegúrate de que la ruta sea correcta
import Inicio from '../views/Inicio.vue'  // Asegúrate de que la ruta sea correcta
import Productos from '../views/Productos.vue'  // Asegúrate de que la ruta sea correcta

const routes = [
  {
    path: '/',
    name: 'login',
    component: Login  // Cambiado a Login para que sea la página de inicio
  },
  {
    path: '/home',
    name: 'home',
    component: HomeView
  },
  {
    path: '/inicio',
    name: 'inicio',
    component: Inicio  // Nueva página de inicio después del login
  },
  {
    path: '/productos',
    name: 'productos',
    component: Productos  // Nueva página de productos
  },
  {
    path: '/cerrar-sesion',
    name: 'cerrar-sesion',
    // Aquí puedes redirigir al usuario al login o simplemente cerrar su sesión
  },
  {
    path: '/about',
    name: 'about',
    component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
