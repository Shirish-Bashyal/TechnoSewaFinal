import { View, Text, TouchableOpacity, StyleSheet } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { OtpInput } from "react-native-otp-entry";

const otp = () => {
  return (
    <SafeAreaView>
      <View className="mt-7 ml-7 text-xl font-rubik-bold">
        Please Enter Otp
      </View>
      <View className="mt-1 ml-7 font-rubik-light">
        We'll text a code to verify your phone
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
        <TouchableOpacity className="bg-[#7A4DFF]/[1.6] shadow-md flex flex-row items-center  justify-center shadow-zinc-300 rounded-full w-[97%] h-18 py-4 mt-8 mx-2 ">
          <View className="text-lg font-rubik-bold text-white text-center">
           Confirm
          </View>
        </TouchableOpacity>
      </View>
       <TouchableOpacity  className="mt-5">
          <Text className="text-base underline text-primary-100 text-center" style={{fontFamily:'outfit-bold'}}>
           Re-send ?
          </Text>
        </TouchableOpacity>
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
