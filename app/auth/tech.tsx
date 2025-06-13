import { View, Text, TouchableOpacity } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { useRouter } from "expo-router";

const Tech = () => {
  const router = useRouter();
   const handleTechnician = () => {
    router.push("/Technician/(root)/(tabs)");
  };
  const handleClient = () => {
    router.push("/(root)/(tabs)");
  };
  return (
    <SafeAreaView className="h-full bg-gray-100">
        <View className="flex justify-center items-center flex-1  ">
            <View className="shadow-black-100 shadow-sm  bg-gray-50 px-4 py-4 rounded-2xl">
      <View className="mt-5 flex justify-center items-center">
        <Text className="text-xl" style={{ fontFamily: "rubik-bold" }}>
          Join as Client or Technician
        </Text>
      </View>
      <View className="mb-1 flex flex-row gap-5">
        <TouchableOpacity onPress={handleClient} className="bg-[#7A4DFF]/[1.6] shadow-md shadow-zinc-300 rounded-lg w-32 h-14 flex justify-center items-center  mt-8 mx-2 ">
          <View className="flex flex-row items-center justify-center">
            <Text className="text-lg font-rubik-bold text-white text-center mt-2 ">
              Client
            </Text>
          </View>
        </TouchableOpacity>
        <TouchableOpacity onPress={handleTechnician} className="bg-[#7A4DFF]/[1.6] shadow-md shadow-zinc-300 rounded-lg w-32 h-14 flex justify-center items-center mt-8 mx-2 ">
          <View className="flex flex-row items-center justify-center">
            <Text className="text-lg font-rubik-bold text-white text-center mt-2 ">
              Technician
            </Text>
          </View>
        </TouchableOpacity>
      </View>
      </View>
      </View>
    </SafeAreaView>
  );
};

export default Tech;
