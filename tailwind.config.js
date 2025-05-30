/** @type {import('tailwindcss').Config} */
module.exports = {
  // NOTE: Update this to include the paths to all of your component files.
  content: ["./app/**/*.{js,jsx,ts,tsx}","./app/**/**/*.{js,jsx,ts,tsx}", "./components/**/*.{js,jsx,ts,tsx}"],
  presets: [require("nativewind/preset")],
  theme: {
    extend: {
      fontFamily: {
        rubik: ["Rubik-Regular", "sans-serif"],
        "rubik-bold": ["Rubik-Bold", "sans-serif"],
        "rubik-Medium": ["Rubik-Medium", "sans-serif"],
        "rubik-light": ["Rubik-Light", "sans-serif"],
        "poppins-bold": ["Poppins-Bold", "sans-serif"],
        "outfit-bold": ["Outfit-Bold", "sans-serif"],
        "outfit-medium": ["Outfit-Medium", "sans-serif"],
        "outfit-light": ["Outfit-Light", "sans-serif"],
      },
      colors:{
        "primary":{
          100:'#7A4DFF',
        },
        accent:{
          100:'#FBFBFD',
        },
        black:{
          DEFAULT:'#000000',
          100:'#8C8E98',
          200:'#666876',
          300:'#191D31'
        },
        danger:'#f75555'
      }
    },
  },
  plugins: [],
};
