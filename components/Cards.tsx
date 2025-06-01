import { View, Text, TouchableOpacity, Image, Touchable, StyleSheet } from "react-native";
import React from "react";
import images from "@/constants/images";
import Entypo from "@expo/vector-icons/Entypo";
import FontAwesome from "@expo/vector-icons/FontAwesome";

interface Props {
  onPress?: () => void;
}

export const FeaturedCard = ({ onPress }: Props) => {
  return (
    <>
      <TouchableOpacity
        onPress={onPress}
        className="flex flex-col items-start w-56 h-72 relative"
      >
        
        <Image
          source={images.coverimage}
           style={styles.image}
        />
        <View style={styles.overlay} />

        <View className="flex flex-row items-center bg-white/85 px-3 py-1.5 rounded-full absolute top-5 right-5">
          <FontAwesome name="star-half-empty" size={20} color="gold" />
          <Text className="text-xs font-rubik-bold text-black-300 ml-1" style={{fontFamily:'rubik-bold'}}>
            4.4
          </Text>
        </View>
        <View className="flex flex-col items-start absolute bottom-5 inset-x-5">
          <View className="flex flex-row items-center gap-7 w-full">
          <Text className="text-base font-rubik-bold mx-2 mt-4 text-white " style={{fontFamily:'rubik-bold'}}>
            Electrican Package
          </Text>
         
        </View>
        <View className="flex flex-row items-center justify-between w-full">
          <Text className="text-sm  text-white mx-2 " style={{fontFamily:'outfit-light'}}>Rs,5000</Text>
        </View>
        </View>

        
      </TouchableOpacity>
    </>
  );
};

export const Card = ({ onPress }: Props) => {
  return (
    <TouchableOpacity
      onPress={onPress}
      className="flex-1 w-full mt-4 px-3 py-4 rounded-lg bg-white shadow-lg shadow-black-100/70 relative "
    >
      {/* <View className="flex flex-row items-center absolute px-2 top-5 right-5 bg-white/90 p1 rounded-full z-50">
        <FontAwesome name="star-half-empty" size={20} color="gold" />
        <Text className="text-xs font-rubik-bold text-primary-300 ml-0.5">
          4.4
        </Text>
      </View> */}
      <Image
          source={images.coverimage}
          className="!w-full !h-24 rounded-lg !object-fill"
        />
      <View className="flex flex-col mt-2">
        <View className="flex flex-row justify-between">
        <Text className="text-base font-outfit-bold text-black-300 " style={{fontFamily:'outfit-Medium'}}>
          Cleaner
        </Text>
        <View className="flex flex-row gap-1">
        <Entypo name="star" size={18} color="gold" />
        <Text className="text-xs font-rubik-bold text-black-200 mt-1" style={{fontFamily:'rubik-bold'}}>
          4.4
        </Text>
        <Text className="text-xs font-rubik-bold text-black-100 mt-1" style={{fontFamily:'rubik-bold'}}>(12)</Text>
        </View>
        </View>
        <Text className="text-xs font-rubik text-primary-100" style={{fontFamily:'rubik-bold'}}>Rs,450</Text>
      </View>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  imageContainer: {
    position: 'relative',
    width: '100%',
    height: 200, // or whatever size
    borderRadius: 16,
    overflow: 'hidden',
  },
  image: {
    width: '100%',
    height: '100%',
    borderRadius:26,
  },
  overlay: {
    borderRadius:16,
    ...StyleSheet.absoluteFillObject,
    backgroundColor: 'rgba(0, 0, 0, 0.2)', // Decrease brightness
    // For increased saturation, overlay a semi-transparent colored view
  },
});
