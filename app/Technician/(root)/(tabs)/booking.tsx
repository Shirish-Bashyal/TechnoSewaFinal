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
import MaterialIcons from "@expo/vector-icons/MaterialIcons";
import { useShowAllBooking } from "@/services/api/booking";
import { useRouter } from "expo-router";

const booking = () => {
  const router = useRouter();
  const { data: bookingData, isError, isLoading } = useShowAllBooking();

  const handleShowPendingBookings = () => {
    router.push("/Bookings/pendingbooking");
  };
    const handleShowActiveBookings = () => {
    router.push("/Bookings/activebooking");
  };

  return (
    <SafeAreaView className="bg-gray-100 h-full">
      <ScrollView showsVerticalScrollIndicator={false}>
        <View className="flex-1 w-[98%] h-auto mt-8 px-3 py-1 !mr-10 ml-2  ">
         
         <TouchableOpacity  onPress={handleShowPendingBookings} className="cursor-pointer">
          <View className=" gap-4  h-auto mb-4 px-2 py-2 bg-white shadow-md shadow-zinc-400 rounded-lg">
            <View className="flex flex-row justify-between mx-4">
              <View className="flex flex-row gap-4">
                <MaterialIcons
                  name="arrow-drop-down"
                  size={24}
                  color="red"
                  style={{ marginTop: 2 }}
                />
                <Text
                  className="text-base font-outfit-bold text-danger mt-1"
                  style={{ fontFamily: "outfit-medium" }}
                >
                  Pending Bookings
                </Text>
              </View>
              <View>
                <Text
                  className="text-base font-outfit-bold text-danger mt-1"
                  style={{ fontFamily: "outfit-medium" }}
                >
                  {bookingData?.data?.pendingBookings?.length ?? 0}
                </Text>
              </View>
            </View>
          </View>
          </TouchableOpacity>
           <TouchableOpacity  onPress={handleShowActiveBookings} className="cursor-pointer">
          <View className=" gap-4  h-auto mb-4 px-2 py-2 bg-white shadow-md shadow-zinc-400 rounded-lg">
            <View className="flex flex-row justify-between mx-4">
              <View className="flex flex-row gap-4">
                <MaterialIcons
                  name="arrow-drop-down"
                  size={24}
                  color="blue"
                  style={{ marginTop: 2 }}
                />
                <Text
                  className="text-base font-outfit-bold text-blue-800 mt-1"
                  style={{ fontFamily: "outfit-medium" }}
                >
                  Active Bookings
                </Text>
              </View>
              <View>
                <Text
                  className="text-base font-outfit-bold text-blue-800 mt-1"
                  style={{ fontFamily: "outfit-medium" }}
                >
                  {bookingData?.data?.activeBookings?.length ?? 0}
                </Text>
              </View>
            </View>
          </View>
          </TouchableOpacity>
          <View
            className=" gap-4  h-auto mb-4 px-2 py-2 bg-white shadow-md shadow-zinc-400 rounded-lg"
            //   key={posts.id}
          >
            <View className="flex flex-row justify-between mx-4">
              <View className="flex flex-row gap-4">
                <MaterialIcons
                  name="arrow-drop-down"
                  size={24}
                  color="green"
                  style={{ marginTop: 2 }}
                />
                <Text
                  className="text-base font-outfit-bold text-green-700 mt-1"
                  style={{ fontFamily: "outfit-medium" }}
                >
                  Completed Bookings
                </Text>
              </View>
              <View>
                <Text
                  className="text-base font-outfit-bold text-green-700 mt-1"
                  style={{ fontFamily: "outfit-medium" }}
                >
                  {bookingData?.data?.completedBookings?.length ?? 0}
                </Text>
              </View>
            </View>
          </View>

          {/* ))} */}
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

export default booking;
