import { View, Text, ScrollView, Image, TouchableOpacity } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import React from "react";
import { useLocalSearchParams } from "expo-router";
import { useViewpostIdById } from "@/services/api/postfortechnician";
import { Link, useRouter } from "expo-router";
import { useViewPostForTechnician } from "@/services/api/postfortechnician";
import Entypo from "@expo/vector-icons/Entypo";
import AntDesign from "@expo/vector-icons/AntDesign";
import images from "@/constants/images";
import LeafletViewMap from "../Expressproblem/ViewMap";
import MaterialIcons from '@expo/vector-icons/MaterialIcons';
import MaterialCommunityIcons from '@expo/vector-icons/MaterialCommunityIcons';

// const MovieInfo = ({ label, value }: MovieInfoProps) => (
//   <View className="flex-col items-start justify-center">
//     <Text className="text-light-200 font-normal text-sm">{label}</Text>
//     <Text className="text-light-100 font-bold text-sm mt-2">
//       {value || "N/A"}
//     </Text>
//   </View>
// );

const showPost = () => {
  const { id } = useLocalSearchParams();
  const { data: postData } = useViewpostIdById(id as string);
  // const {
  //   data: postProblemData,
  //   isError,
  //   isLoading,
  // } = useViewPostForTechnician();

  const rawUrl = postData?.data?.imageUrl;
  console.log("rawurl", rawUrl);
const imageUri =
  Array.isArray(rawUrl) && rawUrl[0]?.trim()
    ? rawUrl[0].replace("https://localhost:7206", "https://4932-2400-1a00-bb20-1efe-575-f20-9c4f-1887.ngrok-free.app")
    : null;

    console.log(imageUri)

  return (
    <SafeAreaView className="h-full bg-white">
      {/* <Text>{postData?.data?.title}</Text> */}
      {/* {postData?.data?.map((posts: any) => ( */}
      
        <ScrollView
          contentContainerStyle={{
            paddingBottom: 80,
          }}
        >
          <View className="flex-1">
          <View>
            <Image
              source={imageUri ? { uri: imageUri } : images.avatar}
              className="w-full h-[350px]"
                resizeMode="contain"
            />
          </View>
          <View className="flex-col items-start justify-center mt-5 px-5">
            <Text className="text-black font-bold text-xl">
              {postData?.data?.title}
            </Text>
            <View className=" mt-2">
              <Text className="text-light-200 text-gray-500 text-sm">
                {postData?.data?.description}
              </Text>
              
            </View>
            <View className="mt-2">
              <Text className="text-light-200 text-sm italic">
              Post By: {postData?.data?.userName}
              </Text>
            </View>
            <View className="flex-row items-center bg-gray-100 px-1 py-1 rounded-md gap-x-1 mt-2">
             <MaterialIcons name="category" size={18} color="blue" />
             <Text className="text-light-200 text-sm text-blue-800">
                Category:
              </Text>
              <Text className="text-black-200 font-bold text-sm">
                {postData?.data?.category}
              </Text>
              
            </View>
            <View className="flex-row items-center bg-gray-100 px-1 rounded-md gap-x-1 mt-2">
             <MaterialCommunityIcons name="calendar-clock-outline" size={18} color="green" />
             <Text className="text-light-200 text-sm text-green-700">
                Service Date:
              </Text>
              <Text className="text-black-200 font-bold text-sm">
              {postData?.data?.creationDate?.split('T')[0]}
              </Text>
              
            </View>
           
            </View>

            {postData?.data?.lattitude !== undefined &&
              postData?.data?.longitude !== undefined && (
                <LeafletViewMap
                  latitude={postData.data.lattitude}
                  longitude={postData.data.longitude}
                  onSelectLocation={(lat, lng) => {
                    console.log("Selected new location:", lat, lng);
                  }}
                />
              )}
          </View>
          <Link href={`/Bid/${id}`} asChild className="flex justify-center items-center ml-14">
            <TouchableOpacity
              // onPress={handleShowAboutTechnician}
              className="bg-[#7A4DFF]/[1.6] shadow-md w-[40%] shadow-zinc-300 rounded-lg  flex justify-center items-center h-12 py-4 mt-1 "
            >
              <View className="flex justify-center items-center">               
                <Text
                  className="text-xs  text-white text-center"
                  style={{ fontFamily: "rubik-bold" }}
                >
                  Start Bidding
                </Text>
              </View>
            </TouchableOpacity>
          </Link>
        </ScrollView>
      

      {/* ))} */}
    </SafeAreaView>
  );
};

export default showPost;
