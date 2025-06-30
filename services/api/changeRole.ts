import { useMutation } from "@tanstack/react-query";
import Toast from "react-native-toast-message";
import axiosInstance from "../axiosInstance";
// import { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";
import { useQuery, useQueryClient } from "@tanstack/react-query";

export interface changeRoleData {
  role: string;
}
export interface changeRoleResponse {
  success: Boolean;
  message: string;
}

export const changeRole = async (
  formData: changeRoleData
): Promise<changeRoleResponse> => {
  const encodedRole = encodeURIComponent(formData.role);
  const response = await axiosInstance.post(
    `/api/Role/ChangeRole?role=${encodedRole}`
  );
  return response.data;
};

export const useChangeRole = () => {
  const router = useRouter();
  const toast = useToast();

  return useMutation<changeRoleResponse, Error, changeRoleData>({
    mutationFn: changeRole,
    onSuccess: (data, variables) => {
      const { role } = variables;
      if (data.success === true) {
        toast.show("Role changed successfully", {
          type: "success",
          placement: "bottom",
          duration: 4000,
          animationType: "slide-in",
        });
        if (role === "Technician") {
          router.replace("/Technician/(root)/(tabs)");
        } else {
          router.push("/(root)/(tabs)");
        }
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
          : "An unknown error occurred while sending OTP";

      toast.show(errorMessage, {
        type: "danger",
        placement: "bottom",
      });
    },
  });
};
