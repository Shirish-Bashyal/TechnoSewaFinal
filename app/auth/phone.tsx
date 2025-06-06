import React, { useState } from "react";
import {
  View,
  TextInput,
  Button,
  Alert,
  StyleSheet,
  TouchableOpacity,
  Text,
} from "react-native";
import PhoneInput from "react-native-phone-input";
import { SafeAreaView } from "react-native-safe-area-context";
import { useRouter } from "expo-router";

const phone = () => {
  const router = useRouter();
  const [phoneNumber, setPhoneNumber] = useState("");
  const [isFocused, setIsFocused] = useState(false);

  const [countryPickerVisible, setCountryPickerVisible] = useState(false);

  const handleOtp = () => {
    Alert.alert(
      "Form Submitted",
      `Phone Number: ${phoneNumber}
        `
    );
    router.push("/auth/otp");
  };

  const toggleCountryPicker = () => {
    setCountryPickerVisible(!countryPickerVisible);
  };
  return (
    <SafeAreaView className="h-full bg-gray-100">
      <View className="mt-7 ml-7">
        <Text className="text-xl"  style={{ fontFamily: "rubik-bold" }}>Join us via Phone Number</Text>
      </View>
      <View className="mt-1 ml-7">
       <Text style={{ fontFamily: "rubik-light"}}> We'll text a code to verify your phone </Text>
      </View>
      <View style={styles.container}>
        <PhoneInput
          initialCountry={"np"}
          initialValue={phoneNumber}
          textProps={{
            placeholder: "Phone number",
            onFocus: () => setIsFocused(true),
            onBlur: () => setIsFocused(false),
          }}
          onChangePhoneNumber={(number) => setPhoneNumber(number)}
          onPressFlag={toggleCountryPicker}
          style={[styles.phoneInput, isFocused && styles.phoneInputFocused]}
          textStyle={styles.phoneInputText}
        />
        <TouchableOpacity
          onPress={handleOtp}
          className="bg-[#7A4DFF]/[1.6] shadow-md flex flex-row items-center  justify-center shadow-zinc-300 rounded-full w-[97%] h-14 py-4 mt-8 mx-2 "
        >
          <View className="text-lg font-rubik-bold text-white text-center">
           <Text className="text-white" style={{ fontFamily: "rubik-bold" }}> Next </Text>
          </View>
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    marginTop: 15,

    paddingHorizontal: 20,
  },
  phoneInput: {
    height: 50,
    width: "100%",
    borderWidth: 1,
    borderColor: "#ccc",
    marginBottom: 20,
    paddingHorizontal: 10,
    borderRadius: 14,
  },
  phoneInputFocused: {
    borderWidth: 1,
  },
  phoneInputText: {
    borderWidth: 0,
  },
});

export default phone;
