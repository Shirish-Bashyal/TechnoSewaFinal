import { View, Text, ScrollView, Image, TouchableOpacity } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import images from "@/constants/images";
import { Link, useRouter } from "expo-router";
import SkeletonPlaceholder from "react-native-skeleton-placeholder";
import { ActivityIndicator, MD2Colors } from "react-native-paper";
import { StyleSheet } from 'react-native';
import { usePostProblemForConsumer } from "@/services/api/consumerpostedproblem";

const Message = () => {
  const router = useRouter();
   const { data: postData, isError, isLoading } = usePostProblemForConsumer();
  return (
    <SafeAreaView className="h-full bg-white">
          <ScrollView
            showsVerticalScrollIndicator={false}
           
          >
            <View className="flex-1 w-full mt-2 px-1 py-1 !mr-10 ml-2  ">
      {isLoading ? (
        <ActivityIndicator
          animating={true}
          color={MD2Colors.red800}
          style={{ marginTop: 8 }}
        />
      ) : (
        <View>
          {postData?.data?.map((posts: any) => (
            <View
              className="flex flex-row gap-4 items-center mb-4 bg-white shadow-md shadow-zinc-400 rounded-lg"
              key={posts.id}
            >
              <Image
                source={
                  posts.imageUrl && posts.imageUrl.length > 0
                    ? { uri: posts.imageUrl[0] }
                    : images.avatar
                }
                className="!w-20 !h-20 rounded-lg !object-fill"
              />
              <View className="flex flex-col mt-2">
                <View>
                  <Text
                    className="text-base font-outfit-bold text-black-300 "
                    style={{ fontFamily: "outfit-Medium" }}
                    numberOfLines={2}
                  >
                    {posts.title || "problem"}
                  </Text>
                </View>
                <View className="flex flex-row gap-1">
                  <Text
                    className="text-xs text-black-300"
                    style={{ fontFamily: "rubik-light" }}
                  >
                    Category:
                  </Text>
                  <Text
                    className="text-xs font-rubik text-primary-100"
                    style={{ fontFamily: "rubik-bold" }}
                  >
                    {posts.category}
                  </Text>
                </View>
                <View className="flex flex-row gap-1">
                  <Text
                    className="text-xs text-black-300"
                    style={{ fontFamily: "rubik-light" }}
                  >
                    Post By:
                  </Text>
                  <Text
                    className="text-xs text-black-300"
                    style={{ fontFamily: "rubik-light" }}
                  >
                    {posts.userName}
                  </Text>
                </View>
              </View>
              <Link href={`/Showpost/${posts.id}`} asChild>
                <TouchableOpacity
                  // onPress={handleShowAboutTechnician}
                  className="bg-[#7A4DFF]/[1.6] shadow-md w-[40%] shadow-zinc-300 rounded-lg  flex justify-center items-center h-12 py-4 mt-1 mr-4 "
                >
                  <Text
                    className="text-xs  text-white text-center"
                    style={{ fontFamily: "rubik-bold" }}
                  >
                    Show Details
                  </Text>
                </TouchableOpacity>
              </Link>
            </View>
          ))}
        </View>
      )}
    </View>
             </ScrollView>
                </SafeAreaView>

  )
}

export default Message

const styles = StyleSheet.create({
  title: {
    textAlign: 'center',
  },
})