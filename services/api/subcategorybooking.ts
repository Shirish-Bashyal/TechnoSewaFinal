import { useMutation } from "@tanstack/react-query";
import Toast from "react-native-toast-message";
import axiosInstance from "../axiosInstance";
// import { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";
import { useQuery, useQueryClient } from "@tanstack/react-query";

const { BookSubCategory } = API_ENDPOINTS;

export interface createBookingSubCategoryData {
  lattitude: number;
  longitude: number;
  categoryId: number;
  subCategoryId: number;
  serviceDate: string;
  technicianID: number;
  timeFrame: string;
}
export interface createBookingSubCategoryResponse {
  success: Boolean;
  message: string;
  data:string;
}

export const createBookingSubCategory = async (
  formData: createBookingSubCategoryData
): Promise<createBookingSubCategoryResponse> => {

  try {
    console.log("Sending login payload:", formData);
    const response = await axiosInstance.post(BookSubCategory, formData);
    console.log(response);
    return response.data;
  } catch (error: any) {
    console.log("Response data:", error.message);
    const backendMessage =
      error?.response?.data?.message || error.message || "Unknown error";
    console.log("Backend error:", backendMessage);
    console.log(error);

    return {
      success: false,
      data: "error",
      message: backendMessage || "Failed to Book  Technician",
    };
  }
};

export const useCreateBookingSubCategory = () => {
  const router = useRouter();
  const toast = useToast();

  return useMutation<createBookingSubCategoryResponse, Error, createBookingSubCategoryData>({
    mutationFn: createBookingSubCategory,
    onSuccess: (data) => {
      if (data.success === true) {
        toast.show("Technician Booked successfully", {
          type: "success",
          placement: "top",
          duration: 4000,
          style: { marginTop: 125 },
          animationType: "slide-in",
        });
        console.log(data);
        router.replace("/(root)/(tabs)/explore");
      } else {
        toast.show(data.message || "Failed to book technician", {
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
