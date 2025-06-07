import {
  View,
  Text,
  TouchableOpacity,
  ScrollView,
  TextInput,
  StyleSheet,
} from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { useRouter } from "expo-router";
import AntDesign from "@expo/vector-icons/AntDesign";
import { MultipleSelectList } from "react-native-dropdown-select-list";
import { SelectList } from "react-native-dropdown-select-list";
import FontAwesome from "@expo/vector-icons/FontAwesome";
import { useForm } from "react-hook-form";

const ExpressProblem = () => {
  const {
    register,
    handleSubmit,
    setValue,
    formState: { errors },
  } = useForm();
  const router = useRouter();
  const [selected, setSelected] = React.useState([]);
  const [text, setText] = React.useState("");

  const data = [
    { key: "1", value: "Plumbing" },
    { key: "2", value: "Electrician" },
    { key: "3", value: "House Keeping" },
    { key: "4", value: "Automobiles" },
    { key: "5", value: "Tech Experts" },
    { key: "6", value: "Carpenter" },
  ];

  return (
    <SafeAreaView className="bg-gray-100 h-full">
      <View className="absolute bg-primary-100/70 h-28 flex justify-center top-0 left-0 right-0 z-10">
        <TouchableOpacity
          onPress={router.back}
          className="flex flex-row  gap-4 mx-4 py-10"
        >
          <AntDesign name="back" size={34} color="white" />
          <Text
            className="text-white   text-[20px]"
            style={{ fontFamily: "rubik-bold", letterSpacing: 1.5 }}
          >
            Express Your Problem
          </Text>
        </TouchableOpacity>
      </View>
      <ScrollView className="mt-28 px-2">
        <View>
          <View className="my-2">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Select Category
            </Text>
          </View>
          <SelectList
            setSelected={(val: any) => setSelected(val)}
            data={data}
            boxStyles={{ borderRadius: 8, borderColor: "#ccc" }}
            defaultOption={{ key: "1", value: "Plumbing" }}
          />
        </View>
        <View className="mt-4">
          <View className="mb-2 flex flex-row gap-">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Enter Title of the Problem
            </Text>
            <Text className="text-red-600 text-base ">*</Text>
          </View>
          <TextInput
            placeholder="Water pipe leakage"
            style={inputStyle}
            placeholderTextColor="#999"
            onChangeText={(text) => setValue("problemTitle", text)}
            {...register("problemTitle", {
              required: "problemTitle is required",
            })}
          />
        </View>
        <View className="mt-4">
          <View className="mb-2 flex flex-row gap-">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Describe the Problem
            </Text>
            <Text className="text-red-600 text-base ">*</Text>
          </View>

          <TextInput
            value={text}
            onChangeText={setText}
            placeholder="Write your message..."
            multiline
            numberOfLines={4}
            style={styles.textArea}
            placeholderTextColor="#999"
          />
        </View>
        <View className="mt-4">
          <View className="mb-2 flex flex-row gap-">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Upload Image
            </Text>
            <Text className="text-red-600 text-base ">*</Text>
          </View>

         
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

export default ExpressProblem;

const inputStyle = {
  borderWidth: 1,
  borderColor: "#ccc",
  padding: 10,
  borderRadius: 8,
};

const styles = StyleSheet.create({
  textArea: {
    height: 120,
    textAlignVertical: "top", // Ensure text starts at the top-left
    borderColor: "#ccc",
    borderWidth: 1,
    borderRadius: 8,
    padding: 12,
   
  },
});
