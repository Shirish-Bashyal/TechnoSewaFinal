import { useMutation } from "@tanstack/react-query";
import Toast from "react-native-toast-message";
import axiosInstance from "../axiosInstance";
// import { fetchWithAuth } from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useToast } from "react-native-toast-notifications";
import { useRouter } from "expo-router";
import { useQuery, useQueryClient } from "@tanstack/react-query";

const {
  GetAllBookings,
  BookTechnician,
  GetAllBookingsForConsumer,
  CompleteBookings,
} = API_ENDPOINTS;

export interface viewPendingBookings {
  postBids?: {
    solutionDescription: string;
    estimationPrice: number;
    serviceDate: string;
    postTitle: string;
    bidId: number;
  };
}

export interface viewActiveBookings {
  bookingId: number;
  title: string;
  price: number;
  serviceDate: string;
  // "timeFrame": null,
  consumerName: string;
  consumerPhoneNumber: string;
  lattitude: number;
  longitude: number;
}

export interface viewCompleteBookings {
  bookingId: number;
  title: string;
  price: number;
  serviceDate: string;
  // "timeFrame": null,
  consumerName: string;
  consumerPhoneNumber: string;
  lattitude: number;
  longitude: number;
}

export interface viewAllBookingResponse {
  success: boolean;
  message: string;
  data?: {
    pendingBookings: viewPendingBookings[];
    activeBookings: viewActiveBookings[];
    completedBookings: viewCompleteBookings[];
  };
}

export const showAllBooking = async (): Promise<viewAllBookingResponse> => {
  try {
    const response = await axiosInstance.get(GetAllBookings);
    const bidContent = await response.data;
    return bidContent;
  } catch (error) {
    throw new Error("Failed to fetch  data");
  }
};

export const useShowAllBooking = () => {
  return useQuery<viewAllBookingResponse, Error>({
    queryKey: ["ViewDataAllBooking"],
    queryFn: showAllBooking,
  });
};

//Book Technician //Create Booking
export interface bookData {
  bidId: number;
}
export interface bookResponse {
  success: Boolean;
  message: string;
}

export const createBooking = async (bidId: string): Promise<bookResponse> => {
  try {
    const response = await axiosInstance.post(
      `${BookTechnician}?bidId=${bidId}`
    );
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
      message: backendMessage || " Booking Failed",
    };
  }
};

export const useCreateBooking = () => {
  const router = useRouter();
  const toast = useToast();

  return useMutation<bookResponse, Error, string>({
    mutationFn: createBooking,
    onSuccess: (data) => {
      if (data.success === true) {
        toast.show("Technician Booked successfully", {
          type: "success",
          placement: "top",
          duration: 4000,
          style: { marginTop: 125 },
          animationType: "slide-in",
        });
        console.log(data);
        router.replace("/(root)/(tabs)/explore");
      } else {
        toast.show(data.message || "Failed to book technician", {
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

//For Consumer get all Bookings
export interface viewPendingBookingsForConsumer {
  postDetails?: {
    id: number;
    title: string;
    description: string;
    category: string;
    lattitude: number;
    longitude: number;
  };
}

export interface viewActiveForConsumer {
  bookingId: number;
  title: string;
  price: number;
  serviceDate: string;
  // "timeFrame": null,
  technicianName: string;
  lattitude: number;
  longitude: number;
}

export interface viewCompleteForConsumer {
  bookingId: number;
  title: string;
  price: number;
  serviceDate: string;
  // "timeFrame": null,
  technicianName: string;
  lattitude: number;
  longitude: number;
}

export interface viewAllBookingResponseForConsumer {
  success: boolean;
  message: string;
  data?: {
    pendingBookings: viewPendingBookingsForConsumer[];
    activeBookings: viewActiveForConsumer[];
    completedBookings: viewCompleteForConsumer[];
  };
}

export const showAllBookingForConsumer =
  async (): Promise<viewAllBookingResponseForConsumer> => {
    try {
      const response = await axiosInstance.get(GetAllBookingsForConsumer);
      const bookingContent = await response.data;
      return bookingContent;
    } catch (error) {
      throw new Error("Failed to fetch  data");
    }
  };

export const useShowAllBookingForConsumer = () => {
  return useQuery<viewAllBookingResponseForConsumer, Error>({
    queryKey: ["ViewForConsumer"],
    queryFn: showAllBookingForConsumer,
  });
};

//To complete booking
export interface bookingData {
  BookingId: number;
}
export interface bookingResponse {
  success: Boolean;
  message: string;
}

export const completeBooking = async (
  BookingId: string
): Promise<bookingResponse> => {
  try {
    const response = await axiosInstance.post(
      `${CompleteBookings}?BookingId=${BookingId}`
    );
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
      message: backendMessage || " Booking Failed",
    };
  }
};

export const useCompleteBooking = () => {
  const router = useRouter();
  const toast = useToast();

  return useMutation<bookResponse, Error, string>({
    mutationFn: completeBooking,
    onSuccess: (data) => {
      if (data.success === true) {
        toast.show("Booking Marked as Completed successfully", {
          type: "success",
          placement: "top",
          duration: 4000,
          style: { marginTop: 125 },
          animationType: "slide-in",
        });
        console.log(data);
        router.replace("/Bookings/completebooking");
      } else {
        toast.show(data.message || "Failed to complete booking", {
          type: "danger",
          placement: "bottom",
        });
      }
    },
    onError: (error) => {
      const errorMessage =
        error instanceof Error
          ? error.message
          : "An unknown error occurred while completing booking";

      toast.show(errorMessage, {
        type: "danger",
        placement: "bottom",
      });
    },
  });
};
