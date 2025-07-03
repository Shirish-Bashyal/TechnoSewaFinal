import { useMutation } from "@tanstack/react-query";
import Toast from "react-native-toast-message";
import axiosInstance from "../axiosInstance";
// import { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";
import { useQuery, useQueryClient } from "@tanstack/react-query";

const { GetPost,GetPostById } = API_ENDPOINTS;

export interface postData {
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

export interface viewPostForTechnicianResponse {
  success: boolean;
  message: string;
  data?: postData[];
}

export const viewPostForTechnician =
  async (): Promise<viewPostForTechnicianResponse> => {
    try {
      const response = await axiosInstance.get(GetPost);
      const postContent = await response.data;
      return postContent;
    } catch (error) {
      throw new Error("Failed to fetch  data");
    }
  };

export const useViewPostForTechnician = () => {
  return useQuery<viewPostForTechnicianResponse, Error>({
    queryKey: ["ViewDataTechnician"],
    queryFn: viewPostForTechnician,
  });
};

//PostById
export interface viewPostByIdResponse {
  success: boolean;
  data?: {
      id: number;
      title: string;
      description: string;
      category: string;
      imageUrl: Array<string>;
      lattitude: number;
      longitude: number;
      creationDate: string;
      userName: string;
    };
}

export const viewPostById = async (
  postId: string
): Promise<viewPostByIdResponse> => {
  try {
    const response = await axiosInstance.get(`${GetPostById}?postId=${postId}`);
    return response.data;
  } catch (error: any) {
    console.error("Error response from server:", error.response);
    throw new Error("Failed view Employee");
  }
};

export const useViewpostIdById = (postId: string) => {
  return useQuery<viewPostByIdResponse, Error>({
    queryKey: ["postViewData", postId],
    queryFn: () => viewPostById(postId),
  });
};
