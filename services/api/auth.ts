import { useMutation} from "@tanstack/react-query";
import Toast from 'react-native-toast-message';
import axiosInstance from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from '@react-native-async-storage/async-storage';

const { Login} = API_ENDPOINTS;


export interface LoginData {
  phoneNumber: string;
  password: string;
}

export interface LoginResponse {
  success: boolean;
  message: string;
  data?: string;
}

export const loginAdmin = async (
  loginData: LoginData
): Promise<LoginResponse> => {
  try {
  //   const response = await fetchWithAuth(Login ,{
  //   method: 'POST',
  //   headers: { 'content-type': 'application/json' },
  //   body: JSON.stringify(loginData),
  // });
    const response = await axiosInstance.post(Login, loginData);
    console.log(response);
    return response.data;
  } catch (error:any) {
    const backendMessage = error.response?.message || "Unknown error";
    console.log("Backend error:", backendMessage);
    console.log(error)
   
    return {
      success: false,
      message: "An error occurred while logging in",
    };
  }
};
export const useLogin = () => {
  return useMutation<LoginResponse, Error, LoginData>({
    mutationFn: loginAdmin,
    onSuccess: async(data) => {
      if (data.success && data.data) {
       await AsyncStorage.setItem("token", data.data);
        console.log("Token set in cookie:", AsyncStorage.getItem("token"));
        Toast.show({
          type:'success',
          text1: "User logged in successfully",
        })
      } else {
        Toast.show({
          type:'error',
          text1: data.message,
        })
        console.log(data.message || "Login failed");
      }
    },
    onError: (error: any) => {
      console.error("Login error:", error);
    },
  });
};