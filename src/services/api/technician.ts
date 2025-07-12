import AsyncStorage from "@react-native-async-storage/async-storage";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

import axiosInstance from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";

const { GetAllTechnician } = API_ENDPOINTS;

export interface viewTechnicianData {
	technicianId: number;
	isVerified: Boolean;
	name: string;
	address: string;
	phoneNumber: string;
	secondPhoneNumber: string;
}

export interface viewAllTechnicianResponse {
	success: boolean;
	message: string;
	data?: viewTechnicianData[];
}

export const showAllTechnicianData = async (): Promise<viewAllTechnicianResponse> => {
	try {
		const response = await axiosInstance.get(GetAllTechnician);
		const Content = await response.data;
		return Content;
	} catch (error) {
		throw new Error("Failed to fetch  data");
	}
};

export const useShowAllTechnicianData = () => {
	return useQuery<viewAllTechnicianResponse, Error>({
		queryKey: ["ViewDataAllTechnician"],
		queryFn: showAllTechnicianData,
	});
};
