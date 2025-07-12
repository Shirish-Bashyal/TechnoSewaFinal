import AsyncStorage from "@react-native-async-storage/async-storage";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

import axiosInstance from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";

const { GetBookings} = API_ENDPOINTS;

export interface viewbookingData {
	id: number;
	status: string;
	title: string;
	price: number;
	serviceDate: string;
	consumerName: string;
	consumerPhone: string;
	technicianName: string;
	technicianPhone: string;
}

export interface viewAllBookingResponse {
	success: boolean;
	message: string;
	data?: viewbookingData[];
}

export const showAllBookingData = async (): Promise<viewAllBookingResponse> => {
	try {
		const response = await axiosInstance.get(GetBookings);
		const bookingContent = await response.data;
		return bookingContent;
	} catch (error) {
		throw new Error("Failed to fetch  data");
	}
};

export const useShowAllBookingData = () => {
	return useQuery<viewAllBookingResponse, Error>({
		queryKey: ["ViewDataAllBooking"],
		queryFn: showAllBookingData,
	});
};
