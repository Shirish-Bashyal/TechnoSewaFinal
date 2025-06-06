import {
  View,
  TextInput,
  ScrollView,
  Button,
  Text,
  TouchableOpacity,
} from "react-native";
import React from "react";
import { useForm } from "react-hook-form";
import { SafeAreaView } from "react-native-safe-area-context";

const userdetails = () => {
  const {
    register,
    handleSubmit,
    setValue,
    formState: { errors },
  } = useForm();

  const submitHandler = (data: any) => {
    console.log("data", data);
  };
  return (
    <SafeAreaView className="h-full bg-gray-100">
      <View className="flex justify-center items-center mt-8">
        <Text
          className="text-lg text-black-300"
          style={{ fontFamily: "rubik-bold" }}
        >
          Welcome to Techno Sewa!!
        </Text>
      </View>
      <View className="mt-1 text-sm flex justify-center items-center">
        <Text className="text-black-200" style={{ fontFamily: "rubik" }}>
          Enter your Details
        </Text>
      </View>
      <ScrollView contentContainerStyle={{ padding: 16 }}>
        <View style={{ gap: 12 }}>
          <View>
            <View className="mb-2 flex flex-row gap-">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                Full Name
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>
            <TextInput
              placeholder="Full Name"
              style={inputStyle}
              placeholderTextColor="#999"
              onChangeText={(text) => setValue("fullName", text)}
              {...register("fullName", { required: "FullName is required" })}
            />
          </View>
          {/* {errors.itemName && (
          <Text style={{ color: 'red' }}>{errors.itemName.message}</Text>
        )} */}

          <View>
            <View className="mb-2 flex flex-row gap-">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                Email
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>
            <TextInput
              placeholder="user@gmail.com"
              style={inputStyle}
              placeholderTextColor="#999"
              keyboardType="email-address"
              autoCapitalize="none"
              onChangeText={(text) => setValue("email", text)}
              {...register("email", {
                required: "Email is required",
                pattern: {
                  value: /^[a-zA-Z0-9._%+-]+@gmail\.com$/,
                  message: "Only Gmail addresses are allowed",
                },
              })}
            />
          </View>

          <View>
            <View className="mb-2 flex flex-row gap-">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                Address
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>
            <TextInput
              placeholder="Butwal"
              style={inputStyle}
              placeholderTextColor="#999"
              onChangeText={(text) => setValue("Address", text)}
              {...register("Address", { required: "Address is required" })}
            />
          </View>

          <View>
            <View className="mb-2 flex flex-row gap-">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                Phone Number
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>
            <TextInput
              placeholder="Phone Number"
              style={inputStyle}
              placeholderTextColor="#999"
              keyboardType="phone-pad" // opens numeric keyboard with symbols
              maxLength={10} // optional: limit digits
              onChangeText={(number) => setValue("phoneNumber", number)}
              {...register("phoneNumber", {
                required: "Phone Number is required",
                pattern: {
                  value: /^[0-9]+$/,
                  message: "Only numeric values are allowed",
                },
              })}
            />
          </View>

          <TouchableOpacity className="bg-[#7A4DFF]/[1.6] shadow-md flex flex-row items-center  justify-center shadow-zinc-300 rounded-full w-[97%] h-18 py-4 mt-5 mx-2 ">
            <Text className="text-lg font-rubik-bold text-white text-center">
              Next
            </Text>
          </TouchableOpacity>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

export default userdetails;

const inputStyle = {
  borderWidth: 1,
  borderColor: "#ccc",
  padding: 10,
  borderRadius: 8,
};
