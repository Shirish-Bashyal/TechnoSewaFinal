import { View, Text, TouchableOpacity } from "react-native";
import React from "react";
import Entypo from "@expo/vector-icons/Entypo";
import FontAwesome5 from "@expo/vector-icons/FontAwesome5";
import { useRouter } from "expo-router";

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
    className="flex flex-row justify-between "
  >
    <View className="flex items-center">
      <FontAwesome5 name={icon} size={20} color="black" />
      <Text
        className={`text-xs font-rubik-light text-black-300 ${textStyle}`}
        style={{ fontFamily: "rubik" }}
      >
        {title}
      </Text>
    </View>
  </TouchableOpacity>
);
const Categories = () => {
    const router = useRouter();
   const handleShowCategory = () => {
     
      router.push("/category_components/[id]");
    };
  return (
    <View className="my-5">
      <View className="flex flex-row justify-between ">
        <SettingsItem icon="plug" title="Electrician" onPress={handleShowCategory}/>
        <SettingsItem icon="tools" title="Plumbing" />
        <SettingsItem icon="broom" title="Cleaner" />
        <SettingsItem icon="paint-roller" title="House Paint" />
      </View>
      <View className="flex flex-row gap-12 mt-5 ">
        <SettingsItem icon="car" title="Automobile" />
        <SettingsItem icon="laptop-code" title="Tech Expert" />
        <SettingsItem icon="question-circle" title="Others" />
       
      </View>
    </View>
  );
};

export default Categories;
