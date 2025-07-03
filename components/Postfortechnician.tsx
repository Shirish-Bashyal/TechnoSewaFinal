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

interface Props {
  onPress?: () => void;
}
export const PostForTechnician = ({ onPress }: Props) => {
  const router = useRouter();
  const { data: postData, isError, isLoading } = useViewPostForTechnician();
  //   const handleServiceDetails = () => {
  //     router.push("/auth/userdetails");
  //   };

  // const handleShowAboutTechnician = () => {
  //   router.push(`/`);
  // };
  return (
    <View className="flex-1 w-[98%] mt-2 px-3 py-1 !mr-10 ml-2  ">
      {/* <View className="flex flex-row items-center absolute px-2 top-5 right-5 bg-white/90 p1 rounded-full z-50">
        <FontAwesome name="star-half-empty" size={20} color="gold" />
        <Text className="text-xs font-rubik-bold text-primary-300 ml-0.5">
          4.4
        </Text>
      </View> */}
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
            className="!w-20 !h-20 rounded-full !object-fill"
          />
          <View className="flex flex-col mt-2">
            <View className="flex flex-row justify-between gap-2">
              {/* <View>
          <Text
            className="text-xs font-outfit-bold text-black-100 mt-1"
            style={{ fontFamily: "outfit-medium" }}
          >
           By Team Nepal Air Condition
          </Text></View> */}
              {/* <View className="flex flex-row gap-1">
                <Entypo name="star" size={18} color="gold" />
                <Text
                  className="text-xs font-rubik-bold text-black-100 mt-1"
                  style={{ fontFamily: "rubik-bold" }}
                >
                  4.4
                </Text>
                <Text
                  className="text-xs font-rubik-bold text-black-100 mt-1"
                  style={{ fontFamily: "rubik-bold" }}
                >
                  (12)
                </Text>
              </View> */}
            </View>
            <View>
              <Text
                className="text-base font-outfit-bold text-black-300 "
                style={{ fontFamily: "outfit-Medium" }}
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
                By:
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
            className="bg-[#7A4DFF]/[1.6] shadow-md w-[40%] shadow-zinc-300 rounded-lg  flex justify-center items-center h-12 py-4 mt-1 "
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
  );
};
