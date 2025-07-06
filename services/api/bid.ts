import { useMutation } from "@tanstack/react-query";
import Toast from "react-native-toast-message";
import axiosInstance from "../axiosInstance";
// import { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";
import { useQuery, useQueryClient } from "@tanstack/react-query";

const { CreateBid, ViewBid, GetBidById } = API_ENDPOINTS;

export interface bidData {
  postId: number;
  solutionDescription: string;
  estimationPrice: number;
  serviceDate: string;
}
export interface bidResponse {
  success: Boolean;
  message: string;
}

export const createBid = async (formData: bidData): Promise<bidResponse> => {
  try {
    console.log("Sending login payload:", formData);
    const response = await axiosInstance.post(CreateBid, formData);
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
      message: backendMessage || "Create Bidding Failed",
    };
  }
};

export const useCreateBid = () => {
  const router = useRouter();
  const toast = useToast();

  return useMutation<bidResponse, Error, bidData>({
    mutationFn: createBid,
    onSuccess: (data) => {
      if (data.success === true) {
        toast.show("Bid created successfully", {
          type: "success",
          placement: "top",
          duration: 4000,
          style: { marginTop: 125 },
          animationType: "slide-in",
        });
        console.log(data);
        router.replace("/Technician/(root)/(tabs)/viewbid");
      } else {
        toast.show(data.message || "Failed to create bid", {
          type: "danger",
          placement: "bottom",
        });
      }
    },
    onError: (error) => {
      const errorMessage =
        error instanceof Error
          ? error.message
          : "An unknown error occurred while creating bid";

      toast.show(errorMessage, {
        type: "danger",
        placement: "bottom",
      });
    },
  });
};

//View bid
export interface viewBidData {
  postId: number;
  solutionDescription: string;
  estimationPrice: number;
  serviceDate: string;
  postTitle: string;
  bidId: number;
}

export interface viewBidDataResponse {
  success: boolean;
  message: string;
  data?: viewBidData[];
}

export const showBidData = async (): Promise<viewBidDataResponse> => {
  try {
    const response = await axiosInstance.get(ViewBid);
    const bidContent = await response.data;
    return bidContent;
  } catch (error) {
    throw new Error("Failed to fetch  data");
  }
};

export const useShowBidData = () => {
  return useQuery<viewBidDataResponse, Error>({
    queryKey: ["ViewDataBidding"],
    queryFn: showBidData,
  });
};

//ViewBidById

export interface viewBid {
  solutionDescription: string;
  estimationPrice: number;
  serviceDate: string;
  technicianName: string;
  bidId: number;
  technicianId: number;
}
export interface viewBidByIdResponse {
  success: boolean;
  data?: viewBid[];
}

export const viewBidById = async (
  postId: string
): Promise<viewBidByIdResponse> => {
  try {
    const response = await axiosInstance.get(`${GetBidById}?postId=${postId}`);
    return response.data;
  } catch (error: any) {
    console.error("Error response from server:", error.response);
    throw new Error("Failed view Employee");
  }
};

export const useViewBidIdById = (postId: string) => {
  return useQuery<viewBidByIdResponse, Error>({
    queryKey: ["postViewData", postId],
    queryFn: () => viewBidById(postId),
  });
};
