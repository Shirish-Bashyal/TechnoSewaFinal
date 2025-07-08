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
import { Rating, AirbnbRating } from "react-native-ratings";
import { reviewData, useCreateReviews } from "@/services/api/review";

type FormValues = {
  bookingId: number;
  comment: string;
  rating: number;
  byConsumer: Boolean;
};

const Reviews = () => {
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

  const { mutate, isPending } = useCreateReviews();

  // useEffect(() => {
  //   register("serviceDate", { required: "serviceDate is required" });
  // }, [register]);

  useEffect(() => {
    if (id) {
      setValue("bookingId", Number(id));
    }
    setValue("byConsumer", true);
  }, [id, setValue]);

  const handleRating = (ratingValue: number) => {
    setValue("rating", ratingValue);
  };
  const submitBidData = async (data: reviewData) => {
    console.log("ok");
    mutate(data);
  };

  return (
    <SafeAreaView className="h-full bg-gray-100">
      <View className="flex flex-row  justify-center items-center ">
        <TouchableOpacity
          onPress={router.back}
          className="flex flex-row mx-1 py-10"
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
          Add a Reviews!!
        </Text>
        {/* <Toaster position="bottom-center" reverseOrder={false} /> */}
      </View>
      {/* <View className="mt-1 text-sm flex justify-center items-center">
        <Text className="text-black-200" style={{ fontFamily: "rubik" }}>
          Enter your Details
        </Text>
      </View> */}
      <ScrollView contentContainerStyle={{ padding: 14 }}>
        <View style={{ gap: 12 }}>
          <View className="">
            <View className="mb-2 flex flex-row gap-">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                Comments
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>

            <TextInput
              value={text}
              onChangeText={(val) => {
                setText(val);
                setValue("comment", val, { shouldValidate: true });
              }}
              placeholder="Write your message..."
              multiline
              numberOfLines={4}
              style={styles.textArea}
              placeholderTextColor="#999"
            />
          </View>
          <View>
            <View className=" flex flex-row gap-0.5">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                Ratings
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>
            <View style={{  }}>
              <AirbnbRating
                defaultRating={5}
                reviews={["Terrible", "Bad", "OK", "Good", "Amazing"]}
                onFinishRating={handleRating}
                size={20}
              />
              <Rating
                showRating
                onFinishRating={handleRating}
                style={{ paddingVertical: 5 }}
                imageSize={25}
              />
            </View>
          </View>
          {/* <View>
            <View className="mb-2 flex flex-row gap-">
              <Text
                className="text-base text-black-300 "
                style={{ fontFamily: "outfit-light" }}
              >
                Service Date
              </Text>
              <Text className="text-red-600 text-base ">*</Text>
            </View>
           
          </View> */}

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

export default Reviews;

const inputStyle = {
  borderWidth: 1,
  borderColor: "#ccc",
  padding: 10,
  borderRadius: 8,
};

const styles = StyleSheet.create({
  textArea: {
    height: 80,
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
