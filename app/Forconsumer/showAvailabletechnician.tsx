import React, { useState } from "react";
import {
  View,
  Text,
  TextInput,
  Button,
  TouchableOpacity,
  ScrollView,
} from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { useForm } from "react-hook-form";
import { useViewAvailableTechnician } from "@/services/api/availabletechnician";
import LeafletWebViewMap from "../Expressproblem/WebMap";
import { ActivityIndicator, MD2Colors } from "react-native-paper";
import { useRouter } from "expo-router";
import AntDesign from "@expo/vector-icons/AntDesign";
import { SelectList } from "react-native-dropdown-select-list";
import DateTimePicker from "@react-native-community/datetimepicker";

type FormValues = {
  Date: string;
  TimeFrameEnum: string;
  Latitude: number;
  Longitude: number;
};

const ShowAvailableTechnician = () => {
  const { register, handleSubmit, setValue, watch } = useForm<FormValues>();
  const [formdata, setFormdata] = useState<any>(null);
  const [latitude, setLatitude] = useState(27.69828);
  const [longitude, setLongitude] = useState(83.46188);
  const router = useRouter();
  const [selected, setSelected] = React.useState<string>("");
  const [showDatePicker, setShowDatePicker] = useState(false);
  const [selectedDate, setSelectedDate] = useState(new Date());

  const {
    data: availableTechnician,
    isLoading,
    error,
  } = useViewAvailableTechnician(formdata);

  const data = [
    { key: "1", value: "1" },
    { key: "2", value: "2" },
    { key: "3", value: "3" },
    { key: "4", value: "4" },
  ];

  React.useEffect(() => {
    register("Date");
    register("TimeFrameEnum");
    register("Latitude", {
      required: "Latitude is required",
      valueAsNumber: true,
    });
    register("Longitude", {
      required: "Longitude is required",
      valueAsNumber: true,
    });
  }, [register]);

  const onSubmit = (values: FormValues) => {
    setFormdata({
      ...values,
      TimeFrameEnum: parseInt(values.TimeFrameEnum),
      Latitude: latitude,
      Longitude: longitude,
    });
     router.push({
    pathname: "/Forconsumer/available-technician", 
  params: {
    ...values,
    TimeFrameEnum: parseInt(values.TimeFrameEnum),
    Latitude: latitude,
    Longitude: longitude,
  },            
  });
  };

  const onDateChange = (event: any, date: any) => {
    if (date) {
      setSelectedDate(date);
      // Combine with current time
      const updatedDateTime = new Date(
        date.getFullYear(),
        date.getMonth(),
        date.getDate()
      );
      const formattedDate = updatedDateTime.toISOString().split("T")[0]; // => '2025-07-03'
setValue("Date", formattedDate);
    }
    setShowDatePicker(false);
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
            Available Technician
          </Text>
        </TouchableOpacity>
      </View>
      <ScrollView className="mt-28 px-2">
        <View>
          <View className="my-2">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Select TimeFrame
            </Text>
          </View>
          <SelectList
            setSelected={(key: any) => {
              const selectedCategory = data.find(
                (item) => item.key === key
              )?.value;
              if (selectedCategory) {
                setSelected(selectedCategory);
                setValue("TimeFrameEnum", selectedCategory, {
                  shouldValidate: true,
                });
              }
            }}
            data={data}
            boxStyles={{ borderRadius: 8, borderColor: "#ccc" }}
            defaultOption={{ key: "1", value: "1" }}
          />
        </View>

        <View className="mt-5 mx-4">
          <Text
            className="text-base font-rubik text-primary-100"
            style={{ fontFamily: "rubik-bold" }}
          >
            Available Date and Time
          </Text>
        </View>

        <View className="flex flex-col gap-4">
          <Button
            title="Pick Date"
            color={"#7A4DFF"}
            onPress={() => setShowDatePicker(true)}
          />
        </View>
        <View className="flex flex-row gap-3 mt-4">
          <View className="bg-gray-50  rounded-full flex justify-center flex-col items-center h-auto  px-4 py-1 shadow-black-100 shadow-sm ">
            <Text
              className="text-xs font-rubik-bold text-black-300 mt-1"
              style={{ fontFamily: "rubik-bold" }}
            >
              Selected Date: {`${selectedDate.toLocaleDateString()}`}
            </Text>
          </View>

          {showDatePicker && (
            <DateTimePicker
              value={selectedDate}
              mode="date"
              display="default"
              onChange={onDateChange}
            />
          )}
        </View>

        <View className="mt-8">
          <View className="mb-2 flex flex-row gap-">
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
              setValue("Latitude", lat, { shouldValidate: true });
              setValue("Longitude", lng, { shouldValidate: true });
            }}
          />
        </View>
        {isLoading ? (
          <ActivityIndicator
            animating={true}
            color={MD2Colors.red800}
            style={{ marginTop: 8 }}
          />
        ) : (
          <Button
            title="Submit"
            onPress={handleSubmit(onSubmit)}
            color="#7A4DFF"
          />
        )} 

      
      </ScrollView>
    </SafeAreaView>
  );
};

export default ShowAvailableTechnician;
