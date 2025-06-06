import { View, Text, TouchableOpacity, StyleSheet } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { OtpInput } from "react-native-otp-entry";
import { useRouter } from "expo-router";

const otp = () => {
  const router = useRouter();

  const handleUserDetails = () => {
    router.push("/auth/userdetails");
  };
  return (
    <SafeAreaView className="h-full bg-gray-100">
      <View className="mt-7 ml-7">
        <Text className="text-xl" style={{ fontFamily: "rubik-bold" }}>
          Please Enter Otp
        </Text>
      </View>
      <View className="mt-1 ml-7">
        <Text style={{ fontFamily: "rubik-light" }}>
          {" "}
          We'll text a code to verify your phone{" "}
        </Text>
      </View>
      <View>
        <OtpInput
          numberOfDigits={4}
          focusColor="#7A4DFF"
          onTextChange={(text) => console.log(text)}
          theme={{
            containerStyle: styles.container,
          }}
        />
      </View>
      <View className="mt-24">
        <TouchableOpacity
          onPress={handleUserDetails}
          className="bg-[#7A4DFF]/[1.6] shadow-md flex flex-row items-center  justify-center shadow-zinc-300 rounded-full w-[97%] h-18 py-4 mt-18 mx-2 "
        >
          <View className=" text-center">
            <Text
              className="text-lg  text-white"
              style={{ fontFamily: "rubik-bold" }}
            >
              {" "}
              Confirm
            </Text>
          </View>
        </TouchableOpacity>

        <TouchableOpacity className="mt-5">
          <Text
            className="text-base underline text-primary-100 text-center"
            style={{ fontFamily: "outfit-bold" }}
          >
            Re-send ?
          </Text>
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );
};

export default otp;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    marginTop: 15,
    paddingHorizontal: 20,
  },
});
