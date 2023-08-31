<template>
  <nav class="fixed top-0 z-50 w-full h-12 bg-blue-900 dark:bg-gray-800 dark:border-gray-700">
    <div class="px-3 py-1 lg:px-5 lg:pl-3 flex items-center justify-between">
      <div class="flex items-center">
        <button data-drawer-target="logo-sidebar" data-drawer-toggle="logo-sidebar" aria-controls="logo-sidebar" type="button" class="inline-flex items-center p-2 text-sm text-white rounded-lg sm:hidden hover:bg-blue-800 focus:outline-none focus:ring-2 focus:ring-white">
          <span class="sr-only">Open sidebar</span>
          <svg class="w-6 h-6" aria-hidden="true" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
            <!-- ... el contenido del botón del menú -->
          </svg>
        </button>
        <a href="#" class="flex items-center ml-2 md:mr-24">
          <img src="URL_DE_TU_LOGO" class="h-8 mr-3" alt="Logo" />
          <span class="self-center text-xl font-semibold sm:text-2xl whitespace-nowrap text-white">Mi aplicación</span>
        </a>
      </div>
      <div class="relative ml-3">
        <button @click="isMenuOpen = !isMenuOpen" aria-haspopup="true" class="flex text-sm transition duration-150 ease-in-out border-2 border-transparent rounded-full focus:outline-none focus:border-white focus:ring-2 focus:ring-white">
          <img class="object-cover w-8 h-8 rounded-full" src="URL_DE_TU_AVATAR" alt="Tu usuario" />
        </button>
        <div v-if="isMenuOpen" @click="isMenuOpen = false" class="fixed inset-0 h-full w-full z-10"></div>
        <div v-if="isMenuOpen" class="absolute right-0 mt-3 py-2 w-48 bg-white border dark:bg-gray-700 border-gray-200 rounded-lg shadow-lg">
          <a href="#" class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 hover:text-gray-900 dark:text-gray-300 dark:hover:bg-gray-600">Configuración</a>
          <a href="#" class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 hover:text-gray-900 dark:text-gray-300 dark:hover:bg-gray-600">Cerrar sesión</a>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  data() {
    return {
      isMenuOpen: false
    };
  }
  // Otros métodos y propiedades que puedas necesitar
};
</script>

<style scoped>
/* Tus estilos personalizados aquí */
</style>
