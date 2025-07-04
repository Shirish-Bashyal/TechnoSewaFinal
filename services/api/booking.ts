import { useMutation } from "@tanstack/react-query";
import Toast from "react-native-toast-message";
import axiosInstance from "../axiosInstance";
// import { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";
import { useQuery, useQueryClient } from "@tanstack/react-query";

const { GetAllBookings } = API_ENDPOINTS;

export interface viewPendingBookings {
  postBids?: {
    solutionDescription: string;
    estimationPrice: number;
    serviceDate: string;
    postTitle: string;
    bidId: number;
  };
}

export interface viewActiveBookings {
  bookingId: number;
  title: string;
  price: number;
  serviceDate: string;
  // "timeFrame": null,
  consumerName: string;
  consumerPhoneNumber: string;
  lattitude: number;
  longitude: number;
}

export interface viewAllBookingResponse {
  success: boolean;
  message: string;
  data?: {
    pendingBookings: viewPendingBookings[];
    activeBookings: viewActiveBookings[];
    completedBookings:[]
  };
}

export const showAllBooking = async (): Promise<viewAllBookingResponse> => {
  try {
    const response = await axiosInstance.get(GetAllBookings);
    const bidContent = await response.data;
    return bidContent;
  } catch (error) {
    throw new Error("Failed to fetch  data");
  }
};

export const useShowAllBooking = () => {
  return useQuery<viewAllBookingResponse, Error>({
    queryKey: ["ViewDataAllBooking"],
    queryFn: showAllBooking,
  });
};
