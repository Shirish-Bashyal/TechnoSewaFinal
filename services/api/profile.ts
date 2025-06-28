import { useMutation } from "@tanstack/react-query";
import Toast from "react-native-toast-message";
import axiosInstance from "../axiosInstance";
// import { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";
import { useQuery, useQueryClient } from "@tanstack/react-query";


const { ConsumerProfile } = API_ENDPOINTS;

export interface viewConsumerProfileResponse {
  success: boolean;
  message: string;
  data?: {
    name: string;
    phoneNumber: string;
    email: string;
    role: string;
    city: string;
    wardNo: Number;
    toleName: string;
  };
}

export const viewProfile = async (): Promise<viewConsumerProfileResponse> => {
  try {
    const response = await axiosInstance.get(ConsumerProfile);
    const profileContent = await response.data;
    return profileContent;
  } catch (error) {
    throw new Error("Failed to fetch  data");
  }
};

export const useViewProfile = () => {
  return useQuery<viewConsumerProfileResponse, Error>({
    queryKey: ["ViewData"],
    queryFn: viewProfile,
  });
};
