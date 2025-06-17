import { SplashScreen, Stack } from "expo-router";
import "./global.css";
import { useFonts } from "expo-font";
import { useEffect } from "react";
import { PaperProvider } from 'react-native-paper';

export default function RootLayout() {
   const [fontsLoaded] = useFonts({
    "outfit": require("./../assets/fonts/Outfit-Regular.ttf"),
    "outfit-bold": require("./../assets/fonts/Outfit-Bold.ttf"),
    "outfit-Medium": require("./../assets/fonts/Outfit-Medium.ttf"),
    "poppins": require("./../assets/fonts/Poppins-Bold.ttf"),
    "rubik-bold": require("./../assets/fonts/Rubik-Bold.ttf"),
    "rubik-light": require("./../assets/fonts/Rubik-Light.ttf"),
    "rubik-medium": require("./../assets/fonts/Rubik-Medium.ttf"),
    "rubik": require("./../assets/fonts/Rubik-Regular.ttf"),
  });

  useEffect(()=>{
    if (!fontsLoaded) {
    SplashScreen.hideAsync(); // Or a loading screen
  }
},[fontsLoaded]);

if(!fontsLoaded) return null;

  return <>
  <PaperProvider>
  <Stack screenOptions={{headerShown:false}} />;
  </PaperProvider>
  </>
}
