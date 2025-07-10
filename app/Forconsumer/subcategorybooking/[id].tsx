import {
  View,
  TextInput,
  ScrollView,
  Button,
  Text,
  TouchableOpacity,
  StyleSheet,
} from "react-native";
import React, { useEffect, useRef, useState } from "react";
import { useForm } from "react-hook-form";
import { SafeAreaView } from "react-native-safe-area-context";
import { useLocalSearchParams, useRouter } from "expo-router";
import FontAwesome from "@expo/vector-icons/FontAwesome";
import DateTimePicker from "@react-native-community/datetimepicker";
import AntDesign from "@expo/vector-icons/AntDesign";
import { ActivityIndicator, MD2Colors } from "react-native-paper";
import {
  createBookingSubCategoryData,
  useCreateBookingSubCategory,
} from "@/services/api/subcategorybooking";
import { SelectList } from "react-native-dropdown-select-list";
import LeafletWebViewMap from "../../Expressproblem/WebMap";

type FormValues = {
  lattitude: number;
  longitude: number;
  categoryId: number;
  subCategoryId: number;
  serviceDate: string;
  technicianID: number;
  timeFrame: string;
};

const subCategoryBooking = () => {
  const {
    register,
    handleSubmit,
    setValue,
    formState: { errors },
  } = useForm<FormValues>();
  const router = useRouter();
  const [text, setText] = React.useState("");
  const [showDatePicker, setShowDatePicker] = useState(false);
  const [selectedDate, setSelectedDate] = useState(new Date());
  const [latitude, setLatitude] = useState(27.69828);
  const [longitude, setLongitude] = useState(83.46188);
  const [selected, setSelected] = React.useState<string>("");

  const { id } = useLocalSearchParams();

  const { mutate, isPending } = useCreateBookingSubCategory();

  const onDateChange = (event: any, date: any) => {
    if (date) {
      setSelectedDate(date);
      // Combine with current time
      const updatedDateTime = new Date(
        date.getFullYear(),
        date.getMonth(),
        date.getDate()
      );
      const formattedDate = updatedDateTime.toISOString().split("T")[0];
      setValue("serviceDate", formattedDate);
    }
    setShowDatePicker(false);
  };
  useEffect(() => {
    register("serviceDate", { required: "serviceDate is required" });
    register("categoryId", { required: "categoryId is required" });
    register("lattitude", {
      required: "Lattitude is required",
      valueAsNumber: true,
    });
    register("longitude", {
      required: "Longitude is required",
      valueAsNumber: true,
    });
  }, [register]);

  const timeframe = [
    { key: "1", value: "1" },
    { key: "2", value: "2" },
    { key: "3", value: "3" },
    { key: "4", value: "4" },
  ];

  const category = [
    { key: "1", value: "Plumbing" },
    { key: "2", value: "Electrical" },
  ];

  const subCategory=[
    { key: "2", value: "Washing Machine Installation" },
    { key: "3", value: "Toilet Unclogging" },
  ]

  useEffect(() => {
    if (id) {
      setValue("technicianID", Number(id));
    }
  }, [id, setValue]);

  const submitBidData = async (data: createBookingSubCategoryData) => {
    console.log("ok");
    mutate(data);
  };

  return (
    <SafeAreaView className="h-full bg-gray-100">
      <View className="flex flex-row  justify-center items-center mt-8">
        <TouchableOpacity
          onPress={router.back}
          className="flex flex-row  gap-4 mx-4 py-5"
        >
          <View>
            <Text>
              <AntDesign name="back" size={24} color="black" />
            </Text>
          </View>
        </TouchableOpacity>
        <Text
          className="text-lg text-black-300"
          style={{ fontFamily: "rubik-bold" }}
        >
          Create A Booking!!
        </Text>
        {/* <Toaster position="bottom-center" reverseOrder={false} /> */}
      </View>
    
      <ScrollView contentContainerStyle={{ padding: 16 }}>
        <View style={{ gap: 12 }}>
          {/* {errors && (
            <Text style={{ color: "red" }}>This field is required</Text>
          )} */}

          <View className="my-1">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Select TimeFrame
            </Text>
          </View>
          <SelectList
            setSelected={(key: any) => {
              const selectedCategory = timeframe.find(
                (item) => item.key === key
              )?.value;
              if (selectedCategory) {
                setSelected(selectedCategory);
                setValue("timeFrame", selectedCategory, {
                  shouldValidate: true,
                });
              }
            }}
            data={timeframe}
            boxStyles={{ borderRadius: 8, borderColor: "#ccc" }}
            defaultOption={{ key: "1", value: "1" }}
          />
        </View>
        <View>
          <View className="my-2">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Select Category
            </Text>
          </View>
          <SelectList
            setSelected={(key: any) => {
              const selectedCategory = category.find(
                (item) => item.key === key
              )?.value;
              if (selectedCategory) {
                setSelected(selectedCategory); 
                setValue("categoryId", key, {
                  // sends "1" or "2" to form state
                  shouldValidate: true,
                });
              }
            }}
            data={category}
            boxStyles={{ borderRadius: 8, borderColor: "#ccc" }}
            defaultOption={{ key: "1", value: "Plumbing" }}
          />

          <View className="my-2">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Select SubCategory
            </Text>
          </View>
          <SelectList
            setSelected={(key: any) => {
              const selectedCategory = subCategory.find(
                (item) => item.key === key
              )?.value;
              if (selectedCategory) {
                setSelected(selectedCategory); 
                setValue("subCategoryId", key, {
                  // sends "1" or "2" to form state
                  shouldValidate: true,
                });
              }
            }}
            data={subCategory}
            boxStyles={{ borderRadius: 8, borderColor: "#ccc" }}
            defaultOption={{ key: "1", value: "Washing Machine Installation" }}
          />

          <View>
            <View className="mb-2 flex flex-row gap-">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                Service Date
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>
            <Button
              title="Pick Date"
              color={"#7A4DFF"}
              onPress={() => setShowDatePicker(true)}
            />
          </View>
          <View className="bg-gray-50  rounded-full flex justify-center flex-col items-center h-auto  px-4 py-3 shadow-black-100 shadow-sm ">
            <Text
              className="text-xs font-rubik-bold text-black-300 mt-2"
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

          <View></View>
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
          <Button
            title="Submit"
            onPress={handleSubmit(submitBidData)}
            color="#7A4DFF"
          />
        )}
      </ScrollView>
    </SafeAreaView>
  );
};

export default subCategoryBooking;

const styles = StyleSheet.create({
  textArea: {
    height: 120,
    textAlignVertical: "top",
    borderColor: "#ccc",
    borderWidth: 1,
    borderRadius: 8,
    padding: 12,
  },
  container: {
    flex: 1,
    alignItems: "center",
    justifyContent: "center",
  },
  image: {
    width: 200,
    height: 200,
  },
});

const inputStyle = {
  borderWidth: 1,
  borderColor: "#ccc",
  padding: 10,
  borderRadius: 8,
};
