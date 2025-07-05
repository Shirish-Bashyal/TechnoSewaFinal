import { useMutation } from "@tanstack/react-query";
import Toast from "react-native-toast-message";
import axiosInstance from "../axiosInstance";
// import { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";
import { useQuery, useQueryClient } from "@tanstack/react-query";

const { GetAllPostedProblem } = API_ENDPOINTS;

export interface postProblemData {
  id: number;
  title: string;
  description: string;
  category: string;
  imageUrl: Array<string>;
  lattitude: number;
  longitude: number;
  creationDate: string;
  userName: string;
}

export interface viewPostForConsumerResponse {
  success: boolean;
  message: string;
  data?: postProblemData[];
}

export const postProblemForConsumer =
  async (): Promise<viewPostForConsumerResponse> => {
    try {
      const response = await axiosInstance.get(GetAllPostedProblem);
      const postContent = await response.data;
      return postContent;
    } catch (error) {
      throw new Error("Failed to fetch  data");
    }
  };

export const usePostProblemForConsumer = () => {
  return useQuery<viewPostForConsumerResponse, Error>({
    queryKey: ["ViewDataTechnician"],
    queryFn: postProblemForConsumer,
  });
};