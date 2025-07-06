import { useMutation } from "@tanstack/react-query";
import axiosInstance from "../axiosInstance";
// import { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";
import { useQuery, useQueryClient } from "@tanstack/react-query";

const { Notification } = API_ENDPOINTS;

export interface viewNotificationData {
  title: string;
  message: string;
  receivedDate: string;
}

export interface viewNotificationDataResponse {
  success: boolean;
  message: string;
  data?: viewNotificationData[];
}

export const showNotificationData =
  async (): Promise<viewNotificationDataResponse> => {
    try {
      const response = await axiosInstance.get(Notification);
      const notiContent = await response.data;
      return notiContent;
    } catch (error) {
      throw new Error("Failed to fetch  data");
    }
  };

export const useShowNotificationData = () => {
  return useQuery<viewNotificationDataResponse, Error>({
    queryKey: ["ViewDataNotif"],
    queryFn: showNotificationData,
  });
};
