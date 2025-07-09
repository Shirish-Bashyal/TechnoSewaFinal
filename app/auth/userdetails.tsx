import {
  View,
  TextInput,
  ScrollView,
  Button,
  Text,
  TouchableOpacity,
   KeyboardAvoidingView,
   StyleSheet,
} from "react-native";
import React, { useEffect, useRef, useState } from "react";
import { useForm } from "react-hook-form";
import { SafeAreaView } from "react-native-safe-area-context";
import { useRouter } from "expo-router";
import FontAwesome from "@expo/vector-icons/FontAwesome";
import { SignUpData, useSignUp } from "@/services/api/auth";
import { ActivityIndicator, MD2Colors } from 'react-native-paper';
// import { Toaster } from 'react-hot-toast';

type FormValues = {
  fullName: string;
  email: string;
  password: string;
  confirmPassword: string;
  phoneNumber: string;
  city: string;
  wardNo: number;
  toleName: string;
};

const userdetails = () => {
  const router = useRouter();
  const {
    register,
    handleSubmit,
    setValue,
    formState: { errors },
  } = useForm<FormValues>();
  const { mutate,isPending } = useSignUp();
  const [showPassword, setShowPassword] = useState(false);
  const passwordRef = useRef<TextInput>(null);
  const scrollRef = useRef<ScrollView>(null);

  useEffect(() => {
    // Register field manually
    register("password", { required: "Password is required" });
  }, [register]);

  const onSubmit = (data: any) => {
    console.log("Form data:", data);
  };

   const submitUserData = async (data: SignUpData) => {
      console.log("ok");
      mutate(data);
    };
  const handleTech = () => {
    router.push("/auth/tech");
  };

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
         {/* <Toaster position="bottom-center" reverseOrder={false} /> */}
      </View>
       <KeyboardAvoidingView
                behavior="height"
                style={{ flex: 1 }}
                keyboardVerticalOffset={0}
              >
                <ScrollView
                  ref={scrollRef}
                  contentContainerStyle={styles.chatContainer}
                  showsVerticalScrollIndicator={false}
                >
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
                City
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>
            <TextInput
              placeholder="Butwal"
              style={inputStyle}
              placeholderTextColor="#999"
              onChangeText={(text) => setValue("city", text)}
              {...register("city", { required: "city is required" })}
            />
          </View>
          <View>
            <View className="mb-2 flex flex-row gap-">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                Ward NO.
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>
            <TextInput
              placeholder="2"
              style={inputStyle}
              keyboardType="phone-pad"
              maxLength={2}
              placeholderTextColor="#999"
              onChangeText={(text) => setValue("wardNo", Number(text))}
              {...register("wardNo", { required: "ward is required" })}
            />
          </View>
          <View>
            <View className="mb-2 flex flex-row gap-">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                Tole Name
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>
            <TextInput
              placeholder="Tole Name"
              style={inputStyle}
              placeholderTextColor="#999"
              onChangeText={(text) => setValue("toleName", text)}
              {...register("toleName", { required: "Tole is required" })}
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

          <View>
            <View className="mb-2 flex flex-row gap-">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                Password
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>
            <View style={inputStyle}>
              <View style={{ flexDirection: "row", alignItems: "center" }}>
                <TextInput
                  ref={passwordRef}
                  secureTextEntry={!showPassword}
                  placeholderTextColor="#999"
                  placeholder="1 Capital | 1 symbol | 1 Number | 6 Character"
                  onChangeText={(text) => setValue("password", text)}
                  style={{
                    flex: 1,

                    borderColor: errors.password ? "red" : "#ccc",
                    padding: 2,
                  }}
                />
                <TouchableOpacity
                  onPress={() => setShowPassword(!showPassword)}
                >
                  <Text style={{ marginLeft: 10 }}>
                    {showPassword ? (
                      <FontAwesome name="eye-slash" size={20} color="black" />
                    ) : (
                      <FontAwesome name="eye" size={20} color="black" />
                    )}
                  </Text>
                </TouchableOpacity>
              </View>
            </View>
            {/* {errors.password && (
        <Text style={{ color: 'red' }}>{errors.password.message}</Text>
      )}
      <Button title="Submit" onPress={handleSubmit(onSubmit)} /> */}
          </View>

          <View>
            <View className="mb-2 flex flex-row gap-">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                Confirm Password
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>
            <View style={inputStyle}>
              <View style={{ flexDirection: "row", alignItems: "center" }}>
                <TextInput
                  ref={passwordRef}
                  secureTextEntry={!showPassword}
                  placeholderTextColor="#999"
                  placeholder=" Confirm Password"
                  onChangeText={(text) => setValue("confirmPassword", text)}
                  style={{
                    flex: 1,

                    borderColor: errors.password ? "red" : "#ccc",
                    padding: 2,
                  }}
                />
                <TouchableOpacity
                  onPress={() => setShowPassword(!showPassword)}
                >
                  <Text style={{ marginLeft: 10 }}>
                    {showPassword ? (
                      <FontAwesome name="eye-slash" size={20} color="black" />
                    ) : (
                      <FontAwesome name="eye" size={20} color="black" />
                    )}
                  </Text>
                </TouchableOpacity>
              </View>
            </View>
            {/* {errors.password && (
        <Text style={{ color: 'red' }}>{errors.password.message}</Text>
      )}
      */}
          </View>
          {isPending? (
        <ActivityIndicator animating={true} color={MD2Colors.red800} style={{marginTop:8}} />
       ) : (
           <Button title="Submit" onPress={handleSubmit(submitUserData)} color='#7A4DFF'/>
       )}
          {/* <TouchableOpacity
            onPress={handleTech}
            className="bg-[#7A4DFF]/[1.6] shadow-md flex flex-row items-center  justify-center shadow-zinc-300 rounded-full w-[97%] h-18 py-4 mt-5 mx-2 "
          >
            <Text className="text-lg font-rubik-bold text-white text-center">
              Next
            </Text>
          </TouchableOpacity> */}
        </View>
        </ScrollView>
        
      </ScrollView>
      </KeyboardAvoidingView>
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

const styles = StyleSheet.create({
  chatContainer: {
    padding: 16,
    paddingBottom: 60,
  },
})