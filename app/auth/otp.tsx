import { View, Text, TouchableOpacity, StyleSheet, Button } from "react-native";
import React, { useEffect, useRef, useState } from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { OtpInput } from "react-native-otp-entry";
import { useRouter } from "expo-router";
import { useForm } from "react-hook-form";
import PhoneInput from "react-native-phone-input";
import { useVerifyOtp, verifyData } from "@/services/api/auth";
import { ActivityIndicator, MD2Colors } from "react-native-paper";

type FormValues = {
  phoneNumber: string;
  otp: string;
};

const otp = () => {
  const router = useRouter();
  const { register, handleSubmit, setValue } = useForm<FormValues>();
  const phoneInputRef = useRef<PhoneInput>(null);
  const [countryPickerVisible, setCountryPickerVisible] = useState(false);
  const [isFocused, setIsFocused] = useState(false);
  const { mutate, isPending } = useVerifyOtp();

  const handleUserDetails = () => {
    router.push("/auth/userdetails");
  };

  const submitUserData = async (data: verifyData) => {
    console.log("ok");
    mutate(data);
  };
  useEffect(() => {
    register("phoneNumber", { required: "Phone number is required" });
    register("otp", { required: "Otp is required" });
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
          Please Enter Otp
        </Text>
      </View>
      <View className="mt-1 ml-7">
        <Text style={{ fontFamily: "rubik-light" }}>
          {" "}
          Verify your phone and Otp{" "}
        </Text>
      </View>
      <View className="mx-4 mt-4">
        <View className="mb-2 flex flex-row gap-0.5 ">
          <Text
            className="text-base text-black-300 "
            style={{ fontFamily: "outfit-light" }}
          >
            Phone Number
          </Text>
          <Text className="text-red-600 text-base ">*</Text>
        </View>
        <View style={styles.phoneContainer}>
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
        </View>
      </View>
      <View className="mt-16">
        <OtpInput
          numberOfDigits={4}
          focusColor="#7A4DFF"
          onTextChange={(text) => setValue("otp", text)}
          theme={{
            containerStyle: styles.container,
          }}
        />
      </View>
      <View className="mt-24">
        {/* <TouchableOpacity
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
        </TouchableOpacity> */}
        {isPending ? (
          <ActivityIndicator
            animating={true}
            color={MD2Colors.red800}
            style={{ marginTop: 8 }}
          />
        ) : (
          <View className="mt-8 mx-3">
            <Button
              title="Submit"
              onPress={handleSubmit(submitUserData)}
              color="#7A4DFF"
            />
          </View>
        )}

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
  phoneContainer: {
    flex: 1,
    marginTop: 5,
  },
  phoneInput: {
    height: 45,
    width: "100%",
    borderWidth: 1,
    borderColor: "#ccc",
    marginBottom: 20,
    paddingHorizontal: 10,
    borderRadius: 10,
  },
  phoneInputFocused: {
    borderWidth: 1,
  },
  phoneInputText: {
    borderWidth: 0,
  },
});
