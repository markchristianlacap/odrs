import antfu from '@antfu/eslint-config'
import withNuxt from './.nuxt/eslint.config.mjs'

export default withNuxt(
  antfu({
    formatters: {
      html: 'prettier',
      css: 'prettier',
      markdown: 'prettier',
      xml: 'prettier',
    },
    unocss: true,
  }),
)
