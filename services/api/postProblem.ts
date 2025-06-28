import { useMutation } from "@tanstack/react-query";
import Toast from "react-native-toast-message";
import axiosInstance from "../axiosInstance";
// import { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";
import { useQuery, useQueryClient } from "@tanstack/react-query";

const { PostProblem } = API_ENDPOINTS;

type ImageFile = {
  uri: string;
  fileName: string;
  type: string;
  name: string;
};

// export interface postProblemData {
//   Title: string;
//   Category: string;
//   Description: string;
//   // ImagesFiles: Array<string>;
//   Lattitude: number;
//   Longitude: number;
// }

export interface postProblemResponse {
  success: boolean;
  message: string;
}

export const postProblem = async (
  // postProblemData: postProblemData
  data: FormData
): Promise<postProblemResponse> => {
  try {
    console.log("Sending login payload:", data);
    // const response = await axiosInstance.post(PostProblem, postProblemData, {
    //   headers: {
    //     "Content-Type": "multipart/form-data",
    //   },
    // });
    const isFormData = data instanceof FormData;
    console.log("Sending login payload:", isFormData);
    if (data instanceof FormData) {
      data.forEach((value, key) => {
        // You might need to adjust how files are logged here
        // as `value` for a file will be a Blob/File object.
        if (
          typeof value === "object" &&
          value !== null &&
          "uri" in value &&
          "name" in value
        ) {
          console.log(
            `${key}: File { name: ${value.name}, type: ${value.type} }`
          );
        } else {
          console.log(`${key}:`, value);
        }
      });
    }

    const response = await axiosInstance.post(PostProblem, data);
    console.log(response);
    return response.data;
  } catch (error: any) {
    console.error("Error response from server:", error.response);
    throw new Error("Failed to post Problem");
  }
};

export const usePostProblem = () => {
  const toast = useToast();
  //   const queryClient = useQueryClient();
  return useMutation<postProblemResponse, Error, FormData>({
    mutationFn: postProblem,
    onSuccess: (data) => {
      //   queryClient.invalidateQueries({ queryKey: ["aboutViewData"] });
      console.log(data);
      if (data.success === true) {
        toast.show("Problem Posted Successfully", {
          type: "success",
          placement: "bottom",
          duration: 4000,
          animationType: "slide-in",
        });

        console.log("Successfully Posted");
        // router.replace("/");
      } else {
        toast.show(data.message || "Problem Posting failed", {
          type: "danger",
          placement: "bottom",
        });
      }
    },
    onError: (error: any) => {
      console.error("About error:", error);
    },
  });
};
