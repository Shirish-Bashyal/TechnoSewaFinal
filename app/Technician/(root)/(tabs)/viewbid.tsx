import {
  View,
  Text,
  ScrollView,
  TouchableOpacity,
  Image,
  StyleSheet,
} from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import MaterialCommunityIcons from "@expo/vector-icons/MaterialCommunityIcons";
import { useShowBidData } from "@/services/api/bid";

const viewbidpost = () => {
  const { data: bidData, isError, isLoading } = useShowBidData();
  return (
    <SafeAreaView className="bg-gray-100 h-full">
      <ScrollView
        showsVerticalScrollIndicator={false}
        // contentContainerClassName="pb-32 px-7"
      >
        <View className="flex-1 w-[98%] h-auto mt-2 px-3 py-1 !mr-10 ml-2  ">
          {bidData?.data?.map((bids: any,index:number) => (
            <View
              className=" gap-4  h-auto mb-4 px-2 py-2 bg-white shadow-md shadow-zinc-400 rounded-lg"
              key={bids.bidId || index}
            >
              <View className="flex flex-col">
                <View className="flex flex-row justify-between gap-2">
                  <View>
                    <Text
                      className="text-base font-outfit-bold text-black mt-1"
                      style={{ fontFamily: "outfit-medium" }}
                    >
                      {bids.postTitle}
                    </Text>
                  </View>
                </View>

                <View className="flex flex-row gap-1">
                  <Text
                    className="text-xs text-black-300"
                    style={{ fontFamily: "rubik-light" }}
                  >
                    {bids.solutionDescription}
                  </Text>
                  {/* <Text
                className="text-xs font-rubik text-primary-100"
                style={{ fontFamily: "rubik-bold" }}
              >
                {bids.solutionDescription}
              </Text> */}
                </View>
                <View className="flex flex-row gap-1">
                  <Text
                    className="text-xs text-black-300"
                    style={{ fontFamily: "rubik-light" }}
                  >
                    Estimated Price:
                  </Text>
                  <Text
                    className="text-xs font-rubik text-primary-100"
                    style={{ fontFamily: "rubik-bold" }}
                  >
                    {bids.estimationPrice}
                  </Text>
                </View>
                <View className="flex flex-row px-2 py-2 mt-2 gap-1 bg-gray-200 rounded-full ">
                  <MaterialCommunityIcons
                    name="calendar-clock"
                    size={18}
                    color="black"
                  />
                  <Text
                    className="text-xs text-black-300"
                    style={{ fontFamily: "rubik-light" }}
                  >
                    Service Date:
                  </Text>
                  <Text className="text-xs font-rubik-bold text-primary-300 ml-0.5">
                    {bids.serviceDate}
                  </Text>
                </View>
              </View>
            </View>
          ))}
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

export default viewbidpost;
