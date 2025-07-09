import { useMutation } from "@tanstack/react-query";
import Toast from "react-native-toast-message";
import axiosInstance from "../axiosInstance";
// import { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";
import { useQuery, useQueryClient } from "@tanstack/react-query";

const { AvailableTechnician } = API_ENDPOINTS;

export interface query {
  Date: string;
  TimeFrameEnum: number;
  Latitude: number;
  Longitude: number;
}

export interface viewAvaliableData {
  technicianId: number;
  name: string;
  distance: number;
  reviews?: {
    averageRating: number;
    reviews: Array<string>;
  };
}
export interface viewAvailableByIdResponse {
  success: boolean;
  data?: viewAvaliableData[];
}

export const viewAvailableTechnician = async (
  formdata: query
): Promise<viewAvailableByIdResponse> => {
  try {
    const response = await axiosInstance.get(
      `${AvailableTechnician}?Date=${formdata.Date}&TimeFrameEnum=${formdata.TimeFrameEnum}&Latitude=${formdata.Latitude}&Longitude=${formdata.Longitude}`
    );
    return response.data;
  } catch (error: any) {
    console.error("Error response from server:", error.response);
    throw new Error("Failed view Employee");
  }
};

export const useViewAvailableTechnician = ( formdata: query) => {
  return useQuery<viewAvailableByIdResponse, Error>({
    queryKey: ["postViewData", formdata],
    queryFn: () => viewAvailableTechnician(formdata),
  });
};
