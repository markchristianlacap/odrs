export default defineNuxtConfig({
  ssr: false,
  app: {
    head: {
      charset: 'utf-8',
      viewport: 'width=device-width, initial-scale=1',
      title:
        'Online Document Request System - Occidental Mindoro State College',
    },
  },
  compatibilityDate: '2024-04-03',
  devtools: { enabled: true },
  css: ['@unocss/reset/tailwind-compat.css', '~/assets/css/main.css'],
  modules: ['@unocss/nuxt', '@nuxt/eslint', 'nuxt-quasar-ui', '@vueuse/nuxt'],
  eslint: {
    config: {
      standalone: false,
    },
  },
  quasar: {
    plugins: ['Notify', 'Dialog'],
  },
})
