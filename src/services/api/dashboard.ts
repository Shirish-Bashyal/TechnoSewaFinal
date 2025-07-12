import AsyncStorage from "@react-native-async-storage/async-storage";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

import axiosInstance from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";

const { GetDashboard } = API_ENDPOINTS;

export interface viewAllResponse {
  success: boolean;
  message: string;
  data?: {
    totalUsers: number;
    totalConsumers: number;
    totalTechnicians: number;
    totalActiveUsers: number;
    totalActiveTechnicians:number;
    totalActiveConsumers: number;
    totalBookings: number;
    totalPendingBookings: number;
    totalActiveBookings: number;
    totalCompletedBookings: number;
    totalRevenue: number;
  };
}

export const showAllDashboardData = async (): Promise<viewAllResponse> => {
  try {
    const response = await axiosInstance.get(GetDashboard);
    const Content = await response.data;
    return Content;
  } catch (error) {
    throw new Error("Failed to fetch  data");
  }
};

export const useShowAllDashboardData = () => {
  return useQuery<viewAllResponse, Error>({
    queryKey: ["ViewDataAll"],
    queryFn: showAllDashboardData,
  });
};
