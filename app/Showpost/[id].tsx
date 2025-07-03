import { View, Text,TouchableOpacity, } from 'react-native'
import React from 'react'
import { useLocalSearchParams } from 'expo-router';
import { useViewpostIdById } from '@/services/api/postfortechnician';
import { Link, useRouter } from "expo-router";
import { useViewPostForTechnician } from "@/services/api/postfortechnician";


const showPost = () => {
    const { id } = useLocalSearchParams();
  const { data: postData } =useViewpostIdById(id as string)
  const { data: postProblemData, isError, isLoading } = useViewPostForTechnician();

  return (
    <View>
      <Text>{postData?.data?.title}</Text>
       {/* {postData?.data?.map((posts: any) => ( */}
      <Link href={`/Bid/${id}`} asChild>
                <TouchableOpacity
                  // onPress={handleShowAboutTechnician}
                  className="bg-[#7A4DFF]/[1.6] shadow-md w-[40%] shadow-zinc-300 rounded-lg  flex justify-center items-center h-12 py-4 mt-1 "
                >
                  <View className="">
                    <Text
                      className="text-xs  text-white text-center"
                      style={{ fontFamily: "rubik-bold" }}
                    >
                      Show Details
                    </Text>
                  </View>
                </TouchableOpacity>
                </Link>
                 {/* ))} */}
    </View>
  )
}

export default showPost