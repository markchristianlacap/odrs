import { defineConfig, presetIcons, presetUno, presetWebFonts } from 'unocss'

export default defineConfig({
  theme: {
    colors: {
      primary: 'var(--primary-color)',
      surface: 'var(--surface-color)',
      background: 'var(--background-color)',
      text: 'var(--text-color)',
      accent: 'var(--accent-color)',
      success: 'var(--success-color)',
      warning: 'var(--warning-color)',
      error: 'var(--error-color)',
    },
  },
  presets: [
    presetUno(),
    presetWebFonts({
      fonts: {
        inter: 'Inter',
      },
    }),
    presetIcons(),
  ],
  shortcuts: {
    card: 'p-4 bg-white rounded-lg shadow-xl',
    btn: 'border-none py-2 px-4 rounded',
  },
})
