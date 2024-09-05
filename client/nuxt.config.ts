export default defineNuxtConfig({
  compatibilityDate: '2024-04-03',
  devtools: { enabled: true },
  css: [
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
})

