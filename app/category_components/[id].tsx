import { View, Text, FlatList, TouchableOpacity } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { Card, FeaturedCard } from "@/components/Cards";
import { Services } from '../../components/services-category';
import { useRouter } from "expo-router";
import AntDesign from "@expo/vector-icons/AntDesign";


const servicesComponents = () => {
   const router = useRouter();
  return (
    <SafeAreaView className="bg-gray-100 h-full">
      <View className="absolute bg-primary-100/70 h-28 flex justify-center top-0 left-0 right-0 z-10">
      <TouchableOpacity
          onPress={router.back}
          className="flex flex-row  gap-4 mx-4 py-10"
        >
          <AntDesign name="back" size={34} color="white" />
          <Text className="text-white   text-[20px]" style={{fontFamily:'rubik-bold' ,letterSpacing: 1.5 }}>Electrician</Text>
        </TouchableOpacity>
        </View>
        <View className="mx-2 mt-20"><Text
                className="text-xl text-black-300"
                style={{ fontFamily: "rubik-bold" }}
              >
                New Services
              </Text></View>
      <FlatList
        data={[1, 2, 3,4,5,6]}
        renderItem={({ item }) => <Services/>}
        keyExtractor={(item) => item.toString()}      
        bounces={false}
        showsHorizontalScrollIndicator={false}
        contentContainerClassName="flex gap-2 mt-4"
      />
    </SafeAreaView>
  );
};

export default servicesComponents;
