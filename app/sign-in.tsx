import { View, Text, ScrollView, Image, TouchableOpacity, StyleSheet } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";

import images from "@/constants/images";
import { useRouter } from 'expo-router';


const signIn = () => {
    const router = useRouter();

  const handleLogin = () => {
    router.push('/auth/phone'); // Routes to /auth/phone
  };
  return (
    <SafeAreaView className="bg-white h-full">
      <ScrollView contentContainerClassName="h-full">
        <View className=" w-full flex justify-center items-center" >
        <Image source={images.logo}  style={{
                paddingTop:3,
                height: 380,
                 borderRadius: 20,
      
      
      
              }} className="!w-[340px] mt-8 shadow-md shadow-zinc-300 rounded-full" />
              </View>
        <View className="px-10 mt-7">
          <Text className="text-sm text-center uppercase font-outfit-light text-black-200  pt-5">
            Welcome to Techno Sewa
          </Text>
          <Text className="text-3xl font-rubik-bold text-black-300 text-center mt-2">
            Let's Get Closer to {"\n"}
            <Text className="text-primary-100">Choose Ideal Technician</Text>
          </Text>
        </View>
        <TouchableOpacity
          onPress={handleLogin}
          className="bg-[#7A4DFF]/[1.6] shadow-md shadow-zinc-300 rounded-full w-[97%] h-20 py-4 mt-8 mx-2 "
        >
          <View className="flex flex-row items-center justify-center">
           <Text className="text-lg font-rubik-bold text-white text-center mt-2 ">Continue With Phone Number</Text>
           </View>
         
        </TouchableOpacity>
        
        <TouchableOpacity
          onPress={handleLogin}
          className="bg-white shadow-md shadow-zinc-400 rounded-full w-[97%] py-4 mt-5 !mr-20 ml-2"
        >
          <View className="flex flex-row items-center justify-center">
            <Image
              className="w-5"
              source={require("../assets/images/google.png")}
              style={{
                width: 40,
                height: 40,
              }}
            />
            <Text className="text-lg font-rubik-Medium text-black ml-2">
              Continue with Google
            </Text>
          </View>
        </TouchableOpacity>
      </ScrollView>
    </SafeAreaView>
  );
};

export default signIn;


