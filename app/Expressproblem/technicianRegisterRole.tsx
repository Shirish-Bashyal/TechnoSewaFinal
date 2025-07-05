import {
  View,
  Text,
  TouchableOpacity,
  ScrollView,
  TextInput,
  StyleSheet,
  Button,
  Image,
} from "react-native";
import React, { useEffect, useRef, useState } from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { useRouter } from "expo-router";
import AntDesign from "@expo/vector-icons/AntDesign";
import { MultipleSelectList } from "react-native-dropdown-select-list";
import { SelectList } from "react-native-dropdown-select-list";
import FontAwesome from "@expo/vector-icons/FontAwesome";
import { useForm } from "react-hook-form";
import { Platform } from "react-native";
import LeafletWebViewMap from "./WebMap";
import PhoneInput from "react-native-phone-input";
import { useCreateTechnicianRole } from "@/services/api/technicianRegisterRole";
import { createTechnicianRoleData } from "../../services/api/technicianRegisterRole";
import { ActivityIndicator, MD2Colors } from "react-native-paper";

type FormValues = {
  secondPhoneNumber: string;
  lattitude: number;
  longitude: number;
};

const technicianRegisterRole = () => {
  const router = useRouter();
  const [isFocused, setIsFocused] = useState(false);
  const { register, handleSubmit, setValue } = useForm<FormValues>();

  const [countryPickerVisible, setCountryPickerVisible] = useState(false);
  const phoneInputRef = useRef<PhoneInput>(null);
  const [latitude, setLatitude] = useState(27.69828);
  const [longitude, setLongitude] = useState(83.46188);

  const { mutate, isPending } = useCreateTechnicianRole();

  const handleOtp = async (data: createTechnicianRoleData) => {
    console.log("ok");
    mutate(data);
  };

  useEffect(() => {
    register("secondPhoneNumber", { required: "Phone number is required" });
    register("lattitude", {
      required: "Lattitude is required",
      valueAsNumber: true,
    });
    register("longitude", {
      required: "Longitude is required",
      valueAsNumber: true,
    });
  }, [register]);

  const handlePhoneChange = (number: string) => {
    const countryCode = phoneInputRef.current?.getCountryCode();
    const localNumber = number.replace(`+${countryCode}`, "");
    setValue("secondPhoneNumber", localNumber);
  };

  const toggleCountryPicker = () => {
    setCountryPickerVisible(!countryPickerVisible);
  };
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
            Register as a Technician
          </Text>
        </TouchableOpacity>
      </View>
      <ScrollView className="mt-28 px-2">
        <View className="m-2 flex flex-row gap-">
          <Text
            className="text-base text-black-300 "
            style={{ fontFamily: "outfit-light" }}
          >
            Enter your Phone Number
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
        <View className="mt-4">
          <View className="m-2 flex flex-row gap-">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Tap on the map to select your location
            </Text>
            <Text className="text-red-600 text-base ">*</Text>
          </View>
          <LeafletWebViewMap
            latitude={latitude}
            longitude={longitude}
            onSelectLocation={(lat, lng) => {
              setLatitude(lat);
              setLongitude(lng);
              setValue("lattitude", lat, { shouldValidate: true });
              setValue("longitude", lng, { shouldValidate: true });
            }}
          />
        </View>
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
              onPress={handleSubmit(handleOtp)}
              color="#7A4DFF"
            />
          </View>
        )}
      </ScrollView>
    </SafeAreaView>
  );
};

export default technicianRegisterRole;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    marginTop: 15,

    paddingHorizontal: 10,
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
