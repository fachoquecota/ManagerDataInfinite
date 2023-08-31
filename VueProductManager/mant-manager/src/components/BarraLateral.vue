<template>
    <aside class="fixed top-14 left-0 z-30 w-64 h-screen pt-20 transition-transform -translate-x-full bg-blue-900 text-white border-r border-gray-200 sm:translate-x-0 dark:bg-gray-800 dark:border-gray-700" aria-label="Sidebar">
      <div class="h-full px-3 pb-4 overflow-y-auto">
        <ul class="space-y-2 font-medium">
          <li>
            <router-link to="/productos" class="flex items-center p-2 rounded-lg hover:bg-blue-700">
              <i class="fas fa-box mr-3"></i>
              <span>Productos</span>
            </router-link>
          </li>
          <li>
            <router-link to="/usuarios" class="flex items-center p-2 rounded-lg hover:bg-blue-700">
              <i class="fas fa-users mr-3"></i>
              <span>Usuarios</span>
            </router-link>
          </li>
          <li>
            <router-link to="/configuracion" class="flex items-center p-2 rounded-lg hover:bg-blue-700">
              <i class="fas fa-cog mr-3"></i>
              <span>Configuración</span>
            </router-link>
          </li>
        </ul>
      </div>
    </aside>
  </template>
  
  <script>
  export default {
    // Tu lógica aquí
  };
  </script>
  