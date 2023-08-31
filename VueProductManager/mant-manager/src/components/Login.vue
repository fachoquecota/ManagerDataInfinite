<template>
  <div class="flex flex-col items-center justify-center min-h-screen bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
    <div class="flex flex-col md:flex-row-reverse w-full md:max-w-4xl">
      <!-- Cuadro del formulario -->
      <div class="bg-white p-8 rounded-lg shadow-md w-full md:w-1/2 h-96">
        <h2 class="text-center text-3xl font-extrabold text-gray-900">
          Iniciar sesión
        </h2>
        <form class="mt-8 space-y-6">
          <div class="rounded-md shadow-sm -space-y-px">
            <div>
              <label for="email-address" class="sr-only">Correo electrónico</label>
              <input id="email-address" name="email" type="email" autocomplete="email" required class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm" placeholder="Correo electrónico">
            </div>
            <div>
              <label for="password" class="sr-only">Contraseña</label>
              <input id="password" name="password" type="password" autocomplete="current-password" required class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm" placeholder="Contraseña">
            </div>
          </div>
          <div>
            <button @click="submit" type="button" class="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
              Iniciar sesión
            </button>
          </div>
        </form>
      </div>
      <!-- Imagen al lado del formulario -->
      <div class="hidden md:block md:w-1/2 h-96">
        <img class="rounded-lg shadow-md h-full w-full object-cover" src="@/assets/1603891972-shutterstock466589549-880x450.jpg" alt="Ropa">
      </div>
    </div>
  </div>
</template>

<script>
import { useRouter } from 'vue-router'

export default {
  name: 'UserLogin',
  setup() {
    const router = useRouter()

    const submit = () => {
      router.push({ name: 'inicio' })
    }

    return {
      submit
    }
  }
}
</script>


<style scoped>
@media (max-width: 768px) {
  .hidden {
    display: none;
  }
}
</style>
