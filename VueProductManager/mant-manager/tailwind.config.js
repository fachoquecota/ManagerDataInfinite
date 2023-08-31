/** @type {import('tailwindcss').Config} */
module.exports = {
  purge: {
    // Habilita la eliminación de clases no utilizadas en producción
    enabled: process.env.NODE_ENV === 'production',
    content: [
      './src/**/*.vue',
      './src/**/*.js',
      './src/**/*.jsx',
      './src/**/*.html',
      './src/**/*.md',
    ],
  },
  darkMode: false, // O 'media' o 'class'
  theme: {
    extend: {
      // Aquí puedes añadir tus propias personalizaciones
    },
  },
  variants: {
    extend: {
      // Aquí puedes añadir variantes personalizadas
    },
  },
  plugins: [
    // Aquí puedes añadir plugins
  ],
}
