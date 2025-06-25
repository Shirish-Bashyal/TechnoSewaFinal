import React, { useEffect, useRef, useState } from "react";
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
import { useForm } from "react-hook-form";
import { phoneData, useSendOtp } from "@/services/api/auth";

type FormValues = {
  phoneNumber: string;
};

const phone = () => {
  const router = useRouter();
  const [isFocused, setIsFocused] = useState(false);
  const { register, handleSubmit, setValue } = useForm<FormValues>();

  const [countryPickerVisible, setCountryPickerVisible] = useState(false);
  const phoneInputRef = useRef<PhoneInput>(null);

  const { mutate } = useSendOtp();

  const handleOtp = async (data: phoneData) => {
    console.log("ok");
    mutate(data);
  };

  useEffect(() => {
    register("phoneNumber", { required: "Phone number is required" });
  }, [register]);

  const handlePhoneChange = (number: string) => {
    const countryCode = phoneInputRef.current?.getCountryCode();
    const localNumber = number.replace(`+${countryCode}`, "");
    setValue("phoneNumber", localNumber);
  };

  const toggleCountryPicker = () => {
    setCountryPickerVisible(!countryPickerVisible);
  };
  return (
    <SafeAreaView className="h-full bg-gray-100">
      <View className="mt-7 ml-7">
        <Text className="text-xl" style={{ fontFamily: "rubik-bold" }}>
          Join us via Phone Number
        </Text>
      </View>
      <View className="mt-1 ml-7">
        <Text style={{ fontFamily: "rubik-light" }}>
          {" "}
          We'll text a code to verify your phone{" "}
        </Text>
      </View>
      <View style={styles.container}>
        <PhoneInput
          ref={phoneInputRef}
          initialCountry="np"
          onChangePhoneNumber={handlePhoneChange}
          textProps={{
            placeholder: "Phone number",
            onFocus: () => setIsFocused(true),
            onBlur: () => setIsFocused(false),
          }}
          onPressFlag={toggleCountryPicker}
          style={[styles.phoneInput, isFocused && styles.phoneInputFocused]}
          textStyle={styles.phoneInputText}
        />
        <View className="mt-8 mx-3">
          <Button
            title="Submit"
            onPress={handleSubmit(handleOtp)}
            color="#7A4DFF"
          />
        </View>
        {/* <TouchableOpacity
          onPress={handleSubmit(handleOtp)}
          className="bg-[#7A4DFF]/[1.6] shadow-md flex flex-row items-center  justify-center shadow-zinc-300 rounded-full w-[97%] h-14 py-4 mt-8 mx-2 "
        >
          <View className="text-lg font-rubik-bold text-white text-center">
           <Text className="text-white" style={{ fontFamily: "rubik-bold" }}> Next </Text>
          </View>
        </TouchableOpacity> */}
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
