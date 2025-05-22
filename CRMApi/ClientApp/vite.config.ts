import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import {VitePWA} from "vite-plugin-pwa"
import tailwindcss from '@tailwindcss/vite'


// https://vite.dev/config/
export default defineConfig({
  plugins: [
    react(),
    tailwindcss(),
    VitePWA({
      registerType: 'autoUpdate',
      manifest: {
        name: 'Crunch Time',
        short_name: 'Crunch Time',
        start_url: '.',
        display: 'standalone',
        background_color: '#ffffff',
        // theme_color: '#1976d2',
        icons: [
          {
            src: 'icons/crunch-time.png',
            sizes: '192x192',
            type: 'image/png'
          },
          {
            src: 'icons/crunch-time.png',
            sizes: '512x512',
            type: 'image/png'
          }
        ]
      }
    })
  ],
  build: {
    outDir: '../wwwroot',
    emptyOutDir: true
  }
})
