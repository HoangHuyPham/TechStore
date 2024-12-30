/** @type {import('tailwindcss').Config} */
export default {
  mode: 'jit',
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      
      animation: {
        fade: 'fadeOut 0.5s ease',
      },

      keyframes: theme => ({
        fadeOut: {
          '0%': { opacity:0 },
          '100%': { opacity:1 },
        },
      }),
    },
  },
  plugins: [],
}

