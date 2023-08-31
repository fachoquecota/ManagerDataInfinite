<template>
  <div class="flex h-screen bg-gray-100">
    <BarraLateral :menu="menu" :is-dropdown-list="false" v-if="mostrarComponentes" />
    <div class="flex-1">
      <router-view />
    </div>
  </div>
</template>


<script>
import BarraLateral from '../components/AsideMenuList.vue';

export default {
  components: {
    BarraLateral,
  },
  data() {
    return {
      menu: [
        {
          label: 'Inicio',
          icon: 'mdi-home',
          to: '/',
        },
        {
          label: 'Productos',
          icon: 'mdi-package',
          to: '/productos',
        },
        // Agrega más elementos de menú aquí
      ],
    };
  },
  computed: {
    mostrarComponentes() {
      return this.$route.name !== 'Login';
    }
  }
};
</script>
