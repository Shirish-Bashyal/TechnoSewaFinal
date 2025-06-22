import { useMutation } from "@tanstack/react-query";
import Toast from "react-native-toast-message";
import axiosInstance from "../axiosInstance";
// import  { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";

const { Login, Signup } = API_ENDPOINTS;

export interface LoginData {
  phoneNumber: string;
  password: string;
}

export interface LoginResponse {
  success: boolean;
  message: string;
  data: string;
}

export const loginAdmin = async (
  loginData: LoginData
): Promise<LoginResponse> => {
   
  try {
    console.log("Sending login payload:", loginData);
    //   const response = await fetchWithAuth(Login ,{
    //   method: 'POST',
    //   headers: { 'content-type': 'application/json' },
    //   body: JSON.stringify(loginData),
    // });

    const response = await axiosInstance.post(Login, loginData);
    console.log(response);
    return response.data;
  } catch (error: any) {
    
    console.log("Response data:", error.message);
    const backendMessage = error.message || "Unknown error";
    console.log("Backend error:", backendMessage);
    console.log(error);

    return {  
      success: false,
      data:"error",
      message: "An error occurred while logging in",
    };
  }
};
export const useLogin = () => {
   const router = useRouter();
   const toast = useToast();
  return useMutation<LoginResponse, Error, LoginData>({
    mutationFn: loginAdmin,
    onSuccess: async (data) => {
      if (data.success) {
         await AsyncStorage.setItem("token", data.data);
        console.log("Token set in cookie:", AsyncStorage.getItem("token"));
        toast.show("User logged in Successfully", {
          type: "success",
          placement: "bottom",
          duration: 4000,
          animationType: "slide-in",
        });
        router.push("/(root)/(tabs)");

      } else {
       toast.show(data.message, {
          type: "danger",
          placement: "bottom",
          duration: 4000,
          animationType: "slide-in",
        });

        console.log(data.message || "Login failed");
      }
    },
    onError: (error: any) => {
      console.error("Login error:", error);
    },
  });
};

//Register
export interface SignUpResponse {
  success: boolean;
  message: string;
}

export interface SignUpData {
  fullName: string;
  email: string;
  password: string;
  confirmPassword: string;
  phoneNumber: string;
  city: string;
  wardNo: number;
  toleName: string;
}

export const SignUpAdmin = async (
  formData: SignUpData
): Promise<SignUpResponse> => {
  try {
    console.log("Sending login payload:", formData);
    //   const response = await fetchWithAuth(Signup,{
    //   method: 'POST',
    //   headers: { 'content-type': 'application/json' },
    //   body: JSON.stringify(formData),
    // });
    const response = await axiosInstance.post(Signup, formData);
    console.log(response.data);
    console.log("ok");

    return response.data;
  } catch (error: any) {
    console.error("An error occurred during signup:", error);

    let errorMessage = "An unknown error occurred while registering user";

    if (error instanceof Error) {
      errorMessage = error.message;
    } else if (error && typeof error === "object" && error.message) {
      errorMessage = error.message;
    }
    return {
      success: false,
      message: "An error occurred while registering admin",
    };
  }
};

export const useSignUp = () => {
  const router = useRouter();
  const toast = useToast();
  return useMutation<SignUpResponse, Error, SignUpData>({
    mutationFn: SignUpAdmin,
    onSuccess: (data) => {
      // if (data.message === "User already exists") {
      //   //  Toast.show({
      //   //   type:'success',
      //   //   text1: "User logged in successfully",
      //   // })
      //   // console.log(data.message);
      //   toast.error("User already exists");
      // } else
      // if (data.success) {

      if (data.success) {
        toast.show("User Registered Successfully", {
          type: "success",
          placement: "bottom",
          duration: 4000,
          animationType: "slide-in",
        });

        console.log("Successfully Registered");
        router.replace("/auth/tech");  
      } else {
        toast.show(data.message || "Registration failed", {
          type: "danger",
          placement: "bottom",
        });
      }
    },
    onError: (error) => {
      console.error("An error occurred during signup:", error);

      let errorMessage = "An unknown error occurred while registering user";

      if (error instanceof Error) {
        errorMessage = error.message;
      }
      console.log("An error occurred while registering user", error);
    },
  });
};
