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
import { bidData, useCreateBid } from "@/services/api/bid";
import AntDesign from "@expo/vector-icons/AntDesign";
import { ActivityIndicator, MD2Colors } from "react-native-paper";

type FormValues = {
  postId: number;
  solutionDescription: string;
  estimationPrice: number;
  serviceDate: string;
};

const Bid = () => {
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
  const { id } = useLocalSearchParams();

  const { mutate, isPending } = useCreateBid();

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
  }, [register]);

  useEffect(() => {
    if (id) {
      setValue("postId", Number(id));
    }
  }, [id, setValue]);

  const submitBidData = async (data: bidData) => {
    console.log("ok");
    mutate(data);
  };

  return (
    <SafeAreaView className="h-full bg-gray-100">
      <View className="flex flex-row  justify-center items-center mt-8">
        <TouchableOpacity
          onPress={router.back}
          className="flex flex-row  gap-4 mx-4 py-10"
        >
          <Text>
            <AntDesign name="back" size={24} color="black" />
          </Text>
        </TouchableOpacity>
        <Text
          className="text-lg text-black-300"
          style={{ fontFamily: "rubik-bold" }}
        >
          Create A Bid!!
        </Text>
        {/* <Toaster position="bottom-center" reverseOrder={false} /> */}
      </View>
      {/* <View className="mt-1 text-sm flex justify-center items-center">
        <Text className="text-black-200" style={{ fontFamily: "rubik" }}>
          Enter your Details
        </Text>
      </View> */}
      <ScrollView contentContainerStyle={{ padding: 16 }}>
        <View style={{ gap: 12 }}>
          <View>
            <View className="mb-2 flex flex-row gap-">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                Post Id
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>
            <TextInput
              placeholder="Post ID"
              style={inputStyle}
              placeholderTextColor="#999"
              value={id?.toString()}
              editable={false}
            />
          </View>
          {/* {errors.itemName && (
          <Text style={{ color: 'red' }}>{errors.itemName.message}</Text>
        )} */}

          <View className="mt-4">
            <View className="mb-2 flex flex-row gap-">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                solution Description
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>

            <TextInput
              value={text}
              onChangeText={(val) => {
                setText(val);
                setValue("solutionDescription", val, { shouldValidate: true });
              }}
              placeholder="Write your message..."
              multiline
              numberOfLines={4}
              style={styles.textArea}
              placeholderTextColor="#999"
            />
          </View>
          <View>
            <View className="mb-2 flex flex-row gap-">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                Estimation Price
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>
            <TextInput
              placeholder="200"
              style={inputStyle}
              keyboardType="phone-pad"
              maxLength={5}
              placeholderTextColor="#999"
              onChangeText={(text) => setValue("estimationPrice", Number(text))}
              {...register("estimationPrice", { required: "ward is required" })}
            />
          </View>
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

          <View></View>
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

export default Bid;

const inputStyle = {
  borderWidth: 1,
  borderColor: "#ccc",
  padding: 10,
  borderRadius: 8,
};

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
