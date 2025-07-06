import { View, Text, ScrollView, Image, TouchableOpacity } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import images from "@/constants/images";
import { Link, useRouter } from "expo-router";
import SkeletonPlaceholder from "react-native-skeleton-placeholder";
import { ActivityIndicator, MD2Colors } from "react-native-paper";
import { StyleSheet } from "react-native";
import { useShowNotificationData } from "../../services/api/notification";
import MaterialIcons from '@expo/vector-icons/MaterialIcons';
import AntDesign from "@expo/vector-icons/AntDesign";

const Notification = () => {
  const router = useRouter();
  const { data: notiData, isError, isLoading } = useShowNotificationData();

 

  const formatDateTime = (dateString: string) => {
  const date = new Date(dateString);
  return date.toLocaleString(undefined, {
    dateStyle: "medium",
    timeStyle: "short",
    hour12: true,
  });
};

  return (
    <SafeAreaView className="h-full bg-white">
         <View className="flex flex-row items-center gap-6 rounded-lg  mt-5 p-2 bg-[#E6E6FA] w-full h-12 ">
             <TouchableOpacity
          onPress={router.back}
          className="px-2"
        >
          <View>
          <Text>
            <AntDesign name="back" size={24} color="black" />
          </Text>
          </View>
        </TouchableOpacity>
                  <Text className="text-xl font-rubik-bold " style={{fontFamily:'rubik-bold'}}>Notification</Text>
                </View>
      <ScrollView showsVerticalScrollIndicator={false}>
        <View className="flex-1  mt-2  px-2 py-1  ">
          {isLoading ? (
            <ActivityIndicator
              animating={true}
              color={MD2Colors.red800}
              style={{ marginTop: 8 }}
            />
          ) : (
            <View>
              {notiData?.data?.map((posts: any) => (
                <View className="flex flex-row gap-4 items-center mx-3 mb-4 px-3 h-auto py-2 bg-blue-400/20 shadow-md shadow-zinc-400 rounded-lg">
                  <View className="flex flex-col mt-2">
                    <View className="flex flex-row gap-1">
                        <MaterialIcons name="celebration" size={20} color="purple" />
                      <Text
                        className="text-base font-outfit-bold text-primary-100 "
                        style={{ fontFamily: "outfit-Medium" }}               
                      >
                        {posts.title || "No Notification"}
                      </Text>
                    </View>
                    <View className="flex flex-row gap-1 mt-1">
                      <Text
                        className="text-xs font-rubik italic"
                        style={{ fontFamily: "rubik" }}
                      >
                        {posts.message}
                      </Text>
                    </View>
                    <View className="flex flex-row gap-1">
                      <Text
                        className="text-xs text-black-300"
                        style={{ fontFamily: "rubik-light" }}
                      >
                        {formatDateTime(posts.receivedDate)}
                      </Text>
                    </View>
                  </View>
                </View>
              ))}
            </View>
          )}
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

export default Notification;
