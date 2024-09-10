export default defineNuxtConfig({
  app: {
    head: {
      charset: 'utf-8',
      viewport: 'width=device-width, initial-scale=1',
      title: 'ODRS - Occidental Mindoro State College',
    },
  },
  compatibilityDate: '2024-04-03',
  devtools: { enabled: true },
  css: [
    '@unocss/reset/tailwind-compat.css',
    '~/assets/css/main.css',
  ],
  modules: [
    '@unocss/nuxt',
    '@nuxt/eslint',
    'nuxt-quasar-ui',
  ],
  eslint: {
    config: {
      standalone: false,
    },
  },
  quasar: {
  },
})
