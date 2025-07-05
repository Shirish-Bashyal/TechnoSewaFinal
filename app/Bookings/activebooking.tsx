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
import { useShowAllBooking } from "@/services/api/booking";
import LeafletViewMap from "../Expressproblem/ViewMap";
import MaterialIcons from '@expo/vector-icons/MaterialIcons';
import { ActivityIndicator, MD2Colors } from "react-native-paper";



const activeBooking = () => {
    const { data: bookingData, isError, isLoading } = useShowAllBooking();
  return (
    <SafeAreaView className="bg-gray-100 h-full">
      <ScrollView
        showsVerticalScrollIndicator={false}
        // contentContainerClassName="pb-32 px-7"
      >
        <View className="flex-1 w-[98%] h-auto mt-2 px-3 py-1 !mr-10 ml-2  ">
          {bookingData?.data?.activeBookings.map((booking: any,index:any) => (
            <View
              className=" gap-4  h-auto mb-4 px-2 py-2 bg-white shadow-md shadow-zinc-400 rounded-lg"
               key={booking?.postBids?.bidId || index}
            >
              <View className="flex-col items-start justify-center mt-5 px-5">
            <Text className="text-black font-bold text-xl">
              {booking?.title}
            </Text>
            {/* <View className=" mt-2">
              <Text className="text-light-200 text-gray-500 text-sm">
                {booking?.description}
              </Text>
              
            </View> */}
            <View className=" mt-2">
              <Text className="text-light-200 text-sm italic">
              Post By: {booking?.consumerName}
              </Text>
            </View>
            <View className=" mt-1">
              <Text className="text-light-200 text-sm italic">
              Consumer Phone Number: {booking?.consumerPhoneNumber}
              </Text>
            </View>
            <View className="flex-row items-center bg-gray-100 px-1 py-1 rounded-md gap-x-1 mt-2">
            <MaterialIcons name="monetization-on" size={18} color="blue" />
             <Text className="text-light-200 text-sm text-blue-800">
                Estimated Price:
              </Text>
              <Text className="text-black-200 font-bold text-sm">
                {booking?.price}
              </Text>
              
            </View>
            <View className="flex-row items-center bg-gray-100 px-1 rounded-md gap-x-1 mt-2">
             <MaterialCommunityIcons name="calendar-clock-outline" size={18} color="green" />
             <Text className="text-light-200 text-sm text-green-700">
                Service Date:
              </Text>
              <Text className="text-black-200 font-bold text-sm">
              {booking?.serviceDate?.split('T')[0]}
              </Text>
              
            </View>
           
            </View>

            {booking?.lattitude !== undefined &&
              booking?.longitude !== undefined && (
                <LeafletViewMap
                  latitude={booking?.lattitude}
                  longitude={booking?.longitude}
                  onSelectLocation={(lat, lng) => {
                    console.log("Selected new location:", lat, lng);
                  }}
                />
              )}
          </View>
           
          ))}
        </View>
      </ScrollView>
    </SafeAreaView>
  )
}

export default activeBooking