import React, { useState } from "react";
import { View, TextInput, Button, Alert, StyleSheet, TouchableOpacity } from "react-native";
import PhoneInput from "react-native-phone-input";
import {  SafeAreaView } from "react-native-safe-area-context";

const phone = () => {
  const [phoneNumber, setPhoneNumber] = useState("");
  const [isFocused, setIsFocused] = useState(false);

  const [countryPickerVisible, setCountryPickerVisible] = useState(false);

  const onSubmit = () => {
    // Perform your desired action with
    // the phone number and country code
    Alert.alert(
      "Form Submitted",
      `Phone Number: ${phoneNumber}
        `
    );
  };

  const toggleCountryPicker = () => {
    setCountryPickerVisible(!countryPickerVisible);
  };
  return (
    <SafeAreaView className="h-full bg-white">
      <View className="mt-7 ml-7 text-xl font-rubik-bold">Join us via Phone Number</View>
      <View className="mt-1 ml-7 font-rubik-light">We'll text a code to verify your phone</View>
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
       <TouchableOpacity className="bg-[#7A4DFF]/[1.6] shadow-md flex flex-row items-center  justify-center shadow-zinc-300 rounded-full w-[97%] h-18 py-4 mt-8 mx-2 "
               >
                 <View className="text-lg font-rubik-bold text-white text-center">
                  Next
                  </View></TouchableOpacity>
    </View>
   
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    marginTop:15,
    
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
    borderWidth:1
  },
   phoneInputText: {
    borderWidth:0
  },
  
});

export default phone;
