import { View, Text, ScrollView, Image, TouchableOpacity } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import images from "@/constants/images";
import FontAwesome6 from "@expo/vector-icons/FontAwesome6";
import Entypo from "@expo/vector-icons/Entypo";
import MaterialIcons from '@expo/vector-icons/MaterialIcons';
import { useViewProfile } from "@/services/api/profile";

interface SettingsItemProps {
  icon: any;
  title: string;
  onPress?: () => void;
  textStyle?: any;
  showArrow?: boolean;
}

const SettingsItem = ({
  icon,
  title,
  onPress,
  textStyle,
  showArrow = true,
}: SettingsItemProps) => (
  <TouchableOpacity
    onPress={onPress}
    className="flex flex-row  py-2 border-t pt-2 mt-3 border-gray-100"
  >
    <View className="flex flex-row items-center gap-3">
      <Entypo name={icon} size={20} color="black" />
      <Text className={`text-lg font-rubik-light text-black-300 ${textStyle}`} style={{fontFamily:'rubik'}}>
        {title}
      </Text>
    </View>
    {/* <View className="">
    {showArrow && <Entypo name="chevron-right" size={24} color="black" />}
    </View> */}
  </TouchableOpacity>
);

const Profile = () => {
  const handleLogout = async () => {};
  const { data: profileData, isError, isLoading } = useViewProfile();
  return (
    <SafeAreaView className="h-full bg-white">
      <ScrollView
        showsVerticalScrollIndicator={false}
        contentContainerClassName="pb-32 px-7"
      >
        <View className="flex flex-row items-center justify-between rounded-lg  mt-5 p-2 bg-[#E6E6FA] w-full h-12 ">
          <Text className="text-xl font-rubik-bold " style={{fontFamily:'rubik-bold'}}>Profile</Text>
        </View>
        <View className="flex-row gap-10 flex mt-5">
          <View className="flex flex-col items-center relative mt-5">
            <Image
              source={images.avatar}
              className="!size-36 relative rounded-full"
            />
            <TouchableOpacity className="absolute bottom-11 right-2">
              <FontAwesome6 name="edit" size={20} color="black" />
            </TouchableOpacity>
            <Text className="text-2l font-rubik-bold mt-2" style={{fontFamily:'rubik-bold'}}>{profileData?.data?.name}</Text>
          </View>
          <View className="mt-14 ">
            <Text className="font-rubik-bold text-base" style={{fontFamily:'rubik-bold'}}>{profileData?.data?.name}</Text>
            <Text className="font-outfit-medium" style={{fontFamily:'outfit-medium'}}>{profileData?.data?.phoneNumber}</Text>
            <Text className="font-outfit-medium" style={{fontFamily:'outfit-medium'}}>{profileData?.data?.email}</Text>
          </View>
        </View>
        <View className="flex flex-col mt-10 ">
          <SettingsItem icon="calendar" title="My Booking" />
          <SettingsItem icon="credit-card" title="Payments" />
          <SettingsItem icon="tools" title="Become technician" />
        </View>
       
        
        <View className="flex flex-col mt-2 border-t pt-2 border-gray-100 ">
          <SettingsItem
            icon="log-out"
            title="Log Out"
            textStyle="text-gray-500"
            showArrow={false}
            onPress={handleLogout}
          />
        </View>
        <View className="flex flex-col mt-1 border-t pt-2 border-gray-100 ">

        </View>
         <TouchableOpacity className="mt-2">
        <View className="flex flex-row mt-3 border-t pt-2 border-gray-100 gap-2 ">
          <MaterialIcons name="delete" size={24} color="red" />
          <Text className="text-lg font-rubik-light text-danger" style={{fontFamily:'rubik'}}>Delete Account</Text>
        </View>
        </TouchableOpacity>
      </ScrollView>
    </SafeAreaView>
  );
};

export default Profile;
