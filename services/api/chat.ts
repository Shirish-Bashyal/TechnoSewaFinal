import { useMutation } from "@tanstack/react-query";
import Toast from "react-native-toast-message";
import axiosInstance from "../axiosInstance";
// import { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";
import { useQuery, useQueryClient } from "@tanstack/react-query";

const { PostQuestion } = API_ENDPOINTS;

export interface postQuestionData {
  question: string;
}

export const postQuestion = async (formData: postQuestionData) => {
  try {
    console.log("Sending login payload:", formData);
    const response = await axiosInstance.post(PostQuestion, formData);
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
      message: backendMessage || "Failed to post question",
    };
  }
};
