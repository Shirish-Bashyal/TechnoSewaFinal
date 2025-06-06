import { View, Text, TouchableOpacity, Image, Touchable, StyleSheet } from "react-native";
import React from "react";
import images from "@/constants/images";
import Entypo from "@expo/vector-icons/Entypo";
import FontAwesome from "@expo/vector-icons/FontAwesome";


interface Props {
  onPress?: () => void;
}

export const Services = ({ onPress }: Props) => {
  return (
   <TouchableOpacity
      onPress={onPress}
      className="flex-1 w-full mt-2 px-3 py-1  "
    >
      {/* <View className="flex flex-row items-center absolute px-2 top-5 right-5 bg-white/90 p1 rounded-full z-50">
        <FontAwesome name="star-half-empty" size={20} color="gold" />
        <Text className="text-xs font-rubik-bold text-primary-300 ml-0.5">
          4.4
        </Text>
      </View> */}
      <View className="flex flex-row gap-4 items-center">
      <Image
        source={images.coverimage}
        className="!w-24 !h-24 rounded-lg !object-fill"
      />
      <View className="flex flex-col mt-2">

        <View className="flex flex-row justify-between gap-2">
          <View>
          <Text
            className="text-xs font-outfit-bold text-black-100 mt-1"
            style={{ fontFamily: "outfit-medium" }}
          >
           By Team Nepal Air Condition
          </Text></View>
          <View className="flex flex-row gap-1">
            <Entypo name="star" size={18} color="gold" />
            <Text
              className="text-xs font-rubik-bold text-black-200 mt-1"
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
          </View>
        </View>
        <View>
           <Text
            className="text-base font-outfit-bold text-black-300 "
            style={{ fontFamily: "outfit-Medium" }}
          >
            AC Repairs and Maintenance
          </Text>
        </View>
        <View className="flex flex-row gap-1">
          <Text
            className="text-xs text-black-300"
            style={{ fontFamily: "rubik-light" }}
          >
            Starting at
          </Text>
          <Text
            className="text-xs font-rubik text-primary-100"
            style={{ fontFamily: "rubik-bold" }}
          >
            Rs,450
          </Text>
        </View>
      </View>
      </View>
    </TouchableOpacity>
  )
}

