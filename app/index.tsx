import {
  View,
  Text,
  ScrollView,
  Image,
  TouchableOpacity,
  StyleSheet,
  Alert,
  TextInput,
  Button,
} from "react-native";
import React, { useEffect, useRef, useState } from "react";
import { SafeAreaView } from "react-native-safe-area-context";

import images from "@/constants/images";
import { useRouter } from "expo-router";
import { useForm } from "react-hook-form";
import PhoneInput from "react-native-phone-input";
import { LoginData, useLogin } from "@/services/api/auth";

type FormValues = {
  phoneNumber: string;
  password: string;
};

const signIn = () => {
  const router = useRouter();
  const { register, handleSubmit, setValue } = useForm<FormValues>();
  const { mutate } = useLogin();

  const phoneInputRef = useRef<PhoneInput>(null);

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

  const handleLogin = () => {
    router.push("/auth/phone"); // Routes to /auth/phone
  };

  const handleRegister = () => {
    router.push("/auth/register");
  };
  const submitUserData = async (data: LoginData) => {
    console.log("ok");
    mutate(data);
  };
  useEffect(() => {
  register("phoneNumber", { required: "Phone number is required" });
  register("password", { required: "Password is required" });
}, [register]);

const handlePhoneChange = (number: string) => {
    const countryCode = phoneInputRef.current?.getCountryCode();
    const localNumber = number.replace(`+${countryCode}`, '');
    setValue('phoneNumber', localNumber);
  };
  return (
    <SafeAreaView className="bg-gray-100 h-full">
      <ScrollView contentContainerClassName="h-full">
        <View className=" w-full flex justify-center items-center">
          <Image
            source={images.logo}
            style={{
              paddingTop: 3,
              height: 240,
              borderRadius: 20,
            }}
            className="!w-[340px] mt-8 shadow-md shadow-zinc-300 rounded-full"
          />
        </View>
        <View className="px-10 mt-7">
          <Text className="text-sm text-center uppercase font-outfit-light text-black-200  pt-5">
            Welcome to Techno Sewa
          </Text>
        </View>
        <View className="mx-4">
          <View className="mb-2 flex flex-row gap-0.5 ">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Phone Number
            </Text>
            <Text className="text-red-600 text-base ">*</Text>
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
          </View>
        </View>

        <View className="mx-4">
          <View className="mb-2 flex flex-row gap-">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Password
            </Text>
            <Text className="text-red-600 text-base ">*</Text>
          </View>
          <TextInput
            placeholder="Password"
            style={inputStyle}
            placeholderTextColor="#999"
            onChangeText={(text) => setValue("password", text)}
          />
        </View>
        {/* className="bg-[#7A4DFF]/[1.6] shadow-md flex flex-row items-center  justify-center shadow-zinc-300 rounded-full w-[97%] h-14 py-4 mt-8 mx-2 " */}
        <View className="mt-8 mx-3">
        <Button title="Submit" onPress={handleSubmit(submitUserData)} color='#7A4DFF'/>
        </View>

        {/* <View className="text-lg font-rubik-bold text-white text-center">
            <Text className="text-white" style={{ fontFamily: "rubik-bold" }}>
              {" "}
              Login{" "}
            </Text>
          </View> */}

        <TouchableOpacity className="mt-5">
          <View className="flex flex-row justify-center items-center gap-2">
            <Text
              className="text-base  text-black text-center"
              style={{ fontFamily: "rubik-light" }}
            >
              Forgot
            </Text>
            <Text
              className="text-base underline text-primary-100 text-center"
              style={{ fontFamily: "outfit-bold" }}
            >
              Password ?
            </Text>
          </View>
        </TouchableOpacity>
        <View className="mb-4">
          <TouchableOpacity
            onPress={handleRegister}
            className=" flex flex-row items-center  justify-center mt-2 mx-2"
          >
            <View className="flex flex-row items-center justify-center">
              <Text>Don't have an Account?</Text>
              <Text className="text-base font-rubik-Medium text-primary-100 ml-2">
                Create New Account
              </Text>
            </View>
          </TouchableOpacity>
        </View>
        <View className="flex justify-center items-center text-black-100">
          <Text> .....</Text>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

export default signIn;
const styles = StyleSheet.create({
  container: {
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

const inputStyle = {
  borderWidth: 1,
  borderColor: "#ccc",
  padding: 10,
  borderRadius: 8,
};
