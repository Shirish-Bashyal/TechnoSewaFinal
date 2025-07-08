import { useMutation } from "@tanstack/react-query";
import Toast from "react-native-toast-message";
import axiosInstance from "../axiosInstance";
// import { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";
import { useQuery, useQueryClient } from "@tanstack/react-query";

const { CreateReviews } = API_ENDPOINTS;

export interface reviewData {
  bookingId: number;
  comment: string;
  rating: number;
  byConsumer: Boolean;
}
export interface reviewResponse {
  success: Boolean;
  message: string;
}

export const createReview = async (formData: reviewData): Promise<reviewResponse> => {
  try {
    console.log("Sending login payload:", formData);
    const response = await axiosInstance.post(CreateReviews, formData);
    console.log(response.data);
    return response.data;
  } catch (error: any) {
    console.log("Response data:", error.message);
    const backendMessage =
      error?.response?.data?.message || error.message || "Unknown error";
    console.log("Backend error:", backendMessage);
    console.log(error);

    return {
      success: false,
      message: backendMessage || "Create Bidding Failed",
    };
  }
};

export const useCreateReviews = () => {
  const router = useRouter();
  const toast = useToast();

  return useMutation<reviewResponse, Error, reviewData>({
    mutationFn: createReview,
    onSuccess: (data) => {
      if (data.success === true) {
        toast.show("Reviews created successfully", {
          type: "success",
          placement: "top",
          duration: 4000,
          style: { marginTop: 125 },
          animationType: "slide-in",
        });
        console.log(data);
        router.replace("/(root)/(tabs)");
      } else {
        toast.show(data.message || "Failed to create bid", {
          type: "danger",
          placement: "bottom",
        });
      }
    },
    onError: (error) => {
      const errorMessage =
        error instanceof Error
          ? error.message
          : "An unknown error occurred while creating bid";

      toast.show(errorMessage, {
        type: "danger",
        placement: "bottom",
      });
    },
  });
};
