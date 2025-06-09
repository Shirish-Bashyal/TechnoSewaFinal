import { View, Text, FlatList, TouchableOpacity, Image, Button } from "react-native";
import React, { useEffect, useState } from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { Card, FeaturedCard } from "@/components/Cards";
import { Services, Technician } from "../../components/services-category";
import { useRouter } from "expo-router";
import AntDesign from "@expo/vector-icons/AntDesign";
import images from "@/constants/images";
import Entypo from "@expo/vector-icons/Entypo";
import DateTimePicker from '@react-native-community/datetimepicker';
import { useForm } from "react-hook-form";


const aboutTechnician = () => {
  const router = useRouter();
  const { register, setValue, handleSubmit } = useForm();
  const [showDatePicker, setShowDatePicker] = useState(false);
  const [showTimePicker, setShowTimePicker] = useState(false);
  const [selectedDate, setSelectedDate] = useState(new Date());
  const [selectedTime, setSelectedTime] = useState(new Date());

  useEffect(() => {
    register("datetime", { required: "Date and time are required" });
  }, [register]);

  const onDateChange = (event:any, date:any) => {
    if (date) {
      setSelectedDate(date);
      // Combine with current time
      const updatedDateTime = new Date(
        date.getFullYear(),
        date.getMonth(),
        date.getDate(),
        selectedTime.getHours(),
        selectedTime.getMinutes()
      );
      setValue("datetime", updatedDateTime.toISOString());
    }
    setShowDatePicker(false);
  };

  const onTimeChange = (event:any, time:any) => {
    if (time) {
      setSelectedTime(time);
      // Combine with selected date
      const updatedDateTime = new Date(
        selectedDate.getFullYear(),
        selectedDate.getMonth(),
        selectedDate.getDate(),
        time.getHours(),
        time.getMinutes()
      );
      setValue("datetime", updatedDateTime.toISOString());
    }
    setShowTimePicker(false);
  };

  const onSubmit = (data:any) => {
    console.log("Submitted datetime:", data);
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
            Book Technician
          </Text>
        </TouchableOpacity>
      </View>
    
      <View className="flex flex-row gap-4 mt-20 mx-3 items-center">
        <Image
          source={images.avatar}
          className="!w-20 !h-20 rounded-full !object-fill"
        />
        <View className="flex flex-col mt-2">
          <View className="flex flex-row justify-between gap-2"></View>
          <View>
            <Text
              className="text-base font-outfit-bold text-black-300 "
              style={{ fontFamily: "outfit-Medium" }}
            >
              John Deo
            </Text>
          </View>
          <View className="flex flex-row gap-1">
            <Text
              className="text-xs text-black-300"
              style={{ fontFamily: "rubik-light" }}
            >
              Butwal-15
            </Text>
            {/* <Text
              className="text-xs font-rubik text-primary-100"
              style={{ fontFamily: "rubik-bold" }}
            >
              Rs,450
            </Text> */}
          </View>
        </View>
      </View>
      {/* <View className="mb-2 mx-4">
        <Text
              className="text-base font-rubik text-black-300"
              style={{ fontFamily: "poppins" }}
            >
              I am an experienced plumber with 10 years of experience.
            </Text>
      </View> */}
      <View className="flex flex-row mx-4 mt-2 gap-4">
        <View className="bg-gray-50 rounded-full cursor-pointer flex justify-center flex-col items-center h-auto  px-4 py-1 shadow-black-100 shadow-sm ">
          <View>
            <Text
              className="text-xs font-rubik-bold text-black-200 mt-1"
              style={{ fontFamily: "rubik-bold" }}
            >
              Ratings
            </Text>
          </View>
          <View className="  flex flex-row gap-1">
            <Entypo name="star" size={18} color="gold" />
            <Text
              className="text-xs font-rubik-bold text-black-100 mt-1"
              style={{ fontFamily: "rubik-bold" }}
            >
              4.4
            </Text>
            <Text
              className="text-xs font-rubik-bold text-black-100 mt-1"
              style={{ fontFamily: "rubik-bold" }}
            >
              (12)
            </Text>
          </View>
        </View>
        <View className="bg-gray-50 cursor-pointer rounded-full flex justify-center flex-col items-center h-auto  px-4 py-1 shadow-black-100 shadow-sm ">
          <View>
            <Text
              className="text-xs font-rubik-bold text-black-200 mt-1"
              style={{ fontFamily: "rubik-bold" }}
            >
              Total Services
            </Text>
          </View>
          <View className="  flex flex-row gap-1">
            <Text
              className="text-xs font-rubik-bold text-black-100 mt-1"
              style={{ fontFamily: "rubik-bold" }}
            >
              20
            </Text>
          </View>
        </View>
        <View className="bg-gray-50 cursor-pointer rounded-full flex justify-center flex-col items-center h-auto  px-4 py-1 shadow-black-100 shadow-sm ">
          <View>
            <Text
              className="text-xs font-rubik-bold text-black-200 mt-1"
              style={{ fontFamily: "rubik-bold" }}
            >
              Reviews
            </Text>
          </View>
          <View className="  flex flex-row gap-1">
            <Text
              className="text-xs font-rubik-bold text-black-100 mt-1"
              style={{ fontFamily: "rubik-bold" }}
            >
              10
            </Text>
          </View>
        </View>
      </View>
      <View className="mt-5 mx-4">
        <Text
              className="text-base font-rubik text-primary-100"
              style={{ fontFamily: "rubik-bold" }}
            >
              Available Date and Time
            </Text>
      </View>
      <View>
       <View style={{ padding: 20,marginTop:10 }}>
      <Button title="Pick Date" onPress={() => setShowDatePicker(true)} />
      <Button title="Pick Time" onPress={() => setShowTimePicker(true)} />

      <Text style={{ marginTop: 20 }}>
        Selected:{" "}
        {`${selectedDate.toLocaleDateString()} ${selectedTime.toLocaleTimeString()}`}
      </Text>

      {showDatePicker && (
        <DateTimePicker
          value={selectedDate}
          mode="date"
          display="default"
          onChange={onDateChange}
        />
      )}

      {showTimePicker && (
        <DateTimePicker
          value={selectedTime}
          mode="time"
          display="default"
          onChange={onTimeChange}
          className="!mt-7"
        />
      )}

      <Button title="Submit" onPress={handleSubmit(onSubmit)} />
    </View>
      </View>

    </SafeAreaView>
  );
};

export default aboutTechnician;
