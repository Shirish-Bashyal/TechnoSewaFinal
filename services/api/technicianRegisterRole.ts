import { useMutation } from "@tanstack/react-query";
import Toast from "react-native-toast-message";
import axiosInstance from "../axiosInstance";
// import { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";
import { useQuery, useQueryClient } from "@tanstack/react-query";

const { CreateTechnician } = API_ENDPOINTS;

export interface createTechnicianRoleData {
  secondPhoneNumber: string;
  lattitude: number;
  longitude: number;
}
export interface createTechnicianRoleResponse {
  success: Boolean;
  message: string;
  data:string;
}

export const createTechnicianRole = async (
  formData: createTechnicianRoleData
): Promise<createTechnicianRoleResponse> => {

  try {
    console.log("Sending login payload:", formData);
    const response = await axiosInstance.post(CreateTechnician, formData);
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
      message: backendMessage || "Role doesnot changed",
    };
  }
};

export const useCreateTechnicianRole = () => {
  const router = useRouter();
  const toast = useToast();

  return useMutation<createTechnicianRoleResponse, Error, createTechnicianRoleData>({
    mutationFn: createTechnicianRole,
    onSuccess: (data) => {
      if (data.success === true) {
        toast.show("Role changed successfully", {
          type: "success",
          placement: "bottom",
          duration: 4000,
          animationType: "slide-in",
        });
         AsyncStorage.removeItem("token");
        router.push("/auth/register")
        
      } else {
        toast.show(data.message || "Failed to change role", {
          type: "danger",
          placement: "bottom",
        });
      }
    },
    onError: (error) => {
      const errorMessage =
        error instanceof Error
          ? error.message
          : "An unknown error occurred while changing Role";

      toast.show(errorMessage, {
        type: "danger",
        placement: "bottom",
      });
    },
  });
};
