import {
  View,
  Text,
  TouchableOpacity,
  Image,
  Touchable,
  StyleSheet,
} from "react-native";
import React from "react";
import images from "@/constants/images";
import Entypo from "@expo/vector-icons/Entypo";
import FontAwesome from "@expo/vector-icons/FontAwesome";
import { Link, useRouter } from "expo-router";
import { useViewPostForTechnician } from "@/services/api/postfortechnician";
import SkeletonPlaceholder from "react-native-skeleton-placeholder";
import { ActivityIndicator, MD2Colors } from "react-native-paper";

import { baseURL } from "../services/axiosInstance";

interface Props {
  onPress?: () => void;
}
export const PostForTechnician = ({ onPress }: Props) => {
  const router = useRouter();
  const { data: postData, isError, isLoading } = useViewPostForTechnician();
  console.log(baseURL);
  //   const handleServiceDetails = () => {
  //     router.push("/auth/userdetails");
  //   };

  // const handleShowAboutTechnician = () => {
  //   router.push(`/`);
  // };

  // const rawUrl = postData?.data?.imageUrl;
  //   const imageUri =
  //   Array.isArray(rawUrl) && rawUrl[0]?.trim()
  //     ? rawUrl[0].replace("https://localhost:7206", "https://5cc9-2400-1a00-bb20-1efe-2022-e39d-832f-c606.ngrok-free.app")
  //     : null;

  //     console.log(imageUri)

  return (
    <View className="flex-1 w-[98%] mt-2 px-3 py-1 !mr-10 ml-2  ">
      {isLoading ? (
        <ActivityIndicator
          animating={true}
          color={MD2Colors.red800}
          style={{ marginTop: 8 }}
        />
      ) : postData?.message === "Technician not registered" ? (
        <View className="items-center justify-center mt-8">
          <Text
            className="text-base text-red-600 font-bold"
            style={{ fontFamily: "rubik-bold" }}
          >
            "Wait for admin to verify!!
          </Text>
        </View>
      ) : (
        <View>
          {postData?.data?.toReversed().map((posts: any) => (
            <View
              className="flex flex-col py-2 mb-4 bg-white shadow-md shadow-zinc-400 rounded-lg"
              key={posts.id}
            >
              <View className="flex flex-row mx-2 gap-4 items-center">
                <Image
                  source={
                    posts.imageUrl && posts.imageUrl.length > 0
                      ? {
                          uri: posts.imageUrl[0].replace(
                            "https://localhost:7206",
                            baseURL
                          ),
                        }
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
              </View>
              <Link href={`/Showpost/${posts.id}`} asChild>
                <TouchableOpacity
                  // onPress={handleShowAboutTechnician}
                  className="bg-[#7A4DFF]/[1.6] shadow-md  shadow-zinc-300 rounded-lg w-[95%] mx-2  flex justify-center items-center h-12 py-4 mt-1 "
                >
                  <View>
                    <Text
                      className="text-xs  text-white text-center"
                      style={{ fontFamily: "rubik-bold" }}
                    >
                      Show Details
                    </Text>
                  </View>
                </TouchableOpacity>
              </Link>
            </View>
          ))}
        </View>
      )}
    </View>
  );
};
