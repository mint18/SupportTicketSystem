/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        'dark-bg': '#1a1a1a',
        'dark-text': '#e0e0e0',
        'accent-gold': '#d4af37',
        'dark-secondary': '#2c2c2c'
      },
      backgroundColor: {
        'dark-secondary': '#2c2c2c'
      }
    }
  },
  plugins: [],
}