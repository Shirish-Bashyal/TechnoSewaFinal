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
import { useShowBidData, useViewBidIdById } from "@/services/api/bid";
import { useLocalSearchParams } from "expo-router";
import { Link, useRouter } from "expo-router";
import { useCreateBooking } from "@/services/api/booking";
import { ActivityIndicator, MD2Colors } from "react-native-paper";
import AntDesign from "@expo/vector-icons/AntDesign";

const Showbid = () => {
     const router = useRouter();
  const { id } = useLocalSearchParams();
  const { data: bidData } = useViewBidIdById(id as string);
  const {mutate,isPending}=useCreateBooking();
  const submitBidData = (bidId: number) => {
  if (!bidId) return;
  mutate(bidId.toString()); 
};
  return (
    <SafeAreaView className="bg-gray-100 h-full">
         <View className="flex flex-row  justify-center items-center mt-8">
        <TouchableOpacity
          onPress={router.back}
          className="flex flex-row  gap-4 mx-4 py-10"
        >
          <View>
          <Text>
            <AntDesign name="back" size={24} color="black" />
          </Text>
          </View>
        </TouchableOpacity>
        <Text
          className="text-lg text-black-300"
          style={{ fontFamily: "rubik-bold" }}
        >
          Available Technician and Their Bidding!!
        </Text>
        {/* <Toaster position="bottom-center" reverseOrder={false} /> */}
      </View>
      <ScrollView
        showsVerticalScrollIndicator={false}
        // contentContainerClassName="pb-32 px-7"
      >
        <View className="flex-1 w-[98%] h-auto mt-2 px-3 py-1 !mr-10 ml-2  ">
          {bidData?.data?.map((bids: any) => (
            <View
              className=" gap-4  h-auto mb-4 px-2 py-2 bg-white shadow-md shadow-zinc-400 rounded-lg"
              //   key={posts.id}
            >
              <View className="flex flex-col">
                <View className="flex flex-row justify-between gap-2">
                  <View>
                    <Text
                      className="text-base font-outfit-bold text-black mt-1"
                      style={{ fontFamily: "outfit-medium" }}
                    >
                      Technician Name: {bids.technicianName}
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
              {/* <Link href={`/Forconsumer/showbid/${bids.bidId}`} asChild> */}
              {isPending ? (
          <ActivityIndicator
            animating={true}
            color={MD2Colors.red800}
            style={{ marginTop: 8 }}
          />
        ) : (
                <TouchableOpacity
                  onPress={() => submitBidData(bids.bidId)}
                  className="bg-[#7A4DFF]/[1.6] shadow-md w-full  shadow-zinc-300 rounded-lg  flex justify-center items-center h-12 py-4 mt-1 mr-4 "
                >
                    <View>
                  <Text
                    className="text-xs  text-white text-center"
                    style={{ fontFamily: "rubik-bold" }}
                  >
                    Book Now
                  </Text>
                  </View>
                </TouchableOpacity>
        )}
              {/* </Link> */}
            </View>
          ))}
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

export default Showbid;
